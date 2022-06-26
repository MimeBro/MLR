using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSO : MonoBehaviour
{
    public string MonsterName;
    public string MonsterDescription;
    
    public float MonsterMaxHealth;
    
    public ElementalTypes type;
    
    public GameObject WildPrefab;
    public GameObject TrainedPrefab;
    public GameObject OwnedPrefab;
    public float SummonedAttackCooldown;
}
