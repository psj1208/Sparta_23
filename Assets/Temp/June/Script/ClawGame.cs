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
<<<<<<< HEAD
        TurnEnd = false;
=======
        clawCount = 0;
        ClawStart(2);
>>>>>>> parent of b96b1ef (Revert "[feat]클로 시스템 턴제 연동")
    }

    public void ClawStart(int num = 1)
    {
<<<<<<< HEAD
        Debug.Log("Claw Turn");
=======
        if (num <= 0)
            return;
        clawCount = num;
>>>>>>> parent of b96b1ef (Revert "[feat]클로 시스템 턴제 연동")
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
