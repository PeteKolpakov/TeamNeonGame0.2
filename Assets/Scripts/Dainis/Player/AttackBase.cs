using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class AttackBase : MonoBehaviour
{
<<<<<<< Updated upstream:Assets/Scripts/Dainis/Player/AttackBase.cs
    public Item _weapon;
    public MeleeWeapon _meleeWeapon;

    public Item CurrentWeapon { get => _weapon; }
    public MeleeWeapon CurrentMeleeWeapon { get => _meleeWeapon; }
=======
    [SerializeField]
    protected Item _weapon;
>>>>>>> Stashed changes:Assets/Scripts/UI/AttackBase.cs

    protected virtual void Update()
    {
        Aim();
        Shoot();
    }
    
    protected abstract void Aim();
    protected abstract void Shoot();
}
