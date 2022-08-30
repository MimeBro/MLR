using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public enum MonsterBehaviour{WILD, TRAINED}
public enum EvolutionStage{Last,Middle,First}
[CreateAssetMenu(fileName = "monster.asset", menuName = "Monster/New Monster")]
public class MonsterSO : ScriptableObject
{
    [Title("Monster Info")]
    public string monsterName;

    public bool nickname;
    [ShowIf("nickname")] 
    public string monsterNickname;
    
    public string MonsterDescription;
    
    [FormerlySerializedAs("monsterProfile")] [PreviewField]
    public Sprite monsterPortrait;

    [PreviewField]
    public Sprite monsterIcon;

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

    [Title("Capture Prefab")]
    //Monster to add when captured;
    public OldUnit monsterPrefab;
    
[Title("Moves")]
    public List<MovesSO> LearnedMoves = new List<MovesSO>(4);
    public List<MovesSO> CompatibleMoves = new List<MovesSO>();

    public void LearnMove(MovesSO move)
    {
        LearnedMoves.Add(move);
    }

    public void ForgetMove(int moveIndex)
    {
        LearnedMoves.Remove(LearnedMoves[moveIndex]);
    }

}
