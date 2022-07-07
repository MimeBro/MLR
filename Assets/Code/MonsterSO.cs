using Sirenix.OdinInspector;
using UnityEngine;

public enum MonsterBehaviour{WILD, TRAINED}
[CreateAssetMenu(fileName = "monster.asset", menuName = "Monster/New Monster")]
public class MonsterSO : ScriptableObject
{
    [Title("Monster Info")]
    public string MonsterName;

    public bool nickname;
    [ShowIf("nickname")] 
    public string MonsterNickname;
    
    public string MonsterDescription;

    public Sprite MonsterImage;
    
    public bool evolves;
    [ShowIf("evolves")]
    public int evolutionLevel;
    [ShowIf("evolves")]
    public PlayerController evolution;

    [Title("Monster Stats")]
    [EnumToggleButtons]
    public MonsterBehaviour Behaviour;

    [EnumToggleButtons]
    public ElementalTypes primaryType, secondaryType;

    [ProgressBar(0,"maxHp", Height = 30)]
    public int currentHp;
    public int maxHp;

    public int maxEnergy;
    public float energyRegen;

    public int level;
    public int exp;
    public int expToNextLevel;

    //Monster to add when captured;
    public PlayerController MonsterPrefab;


    public void ResetMonster()
    {
        //reset the monster to it's default state, usually on death
    }
    
}
