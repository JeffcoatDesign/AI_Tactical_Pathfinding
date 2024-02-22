using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRenderer : MonoBehaviour
{
    [SerializeField] LineRenderer m_lineRenderer;
    public void RenderPath(Path path)
    {
        if (path == null || path.Length < 1) return;
        Vector3[] positions = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            positions[i] = path.PathObjects[i].position;
        }
        if (m_lineRenderer != null)
        {
            m_lineRenderer.positionCount = path.Length;
            m_lineRenderer.SetPositions(positions);
        }
        else
            Debug.Log("Line renderer is null");
    }
}
