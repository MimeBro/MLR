using System;
using System.Collections;
using System.Collections.Generic;
using Code.CommonScripts;
using Code.MonsterScripts;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

public enum Turn{PLAYER, ENEMY}
public class BattleManager : MonoBehaviour
{
    public Turn turn;
    public int turnNumber;

    public static BattleManager Instance;
    
    [Title("Feedbacks")]
    public MMFeedbacks PlayerTurnFeedback, EnemyTurnFeedback;
    private Monster _enemyOnTheField, _playerOnTheField;

    public Transform playerPosition;
    public Transform enemyPosition;

    private void Start()    
    {
        Instance = this;
        StartBattle();
    }
    
    public void StartBattle()
    {
        _playerOnTheField = GameManager.Instance.PlayersTeam[0];
        _enemyOnTheField = GameManager.Instance.EnemyTeam[0];
        
        _playerOnTheField.gameObject.SetActive(true);
        _enemyOnTheField.gameObject.SetActive(true);

        _playerOnTheField.transform.position = playerPosition.position;
        _enemyOnTheField.transform.position = enemyPosition.position;
        
        if (_playerOnTheField.speed >= _enemyOnTheField.speed)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }

        turnNumber = 1;
    }

    public void NextTurn()
    {
        if (turn == Turn.PLAYER)
        {
            turn = Turn.ENEMY;
            EnemyTurn();
        }

        if (turn == Turn.ENEMY)
        {
            turn = Turn.PLAYER;
            PlayerTurn();
        }

        turnNumber++;
    }
    
    public void PlayerTurn()
    {
        _enemyOnTheField?.EndTurn();
        _playerOnTheField?.StartTurn();
        PlayerTurnFeedback?.PlayFeedbacks();
    }

    public void EnemyTurn()
    {
        _playerOnTheField?.EndTurn();
        _enemyOnTheField?.EndTurn();
        EnemyTurnFeedback?.PlayFeedbacks();
    }

    public void Victory()
    {
        
    }

    public void Defeat()
    {
        
    }
    
}
