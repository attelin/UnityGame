using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EntityHealth _entityHealth;

    private void Awake()
    {
        _entityHealth = GetComponent<EntityHealth>();
    }

    private void Start()
    {
        _entityHealth.OnDeath += DestroyEnemy;
    }

    private void OnDisable()
    { 
        _entityHealth.OnDeath -= DestroyEnemy;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
