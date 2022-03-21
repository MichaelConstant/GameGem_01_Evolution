using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        transform.position =
            new Vector3(_player.transform.position.x, _player.transform.position.y, transform.position.z);
    }
}