using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector] public ResourceController ResourceController;
    [HideInInspector] public StatHandler StatHandler;
    [HideInInspector] public Animator Animator;
    [SerializeField] public HpBar hpBar;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        
    }
}
