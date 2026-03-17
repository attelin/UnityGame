using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float travelSpeed = 10f;
    [SerializeField] private float dmg = 1f;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private GameObject _hitParticlePrefab;

    public void InitializeProjectile(Vector2 direction)
    {
        Launch(direction);
    }

    private void Launch(Vector2 direction)
    {
        Vector2 movement = direction.normalized * travelSpeed;
        rb.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        { 
            DealDamage(collision.gameObject);
            DestroyProjectile();
        }
        if (collision.gameObject.CompareTag("Terrain")) DestroyProjectile();
        
    }

    private void DealDamage(GameObject target)
    {
        if (target.TryGetComponent(out EntityHealth entityHealth))
        {
            entityHealth.LoseHealth(dmg);
        }
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
