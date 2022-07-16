    using System.Threading.Tasks;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class AreaAttacks : AttackController
    {
        [Title("Area Attack")]  
        public AreaAttack areaAttack;
        public float hitsPerSecond;
        public float areaDuration;

        public override async void CastAttack()
        {
            base.CastAttack();
            var end = Time.time + startDelay;
            while (Time.time < end)await Task.Yield();

            await AreaAttack();
            gameObject.SetActive(false);
        }

        private Task AreaAttack()
        {
            var aA = Instantiate(areaAttack, shootPositions[0].position, Quaternion.identity);
            aA.damagePerHit = baseDamage;
            aA.secondsPerHit = hitsPerSecond;
            aA.attacker = attacker;
            aA.side = attacker.side;
            Destroy(aA.gameObject,areaDuration);
            return Task.CompletedTask;
        }
    }