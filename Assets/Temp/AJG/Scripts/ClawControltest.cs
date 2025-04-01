using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClawControlTest : MonoBehaviour
{
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;
    [SerializeField] Transform claw;

    [SerializeField] float closeRot;
    [SerializeField] float openRot;
    
    [SerializeField] float waitingTime;
    [SerializeField] float rotDuration;
    
    [SerializeField] float downDistance;
    [SerializeField] float downDuration;
    [SerializeField] float upDuration;
    
    
    
    [SerializeField] Transform attachPosition;
    [SerializeField] float attachRadius;
    [SerializeField] List<StageItem> stageItems = new List<StageItem>();
    
    [SerializeField] Transform dropPoint;
    [SerializeField] float moveToDropDuration;
    
    public LayerMask stageItemLayer;

    Tween leftTween;
    Tween rightTween;
    Tween delayedCall;
    Tween movementTween;

    Vector3 originalPosition;

    void Start()
    {
        originalPosition = claw.position;
        leftHand.rotation = Quaternion.Euler(0, 0, -closeRot);
        rightHand.rotation = Quaternion.Euler(0, 0, closeRot);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            StartClawSequence();
        }
    }

    public void StartClawSequence()
    {
        Open();
        Down();
    }

    void Down()
    {
        Vector3 targetPosition = claw.position + new Vector3(0, -downDistance, 0);
        movementTween = claw.DOMoveY(targetPosition.y, downDuration);
    }

    void Open()
    {
        StopTweens();
        if (stageItems.Count > 0)
        {
            foreach (var item in stageItems)
            {
                Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
                rb.isKinematic = false;
            }
        }
        leftTween = leftHand.DOLocalRotate(new Vector3(0, 0, -openRot), rotDuration);
        rightTween = rightHand.DOLocalRotate(new Vector3(0, 0, openRot), rotDuration)
            .OnComplete(() =>
            {
                delayedCall = DOVirtual.DelayedCall(waitingTime, Close);
            });
    }

    
    void Close()
    {
        leftTween = leftHand.DOLocalRotate(new Vector3(0, 0, -closeRot), rotDuration);
        rightTween = rightHand.DOLocalRotate(new Vector3(0, 0, closeRot), rotDuration)
            .OnComplete(Up);
    }

    void Up()
    {
        Attach();
        movementTween = claw.DOMove(originalPosition, upDuration).OnComplete(MoveToDropPoint);
    }

    void MoveToDropPoint()
    {
        movementTween = claw.DOMove(dropPoint.position, moveToDropDuration).OnComplete(Finish);
    }

    void Finish()
    {
        StopTweens();
        if (stageItems.Count > 0)
        {
            foreach (var item in stageItems)
            {
                Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
                rb.isKinematic = false;
            }
        }
        leftTween = leftHand.DOLocalRotate(new Vector3(0, 0, -openRot), rotDuration);
        rightTween = rightHand.DOLocalRotate(new Vector3(0, 0, openRot), rotDuration)
            .OnComplete(() =>
            {
                delayedCall = DOVirtual.DelayedCall(waitingTime, StageManager.Instance.SelectStages);
            });
    }

    
    void StopTweens()
    {
        leftTween?.Kill();
        rightTween?.Kill();
        delayedCall?.Kill();
        movementTween?.Kill();
    }

    void Attach()
    {
        stageItems.Clear();
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attachPosition.position, attachRadius, stageItemLayer);
        foreach (Collider2D hitCollider in hitColliders)
        {
            StageItem item = hitCollider.GetComponent<StageItem>();
            if (item != null)
            {
                stageItems.Add(item);
                
                Rigidbody2D rb = hitCollider.GetComponent<Rigidbody2D>();
                rb.isKinematic = true;
                item.transform.SetParent(attachPosition);
                // item.transform.localPosition = Vector3.zero;
            }
        }
    }
    
    void OnDrawGizmos()
    {
        if (attachPosition != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attachPosition.position, attachRadius);
        }
    }
}
