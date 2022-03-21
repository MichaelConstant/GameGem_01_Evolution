using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerComponent : BaseUnit
{
    [Header("Player Setting")]
    public GameObject BulletPrefab;

    public KeyCode ShootKey;
    public MouseButton ShootMouseButton;
    
    private Vector3 _playerMoveInput;

    private Camera _mainCamera;
    
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
        }

        var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RotateTo(mousePosition);

        Move(_playerMoveInput);
    }
}
