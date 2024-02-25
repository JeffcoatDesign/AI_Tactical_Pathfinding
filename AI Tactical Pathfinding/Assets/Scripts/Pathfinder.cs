using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Pathfinder : Kinematic
{
    Node end;
    Graph graph;
    bool isInitialized = false;

    FollowPath myMoveType;
    LookWhereGoing myRotateType;

    public void Initialize()
    {
        isInitialized = true;
        myMoveType = new FollowPath();
        myMoveType.character = this;
        myMoveType.threshold = 0.7f;
        myMoveType.timeToTarget = 0.2f;

        CalculatePath(FindClosestNode(myTarget.transform.position));

        myRotateType = new LookWhereGoing();
        myRotateType.character = this;
        myRotateType.target = myTarget;
    }

    private void CalculatePath(Node endNode)
    {
        Node start = FindClosestNode(transform.position);
        end = endNode;
        if (graph == null)
        {
            graph = new Graph();
            graph.Build();
        }
        Connection[] pathfinding = Dijkstra.Pathfind(graph, start, end);

        if (pathfinding == null)
            Debug.Log("pathfinding is null");
        Transform[] pathPoints = new Transform[pathfinding.Length + 1];
        float[] speedModifiers = new float[pathfinding.Length + 1];
        int i = 0;
        foreach (var pathPoint in pathfinding)
        {
            pathPoints[i] = pathPoint.GetFromNode().transform;
            speedModifiers[i] = pathPoint.GetInfluence();
            i++;
        }
        pathPoints[i] = end.transform;
        if (pathfinding.Length > 1)
        speedModifiers[i] = pathfinding.Last().GetInfluence();
        Path path = new (pathPoints, speedModifiers);

        myMoveType.path = path;
        myMoveType.ResetIndex();
    }

    private Node FindClosestNode(Vector3 position)
    {
        Node[] nodes = FindObjectsByType<Node>(FindObjectsSortMode.None);
        Node closest = null;
        float closestDistance = float.MaxValue;
        foreach (Node node in nodes) {
            float distance = Vector3.Distance(position, node.transform.position);
            if (distance < closestDistance)
            {
                closest = node;
                closestDistance = distance;
            }
        }
        return closest;
    }

    public void UpdateTarget()
    {
        Node closestToTarget = FindClosestNode(myTarget.transform.position);
        CalculatePath(closestToTarget);
    }

    protected override void Update()
    {
        if (!isInitialized) return;

        steeringUpdate = new SteeringOutput();
        if (myRotateType == null) Debug.LogError("Rotation is null");
        if (myMoveType == null) Debug.LogError("Move is null");
        steeringUpdate.angular = myRotateType.getSteering().angular;
        steeringUpdate.linear = myMoveType.getSteering().linear;
        base.Update();
    }
}
