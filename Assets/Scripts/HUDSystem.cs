using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class HUDSystem : Singleton<HUDSystem>
{
    public Image Hp;
    public Image Exp;
    public Text Level;
    public GameObject Lose;
    
    private PlayerComponent _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerComponent>();
    }

    private void Update()
    {
        SetHpBar();
        SetExpBar();
        SetLevelText();
    }

    public void SetHpBar()
    {
        Hp.fillAmount = _player.PlayerLevel switch
        {
            PlayerLevel.Level1 => 1 - (float)_player.HealthPoints / 5,
            PlayerLevel.Level2 => 1 - (float)_player.HealthPoints / 10,
            PlayerLevel.Level3 => 1 - (float)_player.HealthPoints / 15,
            PlayerLevel.Level4 => 1 - (float)_player.HealthPoints / 20,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void SetExpBar()
    {
        Exp.fillAmount = _player.PlayerLevel switch
        {
            PlayerLevel.Level1 => 1 - (float)_player.PlayerExp / 10,
            PlayerLevel.Level2 => 1 - (float)_player.HealthPoints / 30,
            PlayerLevel.Level3 => 1- (float) _player.HealthPoints / 60,
            PlayerLevel.Level4 => 1,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void SetLevelText()
    {
        Level.text = _player.PlayerLevel switch
        {
            PlayerLevel.Level1 => "LV 1",
            PlayerLevel.Level2 => "LV 2",
            PlayerLevel.Level3 => "LV 3",
            PlayerLevel.Level4 => "LV 4",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void LoseGame()
    {
        Lose.SetActive(true);
        Time.timeScale = 0;
    }
}
