using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineFollowing : MonoBehaviour
{
    [SerializeField] private Transform target;
    LineRenderer lineRenderer;
    Vector2 pos;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawLine();
    }

    private void FixedUpdate()
    {
        pos = new Vector2(target.position.x, transform.position.y);
        transform.position = pos;
    }

    void DrawLine()
    {
        if (lineRenderer == null)
            return;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, target.position);
        lineRenderer.positionCount = 2;
    }
}
