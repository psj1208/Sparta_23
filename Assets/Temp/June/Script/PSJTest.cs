using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PSJTest : MonoBehaviour
{
    enum ClawState
    {
        Close,
        Open
    }
    public float moveSpeed = 5f; // 이동 속도
    private Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;
    [SerializeField] ClawState state;
    Rigidbody2D le;
    Rigidbody2D ri;
    public float targetAngle = 45f;

    void Start()
    {
        le = left.GetComponent<Rigidbody2D>();
        ri = right.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        state = ClawState.Close;
    }

    void Update()
    {
        // 입력값 받아오기 (-1 ~ 1 범위)
        moveInput.x = Input.GetAxisRaw("Horizontal"); // ← → 방향키 (A, D 포함)
        moveInput.y = Input.GetAxisRaw("Vertical");   // ↑ ↓ 방향키 (W, S 포함)

        moveInput.Normalize(); // 대각선 이동 시 속도 조절
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Z))
            Open();
        if (Input.GetKey(KeyCode.X))
            Close();
        rb.velocity = moveInput * moveSpeed; // 이동
        if(state == ClawState.Open)
        {
            le.MoveRotation(-targetAngle);
            ri.MoveRotation(targetAngle);
        }
        else if(state == ClawState.Close)
        {
            le.MoveRotation(0);
            ri.MoveRotation(0);
        }
    }

    void Open()
    {
        state = ClawState.Open;
    }

    void Close()
    {
        state = ClawState.Close;
    }
}
