using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EntityClass
{
    class DestructableBase : Entity
    {
        // Base destructable class, used to for object hp management
        //public ParticleSystem Explode;

        /*    public void Update()
            {
                if (health <= 0)
                {
                    Explode.Play();

                }
            }*/

       /* protected override void Die()
        {
             base.Die();
            Debug.Log("sssss");

            Drop();
            Destroy(gameObject);
            Explode.Play();

        }
*/

    }
}
