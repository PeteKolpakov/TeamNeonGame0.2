using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    private float timeBtwChop;
    public float startTimeBtwChop;

    public Transform AttackPos;

    public int MeleeDamage;


    public float attackRangeX;
    public float attackRangeY;

    // Reference the animator 

   // public Animator Anim;


    private void Start()
    {
        startTimeBtwChop = 0f;

    }


    public void MeleeAttack()
    {
        if (timeBtwChop <= 0)
        {
           // if (Input.GetKeyDown(KeyCode.Mouse1)) {

           //     Anim.SetTrigger("Cutting");


                //Add the animation trigger here

                // anim.SetTrigger("Daniel is gay");

                var CollidersToDamage = Physics2D.OverlapBoxAll(AttackPos.position, new Vector2(attackRangeX, attackRangeY), 0);

                foreach (var hitCollider in CollidersToDamage)
                {
                    //Check if its an entity

                    if (hitCollider.TryGetComponent(out Entity entity))
                    {

                        Debug.Log(entity.name + " took " + MeleeDamage + " damage");
                        entity.TakeDamage(MeleeDamage, DamageType.Bullet);

                        // ADD particle effects

                    }
                } timeBtwChop = startTimeBtwChop;
            }
            else
            { timeBtwChop -= Time.deltaTime; } }

  //  }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(AttackPos.position, new Vector3(attackRangeX, attackRangeY, 1));
    }




}

