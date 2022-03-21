using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public enum EnemyLevel
{
    Level1,
    Level2,
    Level3,
    Level4,
}

public class BaseEnemy : BaseUnit
{
    [Header("Base Enemy Setting")]
    public EnemyLevel EnemyLevel;

    public GameObject DropLoot;
    
    public GameObject BulletPrefab;

    public float ShootPlayerCoolDown = 2f;

    private float _timer;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        BulletPrefab.GetComponent<BulletComponent>().Damage = EnemyLevel switch
        {
            EnemyLevel.Level1 => 2,
            EnemyLevel.Level2 => 4,
            EnemyLevel.Level3 => 6,
            EnemyLevel.Level4 => 8,
            _ => throw new ArgumentOutOfRangeException()
        };

        HealthPoints = EnemyLevel switch
        {
            EnemyLevel.Level1 => 3,
            EnemyLevel.Level2 => 9,
            EnemyLevel.Level3 => 27,
            EnemyLevel.Level4 => 81,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private void Update()
    {
        if (!_player) return;

        RotateTo(_player.transform.position);
        
        _timer += Time.deltaTime;

        if (_timer < ShootPlayerCoolDown) return;

        ShootBullet(BulletPrefab);

        _timer = 0;
    }

    public void Dying()
    {
        var dropLoot = Instantiate(DropLoot, transform.position, Quaternion.identity);

        dropLoot.transform.SetParent(null);
        
        dropLoot.GetComponent<DropLootComponent>().Exp = EnemyLevel switch
        {
            EnemyLevel.Level1 => 3,
            EnemyLevel.Level2 => 6,
            EnemyLevel.Level3 => 9,
            EnemyLevel.Level4 => 12,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        gameObject.SetActive(false);
    }
}