using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
//좌우 이동, esc 종료, 스페이스바 시작, 탭 집기.
public class ClawControl : MonoBehaviour
{
    ClawGame game;
    [Header("Transform Pos")]
    [SerializeField] Vector3 InitialPos;
    Vector3 startPos;

    [Header("Claw About")]
    public bool IsGameStart;
    public bool CanMove;
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;
    [SerializeField] float startDownDistance;

    [Header("Rotaion About")]
    [SerializeField] float closeRot;
    [SerializeField] float openRot;

    [Header("Open&Close")]
    [SerializeField] float waitingTime;
    [SerializeField] float rotDuration;

    [Header("GoDown&GoUp")]
    [SerializeField] float downDistance;
    [SerializeField] float downDuration;

    [Header("SideMove")]
    [SerializeField] float leftEnd;
    [SerializeField] float rightEnd;
    [SerializeField] float MoveSpeed;
    [SerializeField] float ReturnSpeed;

    float leftPosX;
    float rightPosX;

    [Header("Debug(Gizmos)")]
    [SerializeField] float boxSizeX;
    [SerializeField] float boxSizeY;

    [Header("Tweening&Game")]
    [SerializeField] private bool isTweening;
    Tween MoveTween;
    Tween leftTween;
    Tween rightTween;
    Tween delayedCall;

    [Header("Line Render")]
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform LinePoint;
    [SerializeField] Transform ClawParent;

    // Start is called before the first frame update
    public void Init(ClawGame game)
    {
        this.game = game;
    }

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();    
    }

    void Start()
    {
        IsGameStart = false;
        CanMove = false;
        InitialPos = transform.position;
        startPos = InitialPos + Vector3.down * startDownDistance;
        leftPosX = transform.position.x - leftEnd;
        rightPosX = transform.position.x - rightEnd;
        leftHand.rotation = Quaternion.Euler(0, 0, -closeRot);
        rightHand.rotation = Quaternion.Euler(0, 180, -closeRot);
    }

    // Update is called once per frame
    void Update()
    {
        LineRender();
    }

    private void LateUpdate()
    {
        if (!IsGameStart || !CanMove) 
            return;
        if (Input.GetKeyDown(KeyCode.Tab))
            Open();
        Move();
    }

    public void LineRender()
    {
        if (lineRenderer == null)
            return;
        lineRenderer.SetPosition(0, LinePoint.position);
        lineRenderer.SetPosition(1, ClawParent.position);
        lineRenderer.positionCount = 2;
    }
    public void StartGame(int count)
    {
        if (IsGameStart)
            return;
        transform.DOMove(startPos, MoveSpeed).SetEase(Ease.Linear).SetSpeedBased(true)
            .OnComplete(()=>
            {
                IsGameStart = true;
                CanMove = true;
            });
    }

    public void GameEnd()
    {
        if (!IsGameStart)
            return;
        IsGameStart = false;
        CanMove = false;
        transform.DOMove(startPos, MoveSpeed).SetEase(Ease.Linear).SetSpeedBased(true)
            .OnComplete(()=>
            {
                transform.DOMove(InitialPos, MoveSpeed).SetEase(Ease.Linear).SetSpeedBased(true);
            });
    }

    public void Move()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            float x = transform.position.x - Time.deltaTime * MoveSpeed;
            x = Mathf.Clamp(x, leftPosX, InitialPos.x);
            transform.position = new Vector3(x, transform.position.y, 0);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            float x = transform.position.x + Time.deltaTime * MoveSpeed;
            x = Mathf.Clamp(x, leftPosX , InitialPos.x);
            transform.position = new Vector3(x, transform.position.y, 0);
        }
    }
    private void LeftMove()
    {
        MoveTween = transform.DOMoveX(leftPosX, MoveSpeed).SetEase(Ease.Linear).SetSpeedBased(true)
            .OnComplete(()=>
            {
                RightMove();
            });
    }

    private void RightMove()
    {
        MoveTween = transform.DOMoveX(rightPosX, MoveSpeed).SetEase(Ease.Linear).SetSpeedBased(true)
            .OnComplete(()=>
            {
                LeftMove();
            });
    }

    private void Open()
    {
        CanMove = false;
        MoveTween.Kill();
        leftTween = leftHand.DOLocalRotate(new Vector3(0, 0, -openRot), rotDuration);
        rightTween = rightHand.DOLocalRotate(new Vector3(0, 180, -openRot), rotDuration)
            .OnComplete(() =>
            {
                delayedCall = DOVirtual.DelayedCall(waitingTime, GoDown);
            }); ;
    }

    private void GoDown()
    {
        ClawParent.DOMoveY(ClawParent.position.y - downDistance, downDuration)
            .OnComplete(() =>
            {
                delayedCall = DOVirtual.DelayedCall(waitingTime, Close);
            });
    }

    private void Close()
    {
        leftTween = leftHand.DOLocalRotate(new Vector3(0, 0, -closeRot), rotDuration);
        rightTween = rightHand.DOLocalRotate(new Vector3(0, 180, -closeRot), rotDuration)
            .OnComplete(()=>
            {
                delayedCall = DOVirtual.DelayedCall(waitingTime, GoUP);
            });
    }

    private void GoUP()
    {
        ClawParent.DOMoveY(ClawParent.position.y + downDistance, downDuration)
            .OnComplete(()=>
            {
                delayedCall = DOVirtual.DelayedCall(waitingTime, GoStartPos);
                TurnManager.Instance.EndClawMachine();
            });
    }

    private void GoStartPos()
    {
        transform.DOMove(startPos, ReturnSpeed).SetSpeedBased(true)
            .OnComplete(()=>
            {
                delayedCall = DOVirtual.DelayedCall(waitingTime, OpenAndClose);
            });
    }

    private void OpenAndClose()
    {
        leftTween = leftHand.DOLocalRotate(new Vector3(0, 0, -openRot), rotDuration);
        rightTween = rightHand.DOLocalRotate(new Vector3(0, 180, -openRot), rotDuration)
            .OnComplete(()=>
            {
                delayedCall = DOVirtual.DelayedCall(1f, CloseNotContinuos);
            });
    }
    //일련의 동작 마지막
    private void CloseNotContinuos()
    {
        leftTween = leftHand.DOLocalRotate(new Vector3(0, 0, -closeRot), rotDuration);
        rightTween = rightHand.DOLocalRotate(new Vector3(0, 180, -closeRot), rotDuration)
            .OnComplete(()=>
            {
                game.ClawSpli.SplineEnd();
            });
    }

    private void StopDotween()
    {
        if (MoveTween != null && MoveTween.IsActive()) MoveTween.Kill();
        if (leftTween != null && leftTween.IsActive()) leftTween.Kill();
        if (rightTween != null && rightTween.IsActive()) rightTween.Kill();
        if (delayedCall != null && delayedCall.IsActive()) delayedCall.Kill();
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        if(Application.isPlaying) return;
        Vector3 box = new Vector3(boxSizeX, boxSizeY, 1);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(leftHand.position, box);
        Gizmos.DrawWireCube(leftHand.position + Vector3.down * downDistance + Vector3.left * rightEnd + Vector3.down * startDownDistance, box);
        Gizmos.DrawWireCube(leftHand.position + Vector3.down * startDownDistance, box);
        Gizmos.DrawWireCube(leftHand.position + Vector3.left * leftEnd + Vector3.down * startDownDistance, box);
        Gizmos.DrawWireCube(leftHand.position + Vector3.left * rightEnd + Vector3.down * startDownDistance, box);
#endif
    }
}
