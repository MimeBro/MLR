using UnityEngine;

public enum MoveRarity{}
public enum MoveType{BASICATTACK,SKILL, ATTACK}
[CreateAssetMenu(fileName = "Move.asset", menuName = "Moves/New Move")]
public class MovesSO : ScriptableObject
{
    public string moveName;
    public int damage;
    public int energyCost;

    public string moveDescription;
    public MoveType moveType;

    public float moveDuration;
    
    public Sprite moveIcon;
    
    public GameObject moveGameObject;
}
