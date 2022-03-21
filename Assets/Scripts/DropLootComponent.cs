using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DropLootComponent : MonoBehaviour
{
    public int Exp = 0;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerComponent>() == null) return;

        var playerComponent = col.GetComponent<PlayerComponent>();

        playerComponent.PlayerExp += Exp;

        switch (playerComponent.PlayerLevel)
        {
            case PlayerLevel.Level1:
                if (playerComponent.PlayerExp >= 10)
                {
                    playerComponent.PlayerLevel = PlayerLevel.Level2;
                    playerComponent.Evolution.Play();
                    playerComponent.PlayerExp -= 10;
                    playerComponent.HealthPoints = 10;
                    playerComponent.SetPlayerState(1);
                }
                else
                {
                    playerComponent.PickUp.Play();
                }
                break;
            case PlayerLevel.Level2:
                if (playerComponent.PlayerExp >= 30)
                {
                    playerComponent.PlayerLevel = PlayerLevel.Level3;
                    playerComponent.Evolution.Play();
                    playerComponent.PlayerExp -= 30;
                    playerComponent.HealthPoints = 15;
                    playerComponent.SetPlayerState(2);
                }
                else
                {
                    playerComponent.PickUp.Play();
                }
                break;
            case PlayerLevel.Level3:
                if (playerComponent.PlayerExp >= 60)
                {
                    playerComponent.PlayerLevel = PlayerLevel.Level4;
                    playerComponent.Evolution.Play();
                    playerComponent.PlayerExp -= 60;
                    playerComponent.HealthPoints = 20;
                    playerComponent.SetPlayerState(3);
                }
                else
                {
                    playerComponent.PickUp.Play();
                }
                break;
            case PlayerLevel.Level4:
                playerComponent.PickUp.Play();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }


        gameObject.SetActive(false);
    }
}