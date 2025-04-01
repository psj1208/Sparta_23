using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Scriptable Object / New Skill")]

public class SkillSO : ScriptableObject
{
    [SerializeField] public string skillName;
    [SerializeField] public Sprite sprite;

}
