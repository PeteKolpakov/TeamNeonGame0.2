using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    private float timeBtwChop;
    public float startTimeBtwChop;

    public Transform AttackPos;

    public int MeleeDamage;


    public float chopRange;

    // Reference the animator 

    //public Animator anim;


    private void Start()
    {
        startTimeBtwChop = 0f;

    }
    

    public void MeleeAttack()
    {
        if (timeBtwChop <= 0)
        {
            //Add the animation trigger here

            // anim.SetTrigger("Daniel is gay");

            var CollidersToDamage = Physics2D.OverlapCircleAll(AttackPos.position, chopRange);
            foreach (var hitCollider in CollidersToDamage)
            {
                Debug.Log(hitCollider.name);
                //Check if its an entity

                if (hitCollider.TryGetComponent(out Entity entity))
                {

                    Debug.Log(entity.name + "Took" + MeleeDamage);
                    entity.TakeDamage(MeleeDamage, DamageType.Melee);

                    // ADD particle effects

                }
            } timeBtwChop = startTimeBtwChop;
        }
        else
        { timeBtwChop -= Time.deltaTime; }  }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, chopRange);
    }




}

