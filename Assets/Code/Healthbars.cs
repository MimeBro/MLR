
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public enum HealthorShield{HEALTH, SHIELD}
public class Healthbars : MonoBehaviour
{
    public HealthorShield HealthorShield;
    public MMProgressBar ProgressBar;
    public Unit target;
    public TextMeshProUGUI hpNumber;

    private void Update()
    {
        //hpNumber.text = target.hp.ToString();
        ProgressBar.UpdateBar(target.hp, 0 , target.maxhp);
    }
}
