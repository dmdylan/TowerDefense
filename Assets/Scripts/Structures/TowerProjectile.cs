using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour, IPoolObject
{
    //TODO : Figure out better way to have the tower projectile linked to the tower.
    //Right now can't detect changes in tower, like a buff, all based on the base stats SO.
    [SerializeField] private StructureStats tower = null;
    private Transform targetPosition = null;
    public Structure projectileStructure = null;

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = (targetPosition.position - transform.position).normalized;
        transform.position += moveDir * tower.ProjectileSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.GetComponent<IDamageable>().TakeDamage(tower.BaseDamage);
            Destroy();
        }
        else
        {
            Destroy();
        }
    }

    public void Destroy() => gameObject.SetActive(false);

    public void OnObjectReuse()
    {
        targetPosition = projectileStructure.Target;
    }
}
