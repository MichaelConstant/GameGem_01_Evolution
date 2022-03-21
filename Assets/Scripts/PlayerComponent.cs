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

    public PlayerLevel PlayerLevel = PlayerLevel.Level1;
    public int PlayerExp = 0;
    
    private Vector3 _playerMoveInput;

    private Animator _animator;

    private Camera _mainCamera;

    public List<GameObject> PlayerSprite = new List<GameObject>();

    public AudioSource ShootingSFX_level1;
    public AudioSource ShootingSFX_level2;
    public AudioSource ShootingSFX_level3;
    public AudioSource ShootingSFX_level4;
    public AudioSource Die;
    public AudioSource PickUp;
    public AudioSource Evolution;
    private static readonly int Level = Animator.StringToHash("Level");

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
            var shootSfx = PlayerLevel switch
            {
                PlayerLevel.Level1 => ShootingSFX_level1,
                PlayerLevel.Level2 => ShootingSFX_level2,
                PlayerLevel.Level3 => ShootingSFX_level3,
                PlayerLevel.Level4 => ShootingSFX_level4,
                _ => throw new ArgumentOutOfRangeException()
            };
            shootSfx.Play();
        }

        var mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        RotateTo(mousePosition);

        Move(_playerMoveInput);
    }

    public void SetAnimatorState(int level)
    {
        if(!_animator) return;
        _animator.SetInteger(Level, level);
    }

    public void SetPlayerState(int level)
    {
        PlayerSprite[level - 1].SetActive(false);
        PlayerSprite[level].SetActive(true);
        var scale = (2f + (float) level *2f);
    }
}
