using System.Collections.Generic;
using Cinemachine;
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

    public List<Enemy> enemiesOnTheField = new List<Enemy>();

    [Title("Camera Positioning")]
    public RectTransform crosshair;

    public CinemachineVirtualCamera waitingCam, attackingCam, dodgingCam, aimingCam;

    #endregion

    private void Start()    
    {
        Instance = this;
        StartBattle();
    }
    
    public void StartBattle()
    {
        BattleStartFeedback?.PlayFeedbacks();
        crosshair.gameObject.SetActive(false);
        
        _playerOnTheField = GameManager.Instance.playerCharacter;
        _playerOnTheField.gameObject.SetActive(true);
        _playerOnTheField.transform.position = playerPosition.position;
        _playerOnTheField.returnPosition = playerPosition;

        for (int i = 0; i < GameManager.Instance.EnemiesToSpawn.Count; i++)
        {
            var enemy = Instantiate(GameManager.Instance.EnemiesToSpawn[i], enemyPositions[i].position,
                Quaternion.identity);
            
            enemy.returnPosition = enemyPositions[i];
            enemiesOnTheField.Add(enemy.GetComponent<Enemy>());
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
        //Switch to Enemy turn
        if (turn == Turn.PLAYER)
        {
            CameraDodgingPosition();
            _playerOnTheField.EndMyTurn();
            turn = Turn.ENEMY;
            EnemyTurn(0);
        }
        //Switch to Player turn
        else
        {
            CameraWaitingPosition();
            turn = Turn.PLAYER;
            PlayerTurn();
            foreach (var enemy in enemiesOnTheField)
            {
              enemy.ResetAttackingStatus();  
            }
        }
    }
    
    //Makes the next enemy in line attack
    public void NextTurn()
    {
        _enemyTurn++;
        if (_enemyTurn < enemiesOnTheField.Count)
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
        var enemy = enemiesOnTheField[index];
        
        if (!enemy.alreadyAttacked)
        {
            enemy.StartTurn();
        }
        else
        {
            NextTurn();
        }
    }

    #region Public Getters

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
        return enemiesOnTheField[0];
    }

    public Enemy GetEnemy(int index)
    {
        return enemiesOnTheField[index];
    }
    
    #endregion

    #region Cameras

    public void CameraWaitingPosition()
    {
        attackingCam.gameObject.SetActive(false);
        dodgingCam.gameObject.SetActive(false);
        aimingCam.gameObject.SetActive(false);
        waitingCam.gameObject.SetActive(true);
    }
    
    public void CameraAttackingPosition()
    {
        attackingCam.gameObject.SetActive(true);
        dodgingCam.gameObject.SetActive(false);
        aimingCam.gameObject.SetActive(false);
        waitingCam.gameObject.SetActive(false);
    }
    
    public void CameraDodgingPosition()
    {
        attackingCam.gameObject.SetActive(false);
        dodgingCam.gameObject.SetActive(true);
        aimingCam.gameObject.SetActive(false);
        waitingCam.gameObject.SetActive(false);
    }
    
    public void CameraAimingPosition()
    {
        attackingCam.gameObject.SetActive(false);
        dodgingCam.gameObject.SetActive(false);
        aimingCam.gameObject.SetActive(true);
        waitingCam.gameObject.SetActive(false);
    }
    
    #endregion
    
    public void Victory()
    {
        
    }

    public void Defeat()
    {
        
    }
}
