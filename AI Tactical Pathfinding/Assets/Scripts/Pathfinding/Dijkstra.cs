using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra
{
    public static Connection[] Pathfind (Graph graph, Node start, Node end)
    {
        NodeRecord startRecord = new NodeRecord();
        startRecord.node = start;
        startRecord.connection = null;
        startRecord.costSoFar = 0f;

        PathfindingList open = new PathfindingList();
        open.Add(startRecord);
        PathfindingList closed = new PathfindingList();

        NodeRecord current = new();
        while (open.Length > 0)
        {
            current = open.smallestElement;
            if (current.node == end)
                break;

            Connection[] connections = graph.GetConnections(current.node);

            foreach (Connection connection in connections)
            {
                Node endNode = connection.GetToNode();
                NodeRecord endNodeRecord = new();
                float endNodeCost = current.costSoFar + connection.GetCost();

                if (closed.Contains(endNode))
                    continue;
                else if (open.Contains(endNode))
                {
                    endNodeRecord = open.Find(endNode);
                    if (endNodeRecord.costSoFar <= endNodeCost)
                        continue;
                }
                else
                {
                    endNodeRecord.node = endNode;
                }
                endNodeRecord.costSoFar = endNodeCost;
                endNodeRecord.connection = connection;

                if (!open.Contains(endNode))
                    open.Add(endNodeRecord);
            }
            open.Remove(current);
            closed.Add(current);
        }

        if (current.node != end)
        {
            return null;
        }
        else
        {
            List<Connection> path = new();
            while (current.node != start)
            {
                path.Add(current.connection);
                current = closed.Find(current.connection.GetFromNode());
            }
            path.Reverse();
            return path.ToArray();
        }
    }
}