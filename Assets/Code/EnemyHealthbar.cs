
using MoreMountains.Tools;
using TMPro;
using UnityEngine;

public enum HealthorShield{HEALTH, SHIELD}
public class EnemyHealthbar : MonoBehaviour
{
    public HealthorShield HealthorShield;
    public MMProgressBar ProgressBar;
    public Unit target;
    public TextMeshProUGUI hpNumber;

    private void Update()
    {

        hpNumber.text = target.hp.ToString();
        if (HealthorShield == HealthorShield.SHIELD)
        {
            ProgressBar.UpdateBar(target.shield, 0, target.maxShield);
        }
    }
}
