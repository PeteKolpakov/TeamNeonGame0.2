using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class AttackBase : MonoBehaviour
{
    [SerializeField]
    protected Item _weapon;

    protected virtual void Update()
    {
        Aim();
        Shoot();
    }

    protected abstract void Aim();
    protected abstract void Shoot();
}
