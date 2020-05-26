using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProjectile : MonoBehaviour, IPoolObject
{
    [SerializeField] private float projectileSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * projectileSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<Enemy>())
        {
            collision.collider.gameObject.GetComponent<IDamageable>().TakeDamage(5);
        }
    }

    public void Destroy() => gameObject.SetActive(false);

    public void OnObjectReuse()
    {
        throw new System.NotImplementedException();
    }
}
