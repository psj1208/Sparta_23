using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        if (TurnManager.IsInstance)
            TurnManager.Instance.OnClawMachineStart -= ClawCont.GameStart;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            TurnManager.Instance.OnClawMachineStart += () => ClawStart(1);
        }
        else if (scene.name == "StageSelectScene")
        {
            ClawCont.GameStart();
        }
    }

    private void Start()
    {
        clawCount = 0;
    }

    public void ClawStart(int num = 1)
    {
        if (num <= 0)
            return;
        clawCount = num;
        ClawCont.GameStart();
    }
}
