using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ClawControl : MonoBehaviour
{
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;

    [SerializeField] float closeRot;
    [SerializeField] float openRot;

    [SerializeField] float waitingTime;
    [SerializeField] float rotDuration;

    Tween leftTween;
    Tween rightTween;
    Tween delayedCall;
    // Start is called before the first frame update
    void Start()
    {
        leftHand.rotation = Quaternion.Euler(0, 0, -closeRot);
        rightHand.rotation = Quaternion.Euler(0, 180, -closeRot);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            Open();
    }

    private void Open()
    {
        StopDotween();
        leftTween = leftHand.DOLocalRotate(new Vector3(0, 0, -openRot), rotDuration);
        rightTween = rightHand.DOLocalRotate(new Vector3(0, 180, -openRot), rotDuration)
            .OnComplete(() =>
            {
                delayedCall = DOVirtual.DelayedCall(waitingTime, Close);
            }); ;
    }

    private void Close()
    {
        leftTween = leftHand.DOLocalRotate(new Vector3(0, 0, -closeRot), rotDuration);
        rightTween = rightHand.DOLocalRotate(new Vector3(0, 180, -closeRot), rotDuration);
    }

    private void StopDotween()
    {
        if (leftTween != null && leftTween.IsActive()) leftTween.Kill();
        if (rightTween != null && rightTween.IsActive()) rightTween.Kill();
        if (delayedCall != null && delayedCall.IsActive()) delayedCall.Kill();
    }
}
