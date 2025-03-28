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

    private void LateUpdate()
    {
        if (inputList.Count > 0)
            Moving();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
}
