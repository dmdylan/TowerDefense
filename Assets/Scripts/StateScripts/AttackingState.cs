using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace StateStuff
{
    class AttackingState : State
    {
        private List<Collider> possibleTargets;
        private List<IDamageable> targetsToAttack;

        public AttackingState(Enemy enemy) : base(enemy)
        {
        }

        public override void EnterState()
        {
            possibleTargets = Physics.OverlapSphere(enemy.transform.position, enemy.baseEnemyStats.AttackRange, enemy.layerMask).ToList();
            targetsToAttack = new List<IDamageable>();
            AddTargetsToTargetList();
        }

        public override IEnumerator UpdateState()
        {
            while (enemy.attackableInRange.Equals(true)) 
            { 
                yield return new WaitForSeconds(enemy.baseEnemyStats.AttackRate);
                FaceTheTarget();
                targetsToAttack.First().TakeDamage(enemy.baseEnemyStats.Damage);
                RemoveDeadTargetsAndCheckForNewTargets();
            }
        }

        public override void ExitState()
        {

        }

        private void AddTargetsToTargetList()
        {
            foreach (Collider target in possibleTargets)
            {
                IDamageable targetScript = target.GetComponent<IDamageable>();
                targetsToAttack.Add(targetScript);
            }
        }

        private void RemoveDeadTargetsAndCheckForNewTargets()
        {
            var newTargets = Physics.OverlapSphere(enemy.transform.position, enemy.baseEnemyStats.AttackRange, enemy.layerMask).ToList();

            if (targetsToAttack.First().Equals(null))
            {
                targetsToAttack.RemoveAt(0);
                //targetsToAttack.RemoveAll(null);
            }

            if (possibleTargets.First().Equals(null))
            {
                possibleTargets.RemoveAt(0);
                //possibleTargets.RemoveAll(null);
            }

            foreach (Collider collider in newTargets)
            {
                if (!possibleTargets.Contains(collider))
                {
                    possibleTargets.Add(collider);
                    targetsToAttack.Add(collider.GetComponent<IDamageable>());
                }
            }
        }

        //make separate method to return transform of current target and then use this to face it?
        //Lerp to face is slowly
        private void FaceTheTarget()
        {
            if (possibleTargets.Count.Equals(0))
                return;

            if (possibleTargets.First().Equals(null))
                return;

            var targetTransform = possibleTargets.First().gameObject.GetComponent<Transform>();

            enemy.transform.LookAt(targetTransform);
        }
    }
}
