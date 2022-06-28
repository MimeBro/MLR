using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpBack : MonoBehaviour
{
    public float moveBackDuration;
    public float jumpDuration;
    public float jumpPower;
    public Ease ease;

    public void JumpBack()
    {
        PlayerMovement playerMovement = PlayerController.Instance.playerMovement;
        Transform playerTransform = PlayerController.Instance.transform;
        playerMovement.KnockBack(jumpDuration,jumpPower,ease);
    }
}
