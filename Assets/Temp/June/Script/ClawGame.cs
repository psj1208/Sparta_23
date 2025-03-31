using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawGame : MonoBehaviour
{
    public ClawControl ClawCont;
    public ClawSpline ClawSpli;
    public Container container;
    public int clawCount;

    private System.Action clawStartAction;

    private void Awake()
    {
        ClawCont = GetComponentInChildren<ClawControl>();
        ClawSpli = GetComponentInChildren<ClawSpline>();
        container = GetComponentInChildren<Container>();
        ClawCont.Init(this);
        ClawSpli.Init(this);
        container.Init(this);

        clawStartAction = () => ClawStart();
    }

    private void Start()
    {

        clawCount = 0;
        ClawStart(2);
    }

    public void ClawStart(int num = 1)
    {
        if (num <= 0)
            return;
        clawCount = num;
        ClawCont.StartGame(num);
    }
}
