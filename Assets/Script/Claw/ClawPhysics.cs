using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;

public class ClawPhysics : MonoBehaviour
{
    [Header("Claw Info")]
    private ClawGamePhysics clawGame;
    public bool CanMove;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] ClawState state;
    [SerializeField] private float moveSpeed;
    Rigidbody2D parentRigid;
    Rigidbody2D leftRigid;
    Rigidbody2D rightRigid;
    HingeJoint2D leftJoint;
    HingeJoint2D rightJoint;

    [Header("Start About")]
    [SerializeField] private float startDownDistance;

    [Header("Open&Close")]
    [SerializeField] private float closeDegree;
    [SerializeField] private float openDegree;
    [SerializeField] private float speed;

    [Header("UpDownMove")]
    [SerializeField] private float downDistance;
    [SerializeField] private float verticalMoveSpeed;

    [Header("SideMove")]
    [SerializeField] private float horizontalMoveSpeed;
    [SerializeField] private float RightEnd;
    [SerializeField] private float LeftEnd;

    [Header("DebugSize")]
    [SerializeField] private float boxSizeX;
    [SerializeField] private float boxSizeY;

    [Header("Line Renderer")]
    [SerializeField] LineRenderer lineRenderer;
    Vector2 moveInput;

    Vector3 initialPos;
    Vector3 startPos;
    Vector3 rightPos;
    Vector3 leftPos;

    public void Init(ClawGamePhysics game)
    {
        clawGame = game;
    }
    public void SetInitalPos()
    {
        initialPos = transform.position;
        startPos = initialPos + Vector3.down * startDownDistance;
        rightPos = startPos + Vector3.left * RightEnd;
        leftPos = startPos + Vector3.left * LeftEnd;
    }

    private void Awake()
    {
        parentRigid = GetComponent<Rigidbody2D>();
        leftRigid = left.GetComponent<Rigidbody2D>();
        rightRigid = right.GetComponent<Rigidbody2D>();
        SetInitalPos();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        if (!CanMove)
            return;
        if (parentRigid.position.x < leftPos.x)
        {
            parentRigid.position = new Vector2(leftPos.x, parentRigid.position.y);
            moveInput.x = 0;
        }
        else if (parentRigid.position.x > rightPos.x)
        {
            parentRigid.position = new Vector2(rightPos.x, parentRigid.position.y);
            moveInput.x = 0;
        }
    }

    private void FixedUpdate()
    {
        OpenAndClose();
        if (!CanMove)
            return;
        if (Input.GetKey(KeyCode.Tab))
            CatchAndReturnMove();
        Move();
    }

    void Move()
    {
        parentRigid.velocity = moveInput * moveSpeed * Time.fixedDeltaTime;
    }

    void OpenAndClose()
    {
        float targetLeftAngle;
        float targetRightAngle;
        if (state == ClawState.Idle)
            return;
        targetLeftAngle = (state == ClawState.Open) ? -openDegree : -closeDegree;
        targetRightAngle = (state == ClawState.Open) ? openDegree : closeDegree;

        // 현재 각도에서 목표 각도로 speed만큼 부드럽게 회전
        float newLeftAngle = Mathf.MoveTowardsAngle(leftRigid.rotation, targetLeftAngle, speed * Time.fixedDeltaTime);
        float newRightAngle = Mathf.MoveTowardsAngle(rightRigid.rotation, targetRightAngle, speed * Time.fixedDeltaTime);

        leftRigid.MoveRotation(newLeftAngle);
        rightRigid.MoveRotation(newRightAngle);
    }

    void ChangeState(ClawState state)
    {
        this.state = state;
    }

    public void GameStart()
    {
        CanMove = false;
        StartCoroutine(GameStartCourt());
    }

    IEnumerator GameStartCourt()
    {
        yield return new WaitForFixedUpdate();
        parentRigid.velocity = Vector2.zero;
        //스타트 포지션으로 이동
        while (true)
        {
            parentRigid.MovePosition(Vector2.MoveTowards(parentRigid.position, startPos, verticalMoveSpeed * Time.fixedDeltaTime));
            if (Vector2.Distance(parentRigid.position, startPos) < 0.01f)
            {
                parentRigid.position = startPos; // 정확한 위치 보정
                yield return new WaitForFixedUpdate();
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        //우측 포지션으로 이동
        while (true)
        {
            parentRigid.MovePosition(Vector2.MoveTowards(parentRigid.position, rightPos, horizontalMoveSpeed * Time.fixedDeltaTime));
            if (Vector2.Distance(parentRigid.position, rightPos) < 0.01f)
            {
                parentRigid.position = startPos; // 정확한 위치 보정
                yield return new WaitForFixedUpdate();
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        CanMove = true;
        yield return new WaitForFixedUpdate();
    }

    public void CatchAndReturnMove()
    {
        CanMove = false;
        StartCoroutine(CatchAndReturn());
    }

    IEnumerator CatchAndReturn()
    {
        yield return new WaitForFixedUpdate();
        parentRigid.velocity = Vector2.zero;
        float elapsedTime = 0f; // 경과 시간 변수
        float maxTime = 2f; // 최대 대기 시간
        //집게 오픈
        ChangeState(ClawState.Open);
        yield return new WaitForSeconds(1.0f);

        // 아래로 이동
        Vector3 targetPos = parentRigid.position + Vector2.down * downDistance;
        while (true)
        {
            parentRigid.MovePosition(Vector2.MoveTowards(parentRigid.position, targetPos, verticalMoveSpeed * Time.fixedDeltaTime));

            elapsedTime += Time.fixedDeltaTime;
            if (elapsedTime >= maxTime)
            {
                yield return new WaitForFixedUpdate();
                break;
            }

            yield return new WaitForFixedUpdate();
        }

        //집게 닫기
        ChangeState(ClawState.Close);
        yield return new WaitForSeconds(1.0f);

        // 위로 이동
        targetPos = parentRigid.position + Vector2.up * downDistance;
        while (true)
        {
            parentRigid.MovePosition(Vector2.MoveTowards(parentRigid.position, targetPos, verticalMoveSpeed * Time.fixedDeltaTime));
            if (Vector2.Distance(parentRigid.position, targetPos) < 0.01f)
            {
                parentRigid.position = targetPos; // 정확한 위치 보정
                yield return new WaitForFixedUpdate();
                parentRigid.velocity = Vector2.zero;
                break;
            }
            yield return new WaitForFixedUpdate();
        }

        state = ClawState.Idle;
        
        // Idle 되는 시점에서 턴매니저에 전달.
        if (TurnManager.IsInstance)
            TurnManager.Instance.EndClawMachine();
        
        //시작 위치로
        while (true)
        {
            parentRigid.MovePosition(Vector2.MoveTowards(parentRigid.position, startPos, horizontalMoveSpeed * Time.fixedDeltaTime));
            if (Vector2.Distance(parentRigid.position, startPos) < 0.01f)
            {
                parentRigid.position = startPos; // 정확한 위치 보정
                yield return new WaitForFixedUpdate();
                parentRigid.velocity = Vector2.zero;
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        //집게 열고 닫기
        yield return new WaitForSeconds(2.0f);
        ChangeState(ClawState.Open);
        yield return new WaitForSeconds(2.0f);
        ChangeState(ClawState.Close);
        yield return new WaitForSeconds(1.0f);
        ChangeState(ClawState.Idle);
        clawGame.ClawSpli.SplineEnd();

        yield return new WaitForFixedUpdate();
    }
    
    public void GameEnd()
    {
        StartCoroutine(GameEndCourt());
    }

    IEnumerator GameEndCourt()
    {
        CanMove = false;
        ChangeState(ClawState.Idle);
        //시작 위치로
        while (true)
        {
            parentRigid.MovePosition(Vector2.MoveTowards(parentRigid.position, startPos, horizontalMoveSpeed * Time.fixedDeltaTime));
            if (Vector2.Distance(parentRigid.position, startPos) < 0.01f)
            {
                parentRigid.position = startPos; // 정확한 위치 보정
                yield return new WaitForFixedUpdate();
                parentRigid.velocity = Vector2.zero;
                break;
            }
            yield return new WaitForFixedUpdate();
        }

        //초기 위치로
        while (true)
        {
            parentRigid.MovePosition(Vector2.MoveTowards(parentRigid.position, initialPos, verticalMoveSpeed * Time.fixedDeltaTime));
            if (Vector2.Distance(parentRigid.position, initialPos) < 0.01f)
            {
                parentRigid.position = initialPos; // 정확한 위치 보정
                yield return new WaitForFixedUpdate();
                parentRigid.velocity = Vector2.zero;
                break;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if (Application.isPlaying) return;
        Vector2 boxSize = new Vector2(boxSizeX, boxSizeY);
        Gizmos.DrawWireCube(transform.position, boxSize);
        Gizmos.DrawWireCube(transform.position + Vector3.down * startDownDistance, boxSize);
        Gizmos.DrawWireCube(transform.position + Vector3.down * startDownDistance + Vector3.left * RightEnd, boxSize);
        Gizmos.DrawWireCube(transform.position + Vector3.down * startDownDistance + Vector3.left * LeftEnd, boxSize);
#endif
    }
}
