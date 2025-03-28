using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public ResourceController ResourceController;
    [HideInInspector] public StatHandler StatHandler;
    [HideInInspector] public Animator Animator;

    public PlayerStateMachine PlayerStateMachine;

    private Queue<Skill> SkillQueue;

    private void Awake()
    {
        ResourceController = GetComponent<ResourceController>();
        StatHandler = GetComponent<StatHandler>();
        Animator = GetComponentInChildren<Animator>();

        PlayerStateMachine = new PlayerStateMachine(this);
        PlayerStateMachine.ChangeState(PlayerStateMachine.IdleState);
    }

    private void Update()
    {
        PlayerStateMachine.Update();

#if DEBUG
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.IdleState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.AttackState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ResourceController.ChangeHealth(-5);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ResourceController.ChangeHealth(-10000);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log($"기존 공격력 : {StatHandler.GetStat(EStatType.Attack)}");
            StatHandler.ModifyStat(EStatType.Attack, 10, true, 0);
            Debug.Log($"최종 공격력 : {StatHandler.GetStat(EStatType.Attack)}");
            return;
        }
#endif
    }

    /// <summary>
    /// 플레이어가 한 턴에 사용할 공격Skill 들을 추가.
    /// </summary>
    /// <param name="selectedSkills">추가할 Skill의 List</param>
    public void AddSkills(List<Skill> selectedSkills)
    {
        foreach(Skill skill in selectedSkills)
        {
            SkillQueue.Enqueue(skill);
        }
    }

    /// <summary>
    /// AttackState로 전환.
    /// </summary>
    public void StartAttack()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.AttackState);
    }

    /// <summary>
    /// SkillQueue에서 한 개의 Skill을 Dequeue하여 실행.
    /// </summary>
    /// <returns>실행 성공 여부를 반환</returns>
    public bool ExecuteSkill()
    {
        if(SkillQueue.TryDequeue(out Skill skill))
        {
            skill.Use();
            return true;
        }
        return false;
    }
}