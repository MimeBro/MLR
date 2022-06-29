using DG.Tweening;
using UnityEngine;

public class PlayerJumpBack : MonoBehaviour
{
    public float jumpDuration;
    public float jumpPower;
    public Ease ease;

    private void OnEnable()
    {
        JumpBack();
    }

    public void JumpBack()
    {
        var playerMovement = PlayerController.Instance.playerMovement;
        playerMovement.KnockBack(jumpDuration,jumpPower,ease);
    }
}
