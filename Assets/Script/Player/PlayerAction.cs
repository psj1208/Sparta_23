using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public List<Skill> SkillList;
    private float attackInterval = 0.5f;
    private WaitForSeconds WaitInterval;
    private Coroutine AttackCoroutine;

    private void Awake()
    {
        WaitInterval = new WaitForSeconds(attackInterval);
    }

    public void AttackStart()
    {
        if(AttackCoroutine != null)
        {
            StopCoroutine(AttackCoroutine);
        }
        StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        foreach(Skill skill in SkillList)
        {
            yield return WaitInterval;
            skill.Use();
        }
    }
}
