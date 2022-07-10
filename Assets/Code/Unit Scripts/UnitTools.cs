using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class UnitTools
{
    public static void KnockedBack(Transform transform)
    {
        
    }
    
    public static bool PanelIsOk(Panel panel, Sides side)
    {
        if (panel == null) return false;
        if (panel.occupier != null) return false;
        return panel.side == side || panel.side == Sides.NONE;
    }

    public static Panel GetPanel(Vector3 origin, Vector3 direction, float distance, Sides side)
    {
        RaycastHit2D ray = Physics2D.Raycast(origin,
            direction, distance, LayerMask.GetMask("Panels"));
        var rayResult = ray.collider != null ? ray.collider.GetComponent<Panel>() : null;

        Debug.DrawRay(origin, direction * distance,
            PanelIsOk(rayResult,side) ? Color.green : Color.red);
        
        return rayResult;
    }
}
