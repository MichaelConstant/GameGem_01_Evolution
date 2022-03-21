using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseUnit : MonoBehaviour
{
    [Header("Base Unit")]
    public float MoveSpeed;
    
    public int HealthPoints;

    public Transform ForwardResource;

    public Guid UnitGuid;

    protected bool isPlayer = false;

    private void Awake()
    {
        UnitGuid = Guid.NewGuid();
    }

    protected void Move(Vector3 direction)
    {
        transform.position += (direction * MoveSpeed * Time.deltaTime);
    }

    protected void ShootBullet(GameObject bulletPrefab)
    {
        var bullet = Instantiate(bulletPrefab, ForwardResource.transform);

        bullet.GetComponent<BulletComponent>().BulletGuid = UnitGuid;
        
        bullet.transform.parent = null;
    }

    protected void RotateTo(Vector3 position)
    {
        var aimDirection = (position - transform.position).normalized;
        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle - 90f);
    }

    public void ApplyDamage(int damage)
    {
        HealthPoints -= damage;
        HealthPoints = Mathf.Max(HealthPoints, 0);
        
        if(HealthPoints != 0) return;
        if (isPlayer)
        {
            PlayerInputSystem.Instance.CanPlayerInput = false;
        }
        gameObject.SetActive(false);
    }
}
