using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            TurnManager.Instance.OnClawMachineStart += ClawCont.StartGame;
        }
        else if (scene.name == "DontDestroy")
        {
        }
        else
        {
        }
    }
    
    
    private void Start()
    {
        // clawCount = 0;
    }

    public void ClawStart(int num = 1)
    {
        // if (num <= 0)
        //     return;
        // clawCount = num;
    }
}
