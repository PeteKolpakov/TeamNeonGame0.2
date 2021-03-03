using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.EntityClass;
public class LaserBeamTrigger : MonoBehaviour
{
    public int BeamDamage = 1;
    private void OnTriggerStay2D(Collider2D other) {
        if(other.TryGetComponent(out PlayerBase health)){
            health.TakeDamage(BeamDamage,DamageType.Bullet);
        }
    }
}
