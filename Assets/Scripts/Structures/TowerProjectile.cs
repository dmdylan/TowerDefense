using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour, IPoolObject
{
    //TODO : Figure out better way to have the tower projectile linked to the tower.
    //Right now can't detect changes in tower, like a buff, all based on the base stats SO.
    [SerializeField] private StructureStats tower = null;
    private Vector3 targetPosition = Vector3.zero;
    public Structure projectileStructure;

    private void OnEnable()
    {
        //projectileStructure = null;
        //targetPosition = ProjectileStructure.Target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;
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
        targetPosition = projectileStructure.Target.position;       
    }
}
