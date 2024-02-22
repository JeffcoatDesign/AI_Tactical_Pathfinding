using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveToClick : MonoBehaviour
{
    public UnityEvent OnMove = new UnityEvent();
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                transform.position = hit.point;
                OnMove.Invoke();
            }
        }
    }
}
