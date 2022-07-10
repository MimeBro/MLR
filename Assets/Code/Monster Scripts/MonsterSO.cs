using System;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;

public enum MonsterBehaviour{WILD, TRAINED}
public enum EvolutionStage{Last,Middle,First}
[CreateAssetMenu(fileName = "monster.asset", menuName = "Monster/New Monster")]
public class MonsterSO : ScriptableObject
{
    [Title("Monster Info")]
    public string MonsterName;

    public bool nickname;
    [ShowIf("nickname")] 
    public string MonsterNickname;
    
    public string MonsterDescription;
    
    [PreviewField]
    public Sprite MonsterImage;

    public bool evolves;
    [ShowIf("evolves")] 
    public EvolutionStage evolutionStage;
    [ShowIf("evolves")]
    public int evolutionLevel;
    [ShowIf("evolves")]
    public PlayerController evolution;

    [Title("Monster Stats")]
    [EnumToggleButtons]
    public MonsterBehaviour Behaviour;

    [EnumPaging] public ElementalTypes primaryType;
    
    [PropertySpace]
    [ProgressBar(0,"maxHp", Height = 30)]
    public int currentHp;
    public int maxHp;
    
    [PropertySpace]
    public int maxEnergy;
    public float energyRegenerationRate;
    
    [PropertySpace]
    public int attack;
    public int specialAttack;
    
    [PropertySpace]
    public int defense;
    public int specialDefense;
    [PropertySpace]
    public int speed;
    
    [Title("Leveling Up")]
    [PropertyRange(1,"maxLevel")]
    [OnValueChanged("UpdateLevel")]
    public int level = 1;
    public int maxLevel;
    public int baseExp;
    public int currentExp;
    public int[] expToNextLevel;

    //Monster to add when captured;
    public PlayerController MonsterPrefab;

    public void AddExp(int amount)
    {
        if (level < maxLevel)
        {
            currentExp += amount;

            if (currentExp >= expToNextLevel[level])
            {
                currentExp -= expToNextLevel[level];
                level++;
            }
        }
        else
        {
            currentExp = 0;
        }
    }
        
    private void OnEnable()
    {
        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseExp;

        for (var i = 2; i < expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i - 1] * 1.1f);
        }
    }

    //INSPECTOR ONLY
    private void UpdateLevel()
    {
        currentExp = 0;
    }

    public void ResetMonster()
    {
        //reset the monster to it's default state, usually on death
    }
}
