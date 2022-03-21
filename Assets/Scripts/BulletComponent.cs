using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    public float BulletSpeed = 5f;
    private float TimeToDestroy = 1f;
    public int Damage = 1;

    public Guid BulletGuid;

    private void Start()
    {
        Invoke(nameof(DestroySelfGameObject), TimeToDestroy);
    }

    private void Update()
    {
        transform.position += (transform.up * BulletSpeed * Time.deltaTime);
    }

    private void DestroySelfGameObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<AttackableComponent>() == null) return;

        var attackableComponent = col.GetComponent<AttackableComponent>();
        
        if (attackableComponent.AttackableGuid == BulletGuid) return;
        
        attackableComponent.ApplyDamage(Damage);
        Destroy(gameObject);
    }
}