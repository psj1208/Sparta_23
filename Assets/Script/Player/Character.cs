using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector] public ResourceController ResourceController;
    [HideInInspector] public StatHandler StatHandler;
    [HideInInspector] public Animator Animator;
    public CharacterStatUI CharacterStatUI;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        
    }
}
