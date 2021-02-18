using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    private float timeBtwChop;
    public float startTimeBtwChop;

    public Transform AttackPos;

    public int MeleeDamage;

    //   public LayerMask whatIsTrees;

    public float chopRange;

    // I need to reference the animator 

    //public Animator dudeAnim;


    private void Update()
    {

   //     MeleeAttack();

    }

    public void MeleeAttack()
    {

      //  if ((timeBtwChop <= 0) && (Input.GetButton("click")))

        if (timeBtwChop <= 0)
        {

            //Add the animation trigger here

            // anim.SetTrigger("Example");




            var CollidersToDamage = Physics2D.OverlapCircleAll(AttackPos.position, chopRange);

            /* for (int i = 0; i < CollidersToDamage.Length; i++)
             {*/

            foreach (var hitCollider in CollidersToDamage)
            {
                Debug.Log(hitCollider.name);



                //Check if its an entity

                if (hitCollider.TryGetComponent(out Entity entity))
                {

                    Debug.Log(entity.name + "Took" + MeleeDamage);
                    entity.TakeDamage(MeleeDamage, DamageType.Bullet);

                    // ADD particle effects

                }


            }
            timeBtwChop = startTimeBtwChop;

        }
        else
        {
            timeBtwChop -= Time.deltaTime;
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, chopRange);
    }




}
