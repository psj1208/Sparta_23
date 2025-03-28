using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executer : MonoBehaviour
{
    public SceneLoader sceneLoader;
    private void OnTriggerEnter2D(Collider2D other)
    {
        sceneLoader.LoadStage(other.gameObject.GetComponent<StageItem>().data);
        Destroy(other.gameObject);
    }
}
