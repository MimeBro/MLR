using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    private bool _blinking = false;
    public MoveSet playerMoveSet;
    public static GameManager Instance;
    public MoveSlotsManager MoveSlotsManager;
    public bool controlsStopped;
    private void Start()
    {
        Instance = this;
    }

    public void AddMoveToSet(MovesSO movesSo)
    {
        playerMoveSet.AddMove(movesSo);
    }

    public void StopPlayerControls()
    {
        PlayerController.Instance.CloseBuffer();
        MoveSlotsManager.DisableAllSlots();
    }

    public void StopPlayerControls(float duration)
    {
        PlayerController.Instance.CloseBuffer(duration);
        MoveSlotsManager.DisableAllSlots(duration);
    }

    public void ResumePlayerControls()
    {
        PlayerController.Instance.OpenBuffer();
        MoveSlotsManager.EnableallSlots();
    }
}
