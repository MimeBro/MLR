using UnityEngine;

public enum MonsterBehaviour{WILD, TRAINED}
[CreateAssetMenu(fileName = "monster.asset", menuName = "Monsters/New Monster")]
public class MonsterSO : ScriptableObject
{
    public MonsterBehaviour Behaviour;
    public string MonsterName;
    public string MonsterDescription;
    public Sprite MonsterImage;
    
    public float maxHp;
    public float currentHp;
    
    public ElementalTypes type1;
    public ElementalTypes type2;
    
    public GameObject WildPrefab;
    public GameObject TrainedPrefab;
    public MovesSO MonsterAttackMove;
    
        
    
}
