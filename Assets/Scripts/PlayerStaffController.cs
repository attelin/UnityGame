using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaffController : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _tip;

    [SerializeField] private float _fireRate = 5f;
    [SerializeField] private float _secondaryFireRate = 1.5f;

    [SerializeField] private float _spreadAngle = 15f;

    private float _nextFireTime;
    private float _nextSecondaryFireTime;

    private Vector2 _mousePosition;
    private Vector2 _lookDirection;

    void Update()
    {
        SetMousePosition();
        SetLookDirection();
        RotateStaff();

        if (Input.GetButton("Fire1") && Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + 1f / _fireRate;
            Shoot();
        }
        if (Input.GetButtonDown("Fire2") && Time.time >= _nextSecondaryFireTime)
        {
            _nextSecondaryFireTime = Time.time + 1f / _secondaryFireRate;
            ShotgunAttack();
        }
    }

    private void RotateStaff()
    {
        float angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Shoot()
    {
        Projectile newProjectile = Instantiate(_projectile, _tip.position, _tip.rotation);
        newProjectile.InitializeProjectile(_lookDirection);
    }

    private void ShotgunAttack()
    {
        FireProjectile(_lookDirection);

        Vector2 leftDirection = Quaternion.Euler(0, 0, -_spreadAngle) * _lookDirection;
        FireProjectile(leftDirection);

        Vector2 rightDirection = Quaternion.Euler(0, 0, _spreadAngle) * _lookDirection;
        FireProjectile(rightDirection);
    }

    private void FireProjectile(Vector2 direction)
    {
        Projectile newProjectile = Instantiate(_projectile, _tip.position, Quaternion.identity);
        newProjectile.InitializeProjectile(direction);
    }

    private void SetMousePosition()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void SetLookDirection()
    {
        _lookDirection = (_mousePosition - (Vector2)transform.position).normalized;
    }
}