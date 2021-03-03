using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.EntityClass;

public class OneShotBeam : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        if(other.TryGetComponent(out PlayerBase health)){
            health.TakeDamage(10000,DamageType.Bullet);
        }
    }
}
