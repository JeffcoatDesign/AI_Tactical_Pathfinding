using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Node m_nodePrefab;
    [SerializeField] int m_height = 10;
    [SerializeField] int m_width = 10;
    [SerializeField] float m_nodeSpacing = 2f;
    [SerializeField] Pathfinder m_pathfinder;
    [SerializeField] MoveToClick m_target;
    [SerializeField] Transform m_goalTransform;
    [Header("Trap Prefabs")]
    [SerializeField] GameObject m_spikesPrefab;
    [SerializeField] GameObject m_firePrefab;
    Vector2Int m_startPosition;
    Vector2Int m_goalPosition;
    private Node[,] nodes;
    private void Awake()
    {
        StartCoroutine(Generate());
    }

    private IEnumerator Generate()
    {
        nodes = new Node[m_height, m_width];

        SetStartandGoalPositions();

        for (int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                Node node = Instantiate(m_nodePrefab, transform);
                node.transform.position = new Vector3(x * m_nodeSpacing, 0, y * m_nodeSpacing);
                Node otherNode;

                if (x > 0)
                {
                    otherNode = nodes[x - 1, y];
                    otherNode.connectsTo.Add(node);
                    node.connectsTo.Add(otherNode);
                    if (y > 0)
                    {
                        otherNode = nodes[x - 1, y - 1];
                        otherNode.connectsTo.Add(node);
                        node.connectsTo.Add(otherNode);
                    }
                    if (y < m_height - 1)
                    {
                        otherNode = nodes[x - 1, y + 1];
                        otherNode.connectsTo.Add(node);
                        node.connectsTo.Add(otherNode);
                    }
                }
                if (y > 0)
                {
                    otherNode = nodes[x, y - 1];
                    otherNode.connectsTo.Add(node);
                    node.connectsTo.Add(otherNode);
                }
                nodes[x, y] = node;

                NodeType type = (NodeType)Random.Range(0, 3);
                if ((x == m_startPosition.x) && (y == m_startPosition.y) || (x == m_goalPosition.x) && (y == m_goalPosition.y))
                    type = NodeType.Normal;
                node.type = type;
                if (type == NodeType.SpikeTrap)
                    Instantiate(m_spikesPrefab, node.transform);
                else if (type == NodeType.FireTrap)
                    Instantiate(m_firePrefab, node.transform);
                yield return null;
            }
        }
        //Graph graph = new Graph();
        //graph.Build();
        //Connection[] pathfinding = Dijkstra.Pathfind(graph, nodes[m_startPosition.x, m_startPosition.y], nodes[m_goalPosition.x, m_goalPosition.y]);

        //foreach (Node node in nodes)
        //{
        //    NodeType type = NodeType.Normal;
        //    if (!pathfinding.Any(c => c.GetFromNode() == node || c.GetToNode() == node))
        //    {
        //        type = (NodeType)Random.Range(1, 3);
        //        if (type == NodeType.SpikeTrap)
        //            Instantiate(m_spikesPrefab, node.transform);
        //        else if (type == NodeType.FireTrap)
        //            Instantiate(m_firePrefab, node.transform);
        //    }
        //    node.type = type;
        //    yield return null;
        //}

        m_pathfinder.Initialize();
        yield return null;
    }

    private void SetStartandGoalPositions()
    {
        bool top = 1 == Random.Range(0, 2);
        bool right = 1 == Random.Range(0, 2);
        m_startPosition.x = top ? 0 : m_width - 1;
        m_startPosition.y = right ? 0 : m_height - 1;
        m_goalPosition.x = !top ? 0 : m_width - 1;
        m_goalPosition.y = !right ? 0 : m_height - 1;

        m_pathfinder.transform.position = new Vector3(m_startPosition.x * m_nodeSpacing, 0, m_startPosition.y * m_nodeSpacing);
        m_target.transform.position = (new Vector3(m_startPosition.x * m_nodeSpacing + (m_nodeSpacing / 3), 0, m_startPosition.y * m_nodeSpacing + (m_nodeSpacing / 3)));
        m_goalTransform.position = new Vector3(m_goalPosition.x * m_nodeSpacing, 0, m_goalPosition.y * m_nodeSpacing);
    }
}
