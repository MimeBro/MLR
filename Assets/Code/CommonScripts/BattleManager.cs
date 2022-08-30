using System.Collections.Generic;
using Code.CommonScripts;
using Code.MonsterScripts;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

public enum Turn {PLAYER, ENEMY}
public class BattleManager : MonoBehaviour
{
    public Turn turn;
    public int turnNumber;

    public static BattleManager Instance;
    
    [Title("Feedbacks")]
    public MMFeedbacks PlayerTurnFeedback, EnemyTurnFeedback;
    private Player _playerOnTheField;
    private List<Enemy> _enemiesOnTheField = new List<Enemy>();
    public Transform playerPosition;
    public Transform[] enemyPositions;

    private void Start()    
    {
        Instance = this;
        StartBattle();
    }
    
    public void StartBattle()
    {
        _playerOnTheField = GameManager.Instance.playerCharacter;
        _playerOnTheField.gameObject.SetActive(true);
        _playerOnTheField.transform.position = playerPosition.position;

        for (int i = 0; i < GameManager.Instance.EnemiesToSpawn.Count; i++)
        {
            var enemy = Instantiate(GameManager.Instance.EnemiesToSpawn[i], enemyPositions[i].position, Quaternion.identity);
            enemy.returnPosition = enemyPositions[0];
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
        _playerOnTheField?.StartTurn();
        PlayerTurnFeedback?.PlayFeedbacks();
    }

    public void EnemyTurn()
    {
        _playerOnTheField?.EndTurn();
        EnemyTurnFeedback?.PlayFeedbacks();
    }

    public void Victory()
    {
        
    }

    public void Defeat()
    {
        
    }
    
}
