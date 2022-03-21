using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

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
    [Header("Base Enemy Setting")] public EnemyLevel EnemyLevel;

    public GameObject DropLoot;

    public GameObject BulletPrefab;

    public float ShootPlayerCoolDown = 2f;

    private float _timer;

    private Vector3 _movePosition;

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

        _movePosition = transform.position;
    }

    private void Update()
    {
        if (!_player) return;

        var randomVectorX = Random.Range(-0.2f, 0.2f);
        var randomVectorY = Random.Range(-0.2f, 0.2f);
        
        _movePosition = new Vector3(randomVectorX, randomVectorY, 0);
        

        if (Vector3.Distance(transform.position, _player.transform.position) >= 8f) return;

        RotateTo(_player.transform.position);

        Move(_movePosition);
        
        _timer += Time.deltaTime;

        if (_timer < ShootPlayerCoolDown) return;

        var randomNum = Random.Range(0, 1);

        if (randomNum == 0)
        {
            ShootBullet(BulletPrefab);
        }
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