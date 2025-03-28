using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;

public class ClawSpline : MonoBehaviour
{
    [Serializable]
    class SplineMove
    {
        public GameObject obj;
        public float curSplinePos;
        public float targetSplinePos;

        public SplineMove(GameObject obj)
        {
            this.obj = obj;
            this.curSplinePos = 0;
            this.targetSplinePos = 0;
        }
    }
    [SerializeField] SplineContainer spline;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceBetween;
    [SerializeField] List<SplineMove> inputList;

    private void Awake()
    {
        spline = GetComponent<SplineContainer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Pop();
    }
    private void LateUpdate()
    {
        if (inputList.Count > 0)
            Moving();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //add하고 난 이후로 리스트 비었는지 검사 후에 리스트가 비었으면 턴 종료.
        inputList.Add(new SplineMove(collision.gameObject));
        CalculateT();
    }

    void Moving()
    {
        for (int i = 0; i < inputList.Count; i++)
        {
            spline.Evaluate(inputList[i].curSplinePos,out float3 position,out float3 tan, out float3 upVector);
            inputList[i].obj.transform.position = position;
            if (inputList[i].curSplinePos < inputList[i].targetSplinePos)
                inputList[i].curSplinePos += moveSpeed * Time.deltaTime;
        }
    }

    void CalculateT()
    {
        for (int i = 0; i < inputList.Count; i++)
            inputList[i].targetSplinePos = 1 - i * distanceBetween;
    }

    public void Pop()
    {
        //아이템 건네주기
        //1번 위치에 완벽히 도달한지 검사
        inputList.RemoveAt(0);
        CalculateT();
    }
}
