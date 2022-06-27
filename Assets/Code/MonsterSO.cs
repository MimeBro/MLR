using UnityEngine;

public class MonsterSO : MonoBehaviour
{
    public string MonsterName;
    public string MonsterDescription;
    
    public float maxHp;
    public float currentHp;
    
    public ElementalTypes type1;
    public ElementalTypes type2;
    
    public GameObject WildPrefab;
    public GameObject TrainedPrefab;
    public MonsterAttack AttackPrefab;
}
