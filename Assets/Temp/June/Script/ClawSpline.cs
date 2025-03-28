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

        public bool Arrive()
        {
            return curSplinePos == targetSplinePos;
        }
    }
    [SerializeField] SplineContainer spline;
    [SerializeField] float timeBetweenPop;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceBetween;
    [SerializeField] private float distanceOffset;
    [SerializeField] List<SplineMove> inputList;
    bool TurnStart;

    private void Awake()
    {
        spline = GetComponent<SplineContainer>();
    }

    private void Start()
    {
        TurnStart = false;    
    }

    private void Update()
    {
        if (TurnStart == false)
            return;
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
        TurnStart = true;
        collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid);
        if (rigid != null)
            rigid.simulated = false;
        inputList.Add(new SplineMove(collision.gameObject));
        CalculateT();
    }

    void Moving()
    {
        for (int i = 0; i < inputList.Count; i++)
        {
            if (inputList[i].curSplinePos < inputList[i].targetSplinePos)
                inputList[i].curSplinePos += moveSpeed * Time.deltaTime;
            if (MathF.Abs(inputList[i].targetSplinePos - inputList[i].curSplinePos) < distanceOffset)
                inputList[i].curSplinePos = inputList[i].targetSplinePos;
            spline.Evaluate(inputList[i].curSplinePos,out float3 position,out float3 tan, out float3 upVector);
            inputList[i].obj.transform.position = position;
        }
    }

    void CalculateT()
    {
        inputList[0].targetSplinePos = 1;
        for (int i = 1; i < inputList.Count; i++)
            inputList[i].targetSplinePos = 1 - i * distanceBetween;
    }

    public void Pop()
    {
        //아이템 건네주기
        //1번 위치에 완벽히 도달한지 검사
        inputList.RemoveAt(0);
        CalculateT();
    }

    //첫번째 인덱스가 도착했는지
    void IsFirstIndexCome()
    {
        
    }
}
