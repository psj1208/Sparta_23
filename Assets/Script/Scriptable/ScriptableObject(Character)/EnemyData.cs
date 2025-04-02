using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_", menuName = "Enemy/Enemy")]
public class EnemyData : ScriptableObject
{
    public int power;
    public GameObject prefab;
}
