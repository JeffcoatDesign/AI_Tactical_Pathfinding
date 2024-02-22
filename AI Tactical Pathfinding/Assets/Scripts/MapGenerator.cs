using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Node nodePrefab;
    [SerializeField] int m_height = 10;
    [SerializeField] int m_width = 10;
    [SerializeField] float m_nodeSpacing = 2f;
    [SerializeField] Pathfinder m_pathfinder;
    [SerializeField] MoveToClick m_target;
    [SerializeField] Transform m_goalTransform;
    private Node[,] nodes;
    private void Awake()
    {
        nodes = new Node[m_height,m_width];
        for (int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                Node node = Instantiate(nodePrefab, transform);
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

        SetStartandGoalPositions();

        m_pathfinder.Initialize();
    }

    private void SetStartandGoalPositions()
    {
        bool top = 1 == Random.Range(0, 2);
        bool right = 1 == Random.Range(0, 2);

        int x = top ? 0 : m_width - 1;
        int y = right ? 0 : m_height - 1;

        m_pathfinder.transform.position = new Vector3(x * m_nodeSpacing, 0, y * m_nodeSpacing);
        m_target.transform.position = (new Vector3(x * m_nodeSpacing + (m_nodeSpacing / 3), 0, y * m_nodeSpacing + (m_nodeSpacing / 3)));

        x = !top ? 0 : m_width - 1;
        y = !right ? 0 : m_height - 1;

        m_goalTransform.position = new Vector3(x * m_nodeSpacing, 0, y * m_nodeSpacing);
    }
}
