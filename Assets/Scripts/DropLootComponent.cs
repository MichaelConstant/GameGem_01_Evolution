using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLootComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("get");
    }
}
