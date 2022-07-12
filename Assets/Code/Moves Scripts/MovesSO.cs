using Sirenix.OdinInspector;
using UnityEngine;

public enum MoveRarity{COMMON, RARE, EPIC, LEGENDARY}
public enum MoveCategory{SKILL, ATTACK}
[CreateAssetMenu(fileName = "Move.asset", menuName = "Moves/New Move")]
public class MovesSO : ScriptableObject
{
    [Title("Attack Info")] 
    [EnumToggleButtons]
    public MoveRarity rarity;

    public string moveName;

    [EnumToggleButtons]
    public MoveCategory moveCategory;

    [PreviewField]
    public Sprite moveIcon;
    
    [TextArea]
    public string moveDescription;

    [Title("Attack Stats")] 
    public ElementalTypes moveType;
    public int baseDamage;
    public int energyCost;
    public float cooldown;

    [Title("Setup")]
    public bool waitTillAttackEnds;
    public bool castOnDraw;
    public AttackController attackController;
}
