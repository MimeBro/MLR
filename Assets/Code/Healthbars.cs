using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    public MMProgressBar progressBar;
    public OldUnit target;
    public TextMeshProUGUI hpNumber;
    public TextMeshProUGUI targetName;
    public Image monsterPortrait;
    public Sides side;
    
    private void Update()
    {
        if(target == null) return;
        if (hpNumber != null) hpNumber.text = target.hp + " / " + target.maxhp;
        
        targetName.text = target.stats.monsterName; 
        
        if (monsterPortrait != null)
            if (target.stats.monsterPortrait != null)
                monsterPortrait.sprite = target.stats.monsterPortrait;
        
        progressBar?.UpdateBar(target.hp, 0 , target.maxhp);
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
