using UnityEngine;


[CreateAssetMenu(fileName = "Manabar", menuName = "ManaBar", order = 0)]
public class ManaBarSO : ScriptableObject
{
    public float maxMana;
    public float currentMana;
    public float startingMps;
    public float manaPerSecond;
    public float startingMph;
    public float manaPerHit;


    public void AddMana()
    {
        currentMana += manaPerHit;
    }

    public void AddMana(float amount)
    {
        currentMana += amount;
    }

    public void RemoveMana(float amount)
    {
        //Remove Mana
    }


}