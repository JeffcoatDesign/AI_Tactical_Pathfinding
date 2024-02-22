using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Goal : MonoBehaviour
{
    [SerializeField] float m_minDistance = 1f;
    Pathfinder pathfinder;
    private void Awake()
    {
        pathfinder = FindAnyObjectByType<Pathfinder>();
    }
    private void Update()
    {
        if (pathfinder != null)
        {
            float distance = Vector3.Distance(transform.position, pathfinder.transform.position);
            if (distance < m_minDistance) {
                EndLevel();
            }
        }
    }

    private void EndLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
