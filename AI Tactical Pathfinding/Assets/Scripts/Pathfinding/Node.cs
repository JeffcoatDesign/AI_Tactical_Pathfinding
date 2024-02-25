using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> connectsTo = new();
    public NodeType type;

    private void OnDrawGizmos()
    {
        foreach (var node in connectsTo)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, (node.transform.position - transform.position).normalized);
        }
    }
}

public enum NodeType {
    Normal,
    SpikeTrap,
    FireTrap,
}
