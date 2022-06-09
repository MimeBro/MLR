using System;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerSprite;

    public Vector3 rayOrigin;
    
    public float rayDistance;
    public float jumpPower;
    public float yposition;
    
    public LayerMask layerMask;

    public float movementSpeed;
    
    private Panel _frontPanel, _backPanel;
    private Unit _unit;

    private Animator _animator;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        //PlayerController.Instance.playerMovement = this;
    }

    void Update()
    {
        DirectionalRaycasts();
        MovementInput();
    }

    public void MovementInput()
    {
        //Move Forward
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            PlayerController.Instance.AddCommand(MoveForward, movementSpeed);
        }
        
        //Move Backward
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            PlayerController.Instance.AddCommand(MoveBack, movementSpeed);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            //maybe a passive
        }
    }

    public void StartDodge()
    {
        _unit.uState = UnitState.DODGING;
    }

    public void EndDodge()
    {
        _unit.uState = UnitState.STANDING;
    }

    public void MoveForward()
    {
        if(!PanelIsOk(_frontPanel)) return;
        var panelPos = _frontPanel.transform.position;
        var destination = new Vector2(panelPos.x, yposition);

        transform.DOJump(destination, jumpPower, 1 ,movementSpeed);
        _animator.SetTrigger("DashForward");
    }

    public void KnockForward(float duration)
    {
        if (!PanelIsOk(_frontPanel)) return;
        var panelPos = _frontPanel.transform.position;
        var destination = new Vector2(panelPos.x, yposition);

        transform.DOMove(destination, duration);
    }

    public void MoveBack()
    {
        if(!PanelIsOk(_backPanel)) return;
        var panelPos = _backPanel.transform.position;
        var destination = new Vector2(panelPos.x, yposition);

        transform.DOJump(destination, jumpPower, 1 ,movementSpeed);
        _animator.SetTrigger("DashBackward");
    }

    public void KnockBack(float duration)
    {
        if (!PanelIsOk(_backPanel)) return;
        var panelPos = _backPanel.transform.position;
        var destination = new Vector2(panelPos.x, yposition);

        transform.DOMove(destination, duration);
    }

    public void TestJumpAttack()
    {
        _animator.SetTrigger("JumpAttack");
    }

    public void KnockUp(int jumpPower)
    {
        playerSprite.DOLocalJump(Vector2.zero, jumpPower, 1, 0.7f);
    }

    private bool PanelIsOk(Panel p)
    {
        if (p == null) return false;

        if (p.occupier == null) 
        {
            if (p.side == _unit.side || p.side == Sides.NONE)
            {
                return true;
            }
        }

        return false;
    }

    private void DirectionalRaycasts()
    {
        RaycastHit2D forwardRay = Physics2D.Raycast(transform.position + rayOrigin, 
            Vector2.right, rayDistance,layerMask);
        
        if (forwardRay.collider != null)
        {
            _frontPanel = forwardRay.collider.GetComponent<Panel>();
        }
        else
        {
            _frontPanel = null;
        }
        
        RaycastHit2D backRay = Physics2D.Raycast(transform.position + rayOrigin, 
            -Vector2.right, rayDistance,layerMask);
        
        if (backRay.collider != null)
        {
            _backPanel = backRay.collider.GetComponent<Panel>();
        }
        else
        {
            _backPanel = null;
        }
        
    }
}