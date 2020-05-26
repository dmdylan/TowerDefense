using StateStuff;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StateStuff
{
    public class TowerAttackingState : TowerState
    {
        private Queue<Enemy> possibleTargets;
        private List<Collider> targetColliderList;

        public TowerAttackingState(Structure structure) : base(structure)
        {
        }

        public override void EnterState()
        {
            targetColliderList = Physics.OverlapSphere(structure.transform.position, structure.structureStats.BaseRange, structure.layerMask).ToList();
            possibleTargets = new Queue<Enemy>();
            Debug.Log("TowerAttackingState - EnterState()");
        }

        public override IEnumerator UpdateState()
        {
            while (structure.attackableInRange.Equals(true))
            {
                yield return new WaitForSeconds(structure.structureStats.BaseAttackRate);
                PoolManager.Instance.ReuseObject(structure.structureStats.Projectile, structure.firePoint.position, structure.transform.rotation);
                Debug.Log("TowerAttackingState - UpdateState()");
            }
        }

        public override void ExitState()
        {
            Debug.Log("TowerAttackingState - ExitState()");
        }
    }
}