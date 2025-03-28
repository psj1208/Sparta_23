using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executer : MonoBehaviour
{
    public SceneLoader sceneLoader;
    private void OnTriggerEnter(Collider other)
    {
        sceneLoader.LoadStage(other.gameObject.GetComponent<StageData>());
    }
}
