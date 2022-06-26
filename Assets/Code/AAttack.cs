using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;using UnityEngine.Rendering.Universal.Internal;

public enum AttackDirection{Forward, Backward, Both}
public enum TypeOfAttack{SimpleProjectile, GuidedProjectile, ArchedProjectile }
public class AAttack : MonoBehaviour
{
    public TypeOfAttack TypeOfAttack;
    public AttackDirection AttackDirection;

    [Range(1,5)] public int howManyPanelsInFront;
    public bool hitAllPanelsInTheWay;

    [Header("Guided Projectile")]
    public GuidedProjectile guidedProjectile;
    public float guidedProjectileSpeed;
    public float startDelay;
    public float gapBetweenSpawn;
    
    public List<Panel> panels;
    public Transform[] shootPositions;
    
    private int guidedProjectileDamage;
    private Sides guidedProjectileside;

    public async void CastAttack()
    {
        var end = Time.time + startDelay;
        while (Time.time < end)
        {
            await Task.Yield();
        }
        
        switch (TypeOfAttack)
        {
          case  TypeOfAttack.GuidedProjectile:
              GuidedProjectile();
              break;
        }
    }

    public async void GuidedProjectile()
    {
        GetPanels();
        
        if (hitAllPanelsInTheWay)
        {
            for (int i = 0; i < panels.Count; i++)
            {
                await InstantiateGuidedProjectile(i, 0, gapBetweenSpawn);
            }
            return;
        }

        await InstantiateGuidedProjectile(panels.Count - 1, 0,gapBetweenSpawn);
        
        if (AttackDirection == AttackDirection.Both)
        {
            await InstantiateGuidedProjectile(0, 0, 0);
        }
    }

    public async Task InstantiateGuidedProjectile(int panelIndex,int ShootPositionIndex,float duration)
    {
        var end = Time.time + duration;
        var gp = Instantiate(guidedProjectile, shootPositions[ShootPositionIndex].position, Quaternion.identity);
        gp.target = panels[panelIndex].transform;
        gp.speed = guidedProjectileSpeed;
        gp.damage = guidedProjectileDamage;
        gp.side = guidedProjectileside;
        while (Time.time < end)
        {
            await Task.Yield();
        }
    }

    public void ArchedProjectile()
    {
        
    }

    public void GetPanels()
    {
        panels.Clear();
        var playerPanelIndex = GameManager.Instance.PanelList.IndexOf(PlayerController.Instance.unit.currentPanel);
        var playerpanelF = playerPanelIndex + 1;
        var playerpanelB = playerPanelIndex - 1;
        var lastPanel = GameManager.Instance.PanelList.Count;
        
        
        switch (AttackDirection)
        {
            case AttackDirection.Forward:
                if (playerPanelIndex + howManyPanelsInFront < lastPanel)
                {
                    for (int i = 0; i < howManyPanelsInFront; i++)
                    {
                        panels.Add(GameManager.Instance.PanelList[playerpanelF + i]);
                    }
                }
                else
                {
                    for (int i = 0; i < (GameManager.Instance.PanelList.Count - playerpanelF); i++)
                    {
                        panels.Add(GameManager.Instance.PanelList[playerpanelF + i]);
                    }
                }
                break;
            
            case AttackDirection.Backward:
                if (playerPanelIndex - howManyPanelsInFront >= 0)
                {
                    for (int i = 0; i < howManyPanelsInFront; i++)
                    {
                        panels.Add(GameManager.Instance.PanelList[playerpanelB - i]);
                    }
                }
                else
                {
                    for (int i = 0; i <  playerPanelIndex; i++)
                    {
                        panels.Add(GameManager.Instance.PanelList[playerpanelB - i]);
                    }
                }
                break;
            case AttackDirection.Both:
                if (playerPanelIndex - howManyPanelsInFront >= 0)
                {
                    for (int i = 0; i < howManyPanelsInFront; i++)
                    {
                        panels.Insert(0,GameManager.Instance.PanelList[playerpanelB - i]);
                    }
                }
                else
                {
                    for (int i = 0; i <  playerPanelIndex; i++)
                    {
                        panels.Insert(0,GameManager.Instance.PanelList[playerpanelB - i]);
                    }
                }
                
                if (playerPanelIndex + howManyPanelsInFront < lastPanel)
                {
                    for (int i = 0; i < howManyPanelsInFront; i++)
                    {
                        panels.Add(GameManager.Instance.PanelList[playerpanelF + i]);
                    }
                }
                else
                {
                    for (int i = 0; i < (GameManager.Instance.PanelList.Count - playerpanelF); i++)
                    {
                        panels.Add(GameManager.Instance.PanelList[playerpanelF + i]);
                    }
                }
                break;
        }
    }
}
