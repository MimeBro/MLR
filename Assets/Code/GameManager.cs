using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    private bool _blinking = false;
    public List<Panel> PanelList = new List<Panel>();
    public static GameManager Instance;

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
