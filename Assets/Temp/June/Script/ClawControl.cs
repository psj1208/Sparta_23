using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ClawControl : MonoBehaviour
{
    [Header("Transform Pos")]
    [SerializeField] Vector3 InitialPos;

    [Header("Claw About")]
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;

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

    [Header("Tweening")]
    [SerializeField] private bool isTweening;
    Tween MoveTween;
    Tween leftTween;
    Tween rightTween;
    Tween delayedCall;
    // Start is called before the first frame update
    void Start()
    {
        InitialPos = transform.position;
        leftPosX = transform.position.x - leftEnd;
        rightPosX = transform.position.x - rightEnd;
        leftHand.rotation = Quaternion.Euler(0, 0, -closeRot);
        rightHand.rotation = Quaternion.Euler(0, 180, -closeRot);
        LeftMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && MoveTween.IsPlaying())
            Open();
    }

    public void StartGame()
    {
        LeftMove();
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
        transform.DOMoveY(transform.position.y - downDistance, downDuration)
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
        transform.DOMoveY(transform.position.y + downDistance, downDuration)
            .OnComplete(()=>
            {
                delayedCall = DOVirtual.DelayedCall(waitingTime, GoInitialPos);
            });
    }

    private void GoInitialPos()
    {
        transform.DOMoveX(InitialPos.x, ReturnSpeed).SetSpeedBased(true)
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

    private void CloseNotContinuos()
    {
        leftTween = leftHand.DOLocalRotate(new Vector3(0, 0, -closeRot), rotDuration);
        rightTween = rightHand.DOLocalRotate(new Vector3(0, 180, -closeRot), rotDuration);
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
        Gizmos.DrawWireCube(leftHand.position + Vector3.down * downDistance + Vector3.left * rightEnd, box);
        Gizmos.DrawWireCube(leftHand.position, box);
        Gizmos.DrawWireCube(leftHand.position + Vector3.left * leftEnd, box);
        Gizmos.DrawWireCube(leftHand.position + Vector3.left * rightEnd, box);
#endif
    }
}
