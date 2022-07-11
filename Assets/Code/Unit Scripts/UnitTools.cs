using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// Functions commonly used by most units
/// </summary>
public static class UnitTools
{
    //Check if a panel is there or if it's already occupied by another unit
    public static bool PanelIsOk(Panel panel, Sides side)
    {
        if (panel == null) return false;
        if (panel.occupier != null) return false;
        return panel.side == side || panel.side == Sides.NONE;
    }

    //Return a panel based on raycast 
    public static Panel GetPanel(Vector3 origin, Vector3 direction, float distance, Sides side)
    {
        RaycastHit2D ray = Physics2D.Raycast(origin,
            direction, distance, LayerMask.GetMask("Panels"));
        var rayResult = ray.collider != null ? ray.collider.GetComponent<Panel>() : null;

        Debug.DrawRay(origin, direction * distance,
            PanelIsOk(rayResult,side) ? Color.green : Color.red);
        
        return rayResult;
    }

    //Return a list of panels using an unit's position as a reference
    public static List<Panel> GetPanels(Unit unit, int amount, AttackDirection direction)
    {
        var selectedPanels = new List<Panel>();
        var panelIndex = PanelsManager.Instance.PanelList.IndexOf(unit.currentPanel);
        var lastPanel = PanelsManager.Instance.PanelList.Count;
        var frontPanel = panelIndex + 1;
        var backPanel = panelIndex - 1;
        
        switch (direction)
        {
            case AttackDirection.Forward:
                if (panelIndex + amount < lastPanel)
                {
                    for (int i = 0; i < amount; i++)
                    {
                        selectedPanels.Add(PanelsManager.Instance.PanelList[frontPanel + i]);
                    }
                }
                else
                {
                    for (int i = 0; i < (PanelsManager.Instance.PanelList.Count - frontPanel); i++)
                    {
                        selectedPanels.Add(PanelsManager.Instance.PanelList[frontPanel + i]);
                    }
                }
                break;
            
            case AttackDirection.Backward:
                if (panelIndex - amount >= 0)
                {
                    for (int i = 0; i < amount; i++)
                    {
                        selectedPanels.Add(PanelsManager.Instance.PanelList[backPanel - i]);
                    }
                }
                else
                {
                    for (int i = 0; i <  panelIndex; i++)
                    {
                        selectedPanels.Add(PanelsManager.Instance.PanelList[backPanel - i]);
                    }
                }
                break;
            case AttackDirection.Both:
                if (panelIndex - amount >= 0)
                {
                    for (int i = 0; i < amount; i++)
                    {
                        selectedPanels.Insert(0,PanelsManager.Instance.PanelList[backPanel - i]);
                    }
                }
                else
                {
                    for (int i = 0; i <  panelIndex; i++)
                    {
                        selectedPanels.Insert(0,PanelsManager.Instance.PanelList[backPanel - i]);
                    }
                }
                
                if (panelIndex + amount < lastPanel)
                {
                    for (int i = 0; i < amount; i++)
                    {
                        selectedPanels.Add(PanelsManager.Instance.PanelList[frontPanel + i]);
                    }
                }
                else
                {
                    for (int i = 0; i < (PanelsManager.Instance.PanelList.Count - frontPanel); i++)
                    {
                        selectedPanels.Add(PanelsManager.Instance.PanelList[frontPanel + i]);
                    }
                }
                break;
        }
        
        return selectedPanels;
    }

}
