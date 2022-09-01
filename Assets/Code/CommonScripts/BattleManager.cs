using System.Collections.Generic;
using Code.CharacterScripts;
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
    public MMFeedbacks PlayerTurnFeedback, EnemyTurnFeedback, BattleStartFeedback;
    private Player _playerOnTheField;
    public Transform playerPosition;
    public Transform[] enemyPositions;
    
    [SerializeField] private List<Enemy> _enemiesOnTheField = new List<Enemy>();

    public bool ambush;

    private void Start()    
    {
        Instance = this;
        StartBattle();
    }
    
    public void StartBattle()
    {
        BattleStartFeedback?.PlayFeedbacks();
        
        _playerOnTheField = GameManager.Instance.playerCharacter;
        _playerOnTheField.gameObject.SetActive(true);
        _playerOnTheField.transform.position = playerPosition.position;

        for (int i = 0; i < GameManager.Instance.EnemiesToSpawn.Count; i++)
        {
            var enemy = Instantiate(GameManager.Instance.EnemiesToSpawn[i], enemyPositions[i].position,
                Quaternion.identity);
            
            enemy.returnPosition = enemyPositions[i];
            _enemiesOnTheField.Add(enemy.GetComponent<Enemy>());
        }
        
        turnNumber = 0;
    }

    //Switches the Player turn to the enemy's and otherwise
    public void SwitchTurn()
    {
        if (turn == Turn.PLAYER)
        {
            turn = Turn.ENEMY;
            EnemyTurn(0);
        }

        if (turn == Turn.ENEMY)
        {
            turn = Turn.PLAYER;
            PlayerTurn();
            foreach (var enemy in _enemiesOnTheField)
            {
              enemy.ResetAttackingStatus();  
            }
        }
    }
    
    //Makes the next enemy in line attack
    public void NextTurn()
    {
        turnNumber++;
        if (turnNumber < _enemiesOnTheField.Count)
        {
            EnemyTurn(turnNumber);
        }
        else
        {
            SwitchTurn();
            turnNumber = 0;
        }
    }
    
    public void PlayerTurn()
    {
        PlayerTurnFeedback?.PlayFeedbacks();
    }

    public void EnemyTurn(int index)
    {
        var enemy = _enemiesOnTheField[index];
        
        if (!enemy.alreadyAttacked)
        {
            enemy.StartMyTurn();
            EnemyTurnFeedback?.PlayFeedbacks();
        }
        else
        {
            NextTurn();
        }
    }
    
    public void Victory()
    {
        
    }

    public void Defeat()
    {
        
    }
    
}
