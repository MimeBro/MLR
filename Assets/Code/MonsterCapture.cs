using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCapture : MonoBehaviour
{
    public MoveSet playerSet;
    public MonsterController monsterToCapture;

    private void Start()
    {
        playerSet = GameManager.Instance.playerMoveSet;
    }

    public void CaptureMonster()
    {
        playerSet.AddMove(monsterToCapture.Capture());
    }
    
    
}
