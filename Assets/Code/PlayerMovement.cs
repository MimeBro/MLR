using System;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerSprite;
    
    [Title("Panel Selection")]
    public Vector3 rayOrigin;
    
    public float rayDistance;
    public float yposition;
    
    public LayerMask layerMask;
    
    [Title("Movement")]
    public float movementSpeed;
    public Ease movementEase;
    
    public float leavingDuration;
    public float leavingJumpPower;
    public Ease leavingEasing;

    [Title("Effects")]
    public MMFeedbacks DodgeForward;
    public MMFeedbacks DodgeBackward;
    
    private Panel lastPanel;
    private bool playerLeft;

    private PlayerController _playerController;
    private Panel _frontPanel, _backPanel;
    private Unit _unit;
    private Animator _animator;

    private void Awake()
    {
        _unit = GetComponent<Unit>();
        _animator = GetComponent<Animator>();
        _playerController = PlayerController.Instance;
    }

    private void Start()
    {
        yposition = _unit.yposition;
        _playerController.playerMovement = this;
        _playerController.animator = _animator;
        _playerController.unit = _unit;
    }

    private void Update()
    {
        DirectionalRaycasts();
        MovementInput();
    }

    public void MovementInput()
    {
        //Move Forward
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            _playerController.AddCommand(MoveForward, movementSpeed);

        }
        
        //Move Backwards
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            _playerController.AddCommand(MoveBack, movementSpeed);
;
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
        var destination = new Vector2(panelPos.x, panelPos.y - yposition);

        transform.DOMove(destination ,movementSpeed).SetEase(movementEase);
        
        DodgeForward?.PlayFeedbacks();
        _animator?.SetTrigger("DashForward");
    }

    public void MoveBack()
    {
        if(!PanelIsOk(_backPanel)) return;
        var panelPos = _backPanel.transform.position;
        var destination = new Vector2(panelPos.x, panelPos.y - yposition);

        transform.DOMove(destination ,movementSpeed).SetEase(movementEase);
        
        DodgeBackward?.PlayFeedbacks();
        _animator?.SetTrigger("DashBackward");
    }

    public void KnockForward(float duration)
    {
        if (!PanelIsOk(_frontPanel)) return;
        var panelPos = _frontPanel.transform.position;
        var destination = new Vector2(panelPos.x, yposition);

        transform.DOMove(destination, duration);
    }

    public void KnockBack(float duration)
    {
        if (!PanelIsOk(_backPanel)) return;
        var panelPos = _backPanel.transform.position;
        var destination = new Vector2(panelPos.x, yposition);

        transform.DOMove(destination, duration);
    }

    public void KnockBack(float duration, float jumpPower, Ease ease)
    {
        if (PanelIsOk(_backPanel))
        {
            var panelPos = _backPanel.transform.position;
            var destination = new Vector2(panelPos.x, yposition);
            transform.DOJump(destination, jumpPower, 1, duration).SetEase(ease);
        }
        else
        {
            var panelPos = _unit.currentPanel.transform.position;
            var destination = new Vector2(panelPos.x, yposition);
            transform.DOJump(destination, jumpPower, 1, duration).SetEase(ease);
        }
    }

    public void KnockUp(int jumpPower)
    {
        playerSprite.DOLocalJump(Vector2.zero, jumpPower, 1, 0.7f);
    }

    public void PlayerLeaves()
    {
        lastPanel = GetComponent<Unit>().currentPanel;
        transform.DOJump(TeamManager.Instance.MonsterSpawnPoint.position, leavingJumpPower,1 , leavingDuration)
            .SetEase(leavingEasing).SetUpdate(true);
        playerLeft = true;
    }

    public void PlayerComesBack()
    {
        var panelPos = lastPanel.transform.position;
        var destination = new Vector2(panelPos.x, panelPos.y - yposition);

        transform.DOJump(destination, leavingJumpPower,1 , leavingDuration)
            .SetEase(leavingEasing).SetUpdate(true);
        playerLeft = false;
    }

    private bool PanelIsOk(Panel p)
    {
        if (p == null) return false;

        if (p.occupier != null) return false;
        return p.side == _unit.side || p.side == Sides.NONE;
    }

    private void DirectionalRaycasts()
    {
        RaycastHit2D forwardRay = Physics2D.Raycast(transform.position + rayOrigin, 
            Vector2.right, rayDistance,layerMask);
        
        _frontPanel = forwardRay.collider != null ? forwardRay.collider.GetComponent<Panel>() : null;
        
        RaycastHit2D backRay = Physics2D.Raycast(transform.position + rayOrigin, 
            -Vector2.right, rayDistance,layerMask);
        
        _backPanel = backRay.collider != null ? backRay.collider.GetComponent<Panel>() : null;
        
        if (PanelIsOk(_frontPanel))
        {
            Debug.DrawRay(transform.position + rayOrigin, Vector3.right * rayDistance, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position + rayOrigin, Vector3.right * rayDistance, Color.red);
        }

        if (PanelIsOk(_backPanel))
        {
            Debug.DrawRay(transform.position + rayOrigin, -Vector3.right * rayDistance, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position + rayOrigin, -Vector3.right * rayDistance, Color.red);
        }
    }
}