using System;
using System.Collections;
using System.Collections.Generic;
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
    
    public GameObject BulletPrefab;

    public float ShootPlayerCoolDown = 2f;

    private float _timer;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
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
}