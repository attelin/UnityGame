using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStaffController : MonoBehaviour
{
    [SerializeField] private float _fireRate;
    private float _nextFireTime;


    void Update()
    {
        RotateStaff();
        if (Input.GetButton("Fire1") && Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + 1f / _fireRate;
            Shoot();
        }
    }


    private void RotateStaff()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDirection = (mousePosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private void Shoot()
    {
        Debug.Log("BANG!");
    }

}