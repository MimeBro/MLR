using System.Collections.Generic;
using Code.CharacterScripts;
using Code.CommonScripts;
using Code.MonsterScripts;
using MoreMountains.Feedbacks;
using RoboRyanTron.Unite2017.Events;
using Sirenix.OdinInspector;
using UnityEngine;

public enum Turn {PLAYER, ENEMY}
public class BattleManager : MonoBehaviour
{
    #region Variables
    public Turn turn;
    public int turnNumber;
    public bool ambush;
    
    private int _enemyTurn;

    public static BattleManager Instance;
    
    [Title("Feedbacks")]
    public MMFeedbacks PlayerTurnFeedback;
    public MMFeedbacks EnemyTurnFeedback;
    public MMFeedbacks BattleStartFeedback;
    public GameEvent playersTurnEvent;
    
    private Player _playerOnTheField;
    
    [Title("Character Positioning")]
    public Transform playerPosition;
    public Transform[] enemyPositions;
    
    [SerializeField] 
    private List<Enemy> _enemiesOnTheField = new List<Enemy>();

    #endregion

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
        _playerOnTheField.returnPosition = playerPosition;

        for (int i = 0; i < GameManager.Instance.EnemiesToSpawn.Count; i++)
        {
            var enemy = Instantiate(GameManager.Instance.EnemiesToSpawn[i], enemyPositions[i].position,
                Quaternion.identity);
            
            enemy.returnPosition = enemyPositions[i];
            _enemiesOnTheField.Add(enemy.GetComponent<Enemy>());
        }
        
        _enemyTurn = 0;
        turnNumber = 0;
        
        if (ambush)
        {
            turn = Turn.ENEMY;
            EnemyTurn(0);
        }
        else
        {
            turn = Turn.PLAYER;
            PlayerTurn();
        }
    }

    //Switches the Player turn to the enemy's and vice-versa
    public void SwitchTurn()
    {
        if (turn == Turn.PLAYER)
        {
            _playerOnTheField.EndMyTurn();
            turn = Turn.ENEMY;
            EnemyTurn(0);
        }

        else
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
        _enemyTurn++;
        if (_enemyTurn < _enemiesOnTheField.Count)
        {
            EnemyTurn(_enemyTurn);
        }
        else
        {
            SwitchTurn();
            turnNumber++;
            _enemyTurn = 0;
        }
    }
    
    public void PlayerTurn()
    {
        Debug.Log("Player's Turn");
        playersTurnEvent.Raise();
        _playerOnTheField.StartTurn();
    }

    public void EnemyTurn(int index)
    {
        var enemy = _enemiesOnTheField[index];
        
        if (!enemy.alreadyAttacked)
        {
            enemy.StartTurn();
        }
        else
        {
            NextTurn();
        }
    }
    
    //public Getters
    public Player GetPlayer()
    {
        return _playerOnTheField;
    }

    public Vector3 GetPlayerPosition()
    {
        return _playerOnTheField.transform.position;
    }

    public Enemy GetEnemy()
    {
        return _enemiesOnTheField[0];
    }

    public Enemy GetEnemy(int index)
    {
        return _enemiesOnTheField[index];
    }
    
    public void Victory()
    {
        
    }

    public void Defeat()
    {
        
    }
}
