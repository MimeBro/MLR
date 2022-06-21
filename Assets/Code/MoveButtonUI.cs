using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MoveButtonUI : MonoBehaviour
{
    public KeyCode assignedButton;
    public MovesSO currentMove;
    public Image buttonIcon;
    public Image buttonCooldownEffect;

    public bool OnCooldown;
    public float _cooldown;
    
    public void SetMove(MovesSO newMove)
    {
        currentMove = newMove;
        SetIcon(currentMove.moveIcon);
    }

    public void SetIcon(Sprite newIcon)
    {
        buttonIcon.sprite = newIcon;
    }

    private void Update()
    {
        GoOnCooldown();

        if (Input.GetKeyDown(assignedButton))
        {
            if(OnCooldown) return;
            if(PlayerController.Instance.commandBuffer.Any()) return;
            PlayerController.Instance.AddCommand(CastMove,0);
        }
    }

    public void CastMove()
    {
        var castedMove = Instantiate(currentMove.moveGameObject, 
            PlayerController.Instance.shootPoint.position, quaternion.identity);
        
        var shots = castedMove.GetComponentsInChildren<Attacks>();
        foreach (var shot in shots)
        {
            shot.SetSide(Sides.PLAYER);
            shot.SetDamage(currentMove.damage);
        }

        _cooldown += currentMove.moveCooldown;
    }

    public void GoOnCooldown()
    {
        if (_cooldown > 0)
        {
            OnCooldown = true;
            _cooldown -= Time.deltaTime;
        }
        else
        {
            _cooldown = 0;
            OnCooldown = false;
        }

        //buttonCooldownEffect.fillAmount = _cooldown / currentMove.moveCooldown;
    }
}
