using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackableComponent : MonoBehaviour
{
    public BaseUnit BaseUnit;
    public Guid AttackableGuid;

    private void Start()
    {
        AttackableGuid = BaseUnit.UnitGuid;
    }

    public void ApplyDamage(int damage)
    {
        BaseUnit.ApplyDamage(damage);
    }
}
