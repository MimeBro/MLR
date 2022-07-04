using UnityEngine;

public enum MoveRarity{COMMON, RARE, EPIC, LEGENDARY}
public enum MoveType{SKILL, ATTACK, SUMMON, PASSIVE}
[CreateAssetMenu(fileName = "Move.asset", menuName = "Moves/New Move")]
public class MovesSO : ScriptableObject
{
    public string moveName;
    public int damage;
    public int energyCost;

    public string moveDescription;
    public MoveType moveType;

    public bool waitTillAttackEnds;
    public bool castOnDraw;
    
    public Sprite moveIcon;
    
    public AAttack moveGameObject;

    public float MoveDuration()
    {
        return moveGameObject.moveDuration;
    }
}
