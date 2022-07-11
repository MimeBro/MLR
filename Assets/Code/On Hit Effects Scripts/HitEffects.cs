using DG.Tweening;
using UnityEngine;

public enum HitEffects
{
    KNOCKUP, //launches the target vertically for 1 second.
    KNOCKBACK,//launches the target to an specified amount of panels back.
    PUSHBACK//Pushes ther target back 1 panel, if there's an obstacle at the destination it will be broken.
}

public static class OnHitEffects
{
    public static void KnockUp(Unit unit)
    {
        var panelPos = unit.lastPanel.transform.position;
        var destination = new Vector2(panelPos.x, panelPos.y -unit.yposition);
        unit.transform.DOJump(destination, 4, 1, 0.75f).SetEase(Ease.Linear);
    }
    
    public static void Knockback(Unit unit, int amount)
    {
        
    }

    public static void Pushback(Unit unit)
    {
        
    }
}

