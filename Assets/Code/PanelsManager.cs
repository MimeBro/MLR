using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    public List<Panel> PanelList = new List<Panel>();
    public static PanelsManager Instance;

    private void Start()
    {
        Instance = this;
    }
    
    //The Panel where the player is currently standing on
    public Panel PlayerPanel()
    {
        foreach (var panel in PanelList)
        {
            if (panel.occupier != null)
            {
                if (panel.occupier.transform.GetComponent<PlayerController>())
                {
                    return panel;
                }
            }
        }
        
        return null;
    }
}
