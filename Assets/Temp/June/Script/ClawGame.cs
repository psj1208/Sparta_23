using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawGame : MonoBehaviour
{
    public ClawControl ClawCont;
    public ClawSpline ClawSpli;
    public Container container;
    bool TurnEnd;

    private void Awake()
    {
        ClawCont = GetComponentInChildren<ClawControl>();
        ClawSpli = GetComponentInChildren<ClawSpline>();
        container = GetComponentInChildren<Container>();
        ClawCont.Init(this);
        ClawSpli.Init(this);
        container.Init(this);
    }

    private void Start()
    {
        TurnEnd = false;
        ClawStart();
    }

    public void ClawStart(int num = 1)
    {
        ClawCont.StartGame(num);
    }
}
