using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawGame : MonoBehaviour
{
    public ClawControl ClawCont;
    public ClawSpline ClawSpli;
    public Container container;
    bool TurnEnd;

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
        TurnEnd = false;
    }

    public void ClawStart(int num = 1)
    {
        Debug.Log("Claw Turn");
        ClawCont.StartGame(num);
    }

    private void OnEnable()
    {
        TurnManager.Instance.OnClawMachineStart += clawStartAction;
    }

    private void OnDisable()
    {
        TurnManager.Instance.OnClawMachineStart -= clawStartAction;
    }
}
