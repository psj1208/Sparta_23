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
    ClawGame game;
    [SerializeField] SplineContainer spline;
    [SerializeField] float timeBetweenPop;
    float curTimePop;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float distanceBetween;
    [SerializeField] private float distanceOffset;
    [SerializeField] List<SplineMove> inputList;
    Coroutine curCourtine;

    private void Awake()
    {
        spline = GetComponent<SplineContainer>();
    }

    private void Start()
    {
        curTimePop = 0;
    }

    public void Init(ClawGame game)
    {
        this.game = game;
    }

    private void LateUpdate()
    {
        if (inputList.Count > 0)
            Moving();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigid);
        inputList.Add(new SplineMove(collision.gameObject));
        CalculateT();
    }

    void Moving()
    {
        if (curTimePop < timeBetweenPop)
            curTimePop += Time.deltaTime;
        for (int i = 0; i < inputList.Count; i++)
        {
            if (inputList[i].curSplinePos < inputList[i].targetSplinePos)
                inputList[i].curSplinePos += moveSpeed * Time.deltaTime;
            if (MathF.Abs(inputList[i].targetSplinePos - inputList[i].curSplinePos) < distanceOffset)
                inputList[i].curSplinePos = inputList[i].targetSplinePos;
            spline.Evaluate(inputList[i].curSplinePos,out float3 position,out float3 tan, out float3 upVector);
            inputList[i].obj.transform.position = position;
        }
        if (inputList[0].Arrive() && curTimePop >= timeBetweenPop)
        {
            Pop();
            curTimePop = 0;
        }
    }

    void CalculateT()
    {
        if (inputList.Count <= 0)
            return;
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

    public void SplineEnd()
    {
        if (curCourtine == null) 
            curCourtine = StartCoroutine(isEnd());
    }

    IEnumerator isEnd()
    {
        //2초 뒤에 리스트가 비었는지 검사
        yield return new WaitForSeconds(2.0f);
        while (inputList.Count > 0)
            yield return null;
        if (TurnManager.IsInstance)
        {
            yield return new WaitForSeconds(2.0f);
            TurnManager.Instance.EndPlayerTurn();
        }
        if (StageManager.IsInstance)
        {
            if (StageManager.Instance.GetCurrentStageIndex() == -1)
            {
                StageManager.Instance.SelectStages();
            }
        }
        game.ClawCont.CanMove = true;
        game.clawCount--;
        if(game.clawCount <= 0)
        {
            game.ClawCont.GameEnd();
        }
        curCourtine = null;
    }
}
