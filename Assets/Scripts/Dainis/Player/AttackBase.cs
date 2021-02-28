using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class AttackBase : MonoBehaviour
{
    public Item _weapon;
    public MeleeWeapon _meleeWeapon;

    public Item CurrentWeapon { get => _weapon; }
    public MeleeWeapon CurrentMeleeWeapon { get => _meleeWeapon; }

    protected virtual void Update()
    {
        Aim();
        Shoot();
    }
    
    protected abstract void Aim();
    protected abstract void Shoot();
}
