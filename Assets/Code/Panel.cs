using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel : MonoBehaviour
{
    public int number = 0;
    public Sides side;
    public PanelStatus panelStatus;
    public Color topcolor, botColor;
    private Color _startingColor, _innerPanelStartingColor;

    public Unit occupier;
    //public Image innerPanel;

    private Image _img;


    private void Awake()
    {
        _img = GetComponent<Image>();
    }

    private void Start()
    {
        //ChangeTeamColor();
        //_startingColor = _img.color;
        //_innerPanelStartingColor = innerPanel.color;
    }

    public void ChangeStatus(PanelStatus status)
    {
        panelStatus = status;
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Unit>() != null)
        {
            occupier = other.GetComponent<Unit>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Unit>() != null)
        {
            occupier = null;
        }
    }

   /* private void ChangeTeamColor()
    {
        if (side == Sides.ENEMY)
        {
            _img.color = topcolor;
        }      
        
        if (side == Sides.PLAYER)
        {
            _img.color = botColor;
        }
    }

    public void SwitchSide(Sides newSide)
    {
        side = newSide;
        ChangeTeamColor();
    }

    public void SwitchColor(Color newColor)
    {
        _img.color = newColor;
        //innerPanel.color = new Color(newColor.r, newColor.g, newColor.b, 0.5f);
    }

    public void ResetColor()
    {
        _img.color = _startingColor;
        //innerPanel.color = _innerPanelStartingColor;
    }

    public void PanelSelected()
    {
    }

    #region OnHitOverloads
    public void Hit(float damage)
    {
        HitOccupier(damage);
    }

    void HitOccupier(float dmg)
    {
        if (occupier == null) return;
        occupier.TakeDamage((int)dmg);
    }
    #endregion*/
}