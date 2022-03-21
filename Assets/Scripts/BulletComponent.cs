using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum BulletLevel
{
    Level1,
    Level2,
    Level3,
    Level4,
    Enemy,
}

public class BulletComponent : MonoBehaviour
{
    public BulletLevel BulletLevel;
    
    public float BulletSpeed = 5f;
    public float TimeToDestroy = 5f;
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
    }
}