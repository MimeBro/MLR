using UnityEngine;

[CreateAssetMenu(fileName = "monster.asset", menuName = "Monsters/New Monster")]
public class MonsterSO : ScriptableObject
{
    public string MonsterName;
    public string MonsterDescription;
    
    public float maxHp;
    public float currentHp;
    
    public ElementalTypes type1;
    public ElementalTypes type2;
    
    public GameObject WildPrefab;
    public GameObject TrainedPrefab;
    public MovesSO MonsterAttackMove;

    public MovesSO Capture()
    {
        return MonsterAttackMove;
    }
        
    
}
