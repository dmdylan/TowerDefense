using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace StateStuff
{
    class AttackingState : State
    {
        private List<Collider> attackTargets;
        private List<IDamageable> attackTargetsTwo;

        public AttackingState(NPCBehavior npcBehavior) : base(npcBehavior)
        {
            EnterState();
        }

        public override void EnterState()
        {
            attackTargets = Physics.OverlapSphere(npcBehavior.transform.position, npcBehavior.baseEnemyStats.AttackRange, npcBehavior.layerMask).ToList();
            foreach(Collider target in attackTargets)
            {
                IDamageable targetScript = target.GetComponent<IDamageable>();
                //Throws null reference exception
                attackTargetsTwo.Add(targetScript);
            }
        }

        public override IEnumerator UpdateState()
        {
            while (npcBehavior.attackableInRange.Equals(true)) 
            { 
                yield return new WaitForSeconds(npcBehavior.baseEnemyStats.AttackRate);
                Debug.Log("Attacking");
                foreach(IDamageable damageable in attackTargetsTwo)
                {
                    damageable.TakeDamage(npcBehavior.baseEnemyStats.Damage);
                    //damageable.GetComponent<IDamageable>().TakeDamage(npcBehavior.baseEnemyStats.Damage);
                }
            }
        }

        public override void ExitState()
        {

        }
    }
}
