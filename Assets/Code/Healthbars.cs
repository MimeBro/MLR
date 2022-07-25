using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    public MMProgressBar ProgressBar;
    public Unit target;
    public TextMeshProUGUI hpNumber;
    public TextMeshProUGUI targetName;
    public TextMeshProUGUI level;
    public Image monsterPortrait;
    public Sides side;
    
    public void SetTarget()
    {
        target = TeamManager.Instance.GetPlayer();
    }

    private void Update()
    {
        if(target == null) return;
        hpNumber.text = target.hp + " / " + target.maxhp;
        targetName.text = target.stats.monsterName; 
        level.text = "Lvl. " + target.level;
        if (target.stats.monsterProfile != null) monsterPortrait.sprite = target.stats.monsterProfile;
        ProgressBar?.UpdateBar(target.hp, 0 , target.maxhp);
    }

    public void SwitchTarget()
    {
        target = side switch
        {
            Sides.PLAYER => TeamManager.Instance.GetPlayer(),
            Sides.ENEMY => TeamManager.Instance.GetEnemy(),
            _ => target
        };
    }
}
