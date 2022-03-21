using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum PlayerLevel
{
    Level1,
    Level2,
    Level3,
    Level4,
}

public class PlayerComponent : BaseUnit
{
    [Header("Player Setting")]
    public GameObject BulletPrefab;

    public KeyCode ShootKey;
    public MouseButton ShootMouseButton;
    
    private Vector3 _playerMoveInput;

    private Camera _mainCamera;

    public AudioSource ShootingSFX_level1;
    public AudioSource ShootingSFX_level2;
    public AudioSource ShootingSFX_level3;
    public AudioSource ShootingSFX_level4;
    public AudioSource Die;
    public AudioSource PickUp;
    public AudioSource Evolution;

    private void Start()
    {
        _mainCamera = Camera.main;
        isPlayer = true;
    }

    private void Update()
    {
        if(!PlayerInputSystem.Instance.CanPlayerInput) return;
        
        var rightInput = Input.GetAxis("Horizontal");
        var upInput = Input.GetAxis("Vertical");

        _playerMoveInput = rightInput * Vector3.right + upInput * Vector3.up;

        if (Input.GetKeyDown(ShootKey) || Input.GetMouseButtonDown((int)ShootMouseButton))
        {
            ShootBullet(BulletPrefab);
            ShootingSFX_level1.Play();
        }

        var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RotateTo(mousePosition);

        Move(_playerMoveInput);
    }
}
