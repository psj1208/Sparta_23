using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawRotation : MonoBehaviour
{
    public float moveDuration = 2f; // 이동 시간
    public float tiltAmount = 30f;  // 최대 기울기

    private Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
    }

    void Update()
    {
        // 현재 속도 계산
        Vector3 velocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;

        // 이동 속도에 따라 기울기 조절
        float targetAngle = Mathf.Clamp(velocity.x * 5f, -tiltAmount, tiltAmount);

        // 부드러운 회전 적용
        transform.DOLocalRotate(new Vector3(0, 0, -targetAngle), 0.2f);
    }
}
