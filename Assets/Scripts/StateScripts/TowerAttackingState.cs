using StateStuff;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StateStuff
{
    //TODO: Currently can set the target from the structure property, but do not have a way to reference the projectile
    //so the position cannot be set. Could just have the tower rotate and have the projectile fire forward.
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
        }

        public override IEnumerator UpdateState()
        {
            while (structure.attackableInRange.Equals(true))
            {
                AddAndRemoveEnemies();
                yield return new WaitForSeconds(structure.structureStats.BaseAttackRate);
                SetTowerTarget(possibleTargets);
                PoolManager.Instance.ReuseObject(structure.structureStats.Projectile.gameObject, structure.firePoint.position, structure.firePoint.rotation);
            }
        }

        public override void ExitState()
        {
            targetColliderList.Clear();
            possibleTargets.Clear();
        }

        void AddAndRemoveEnemies()
        {
            //Casts all enemies in a sphere to a list each time the method is called. 
            targetColliderList = Physics.OverlapSphere(structure.transform.position, structure.structureStats.BaseRange, structure.layerMask).ToList();

            //Run through all the targets in the list
            foreach(Collider collider in targetColliderList)
            {
                //Just checks if it is an enemy, maybe not needed
                if(collider.gameObject.TryGetComponent(out Enemy enemy))
                {
                    //If it is not in the Enemy queue, it adds it. Otherwise it removes the enemy.
                    if (!possibleTargets.Contains(enemy))
                        possibleTargets.Enqueue(enemy);
                    //else
                    //    possibleTargets.Dequeue();                        
                }
            }
        }

        void SetTowerTarget(Queue<Enemy> enemies)
        {
            if (possibleTargets.Count.Equals(0))
                return;

            structure.Target = enemies.Peek().transform;
        }
    }
}