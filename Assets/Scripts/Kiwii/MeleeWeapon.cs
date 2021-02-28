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

    SpriteRenderer spriteRenderer;

    // Reference the animator 
    //public Animator anim;

    //public Animator Anim;



    private void Start()
    {
        startTimeBtwChop = 0f;

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

    }


    public void MeleeAttack()
    {

        if (timeBtwChop <= 0)
        {
            //Add the animation trigger here

            // anim.SetTrigger("Daniel is gay");
            //   spriteRenderer.enabled = true;


            //     Anim.SetTrigger("Cutting");


            //Add the animation trigger here

            // anim.SetTrigger("Daniel is not gay");

            var CollidersToDamage = Physics2D.OverlapCircleAll(AttackPos.position, chopRange);
            foreach (var hitCollider in CollidersToDamage)
            {
                //Check if its an entity

                if (hitCollider.TryGetComponent(out Entity entity))
                {

                    Debug.Log(entity.name + " took " + MeleeDamage + " damage");
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

        //Debug.Log(entity.name + " took " + MeleeDamage + " damage");
        //entity.TakeDamage(MeleeDamage, DamageType.Bullet);


        // ADD particle effects

        timeBtwChop = startTimeBtwChop;
        /* StopCoroutine(HideMelee());
         StartCoroutine(HideMelee());*/
    }






    public IEnumerator HideMelee()
    {
        yield return new WaitForSeconds(timeBtwChop);
        spriteRenderer.enabled = false;
    }

    /*  public void HideMelee()
      {
          spriteRenderer.enabled = false;
      }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, chopRange);
    }

    public Item AccessItemData()
    {
        Item data = GetComponent<Item>();
        return data;
    }
}








