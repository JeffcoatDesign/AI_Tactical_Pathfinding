using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] Node nodePrefab;
    [SerializeField] int m_height = 10;
    [SerializeField] int m_width = 10;
    [SerializeField] float m_nodeSpacing = 2f;
    [SerializeField] Pathfinder m_pathfinder;
    private Node[,] nodes;
    private void Awake()
    {
        nodes = new Node[m_height,m_width];
        for (int x = 0; x < m_height; x++)
        {
            for (int y = 0; y < m_width; y++)
            {
                Node node = Instantiate(nodePrefab);
                node.transform.position = new Vector3(x * m_nodeSpacing, 0, y * m_nodeSpacing);
                Node otherNode;
                if (x > 0)
                {
                    otherNode = nodes[x - 1, y];
                    otherNode.ConnectsTo.Add(node);
                    node.ConnectsTo.Add(otherNode);
                }
                if (y > 0)
                {
                    otherNode = nodes[x, y - 1];
                    otherNode.ConnectsTo.Add(node);
                    node.ConnectsTo.Add(otherNode);
                }
                nodes[x,y] = node;
            }
        }

        m_pathfinder.Initialize();
    }
}
