using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> ConnectsTo = new();

    private void OnDrawGizmos()
    {
        foreach (var node in ConnectsTo)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, (node.transform.position - transform.position).normalized);
        }
    }
}
