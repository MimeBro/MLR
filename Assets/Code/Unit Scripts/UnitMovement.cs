using System;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;

public class UnitMovement : MonoBehaviour
{
    public Transform playerSprite;
      
    [Title("Panel Selection")]
    public Vector3 rayOrigin;
    
    public float rayDistance;
    public float yposition;
    
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
    
    private Panel frontPanel,backPanel; 
    private OldUnit _oldUnit;
    private Animator animator;

    private void Awake()
    {
        _oldUnit = GetComponent<OldUnit>();
        animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        yposition = _oldUnit.yposition;
        frontPanel = UnitTools.GetPanel(transform.position + rayOrigin, Vector3.right, 
            rayDistance,_oldUnit.side);
        backPanel = UnitTools.GetPanel(transform.position + rayOrigin, -Vector3.right, 
            rayDistance,_oldUnit.side);
    }

    public void StartDodge()
    {
        _oldUnit.uState = UnitState.DODGING;
    }

    public void EndDodge()
    {
        _oldUnit.uState = UnitState.STANDING;
    }

    public void MoveForward()
    {
        if(!UnitTools.PanelIsOk(frontPanel, _oldUnit.side)) return;
        var panelPos = frontPanel.transform.position;
        var destination = new Vector2(panelPos.x, panelPos.y - yposition);

        transform.DOMove(destination ,movementSpeed).SetEase(movementEase);
        
        DodgeForward?.PlayFeedbacks();
        animator?.SetTrigger("DashForward");
    }

    public void MoveBack()
    {
        if(!UnitTools.PanelIsOk(backPanel, _oldUnit.side)) return;
        var panelPos = backPanel.transform.position;
        var destination = new Vector2(panelPos.x, panelPos.y - yposition);

        transform.DOMove(destination ,movementSpeed).SetEase(movementEase);
        
        DodgeBackward?.PlayFeedbacks();
        animator?.SetTrigger("DashBackward");
    }
    
    public void JumpBack(float duration, float jumpPower)
    {
        if (UnitTools.PanelIsOk(backPanel, _oldUnit.side))
        {
            var panelPos = backPanel.transform.position;
            var destination = new Vector2(panelPos.x, panelPos.y - yposition);
            transform.DOJump(destination, jumpPower, 1, duration).SetEase(Ease.Linear);
        }
        else
        {
            var panelPos = _oldUnit.currentPanel.transform.position;
            var destination = new Vector2(panelPos.x, panelPos.y - yposition);
            transform.DOJump(destination, jumpPower, 1, duration).SetEase(Ease.Linear);
        }
    }
    
    public void UnitSummoned ()
    {
        lastPanel = GetComponent<OldUnit>().currentPanel;
        transform.DOJump(TeamManager.Instance.MonsterSpawnPoint.position, leavingJumpPower,1 , leavingDuration)
            .SetEase(leavingEasing).SetUpdate(true);
        playerLeft = true;
    }

    public void UnitRecalled()
    {
        var panelPos = lastPanel.transform.position;
        var destination = new Vector2(panelPos.x, panelPos.y - yposition);

        transform.DOJump(destination, leavingJumpPower,1 , leavingDuration)
            .SetEase(leavingEasing).SetUpdate(true);
        playerLeft = false;
    }
}