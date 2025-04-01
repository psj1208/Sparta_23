using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawGamePhysics : MonoBehaviour
{
    public ClawPhysics ClawCont;
    public ClawSplinePhysics ClawSpli;
    public ContainerPhyscis container;
    public int clawCount;

    private System.Action clawStartAction;

    private void Awake()
    {
        ClawCont = GetComponentInChildren<ClawPhysics>();
        ClawSpli = GetComponentInChildren<ClawSplinePhysics>();
        container = GetComponentInChildren<ContainerPhyscis>();
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
        ClawCont.GameStart();
    }
}
