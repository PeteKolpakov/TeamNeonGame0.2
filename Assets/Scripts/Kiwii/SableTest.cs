/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SableTest : MonoBehaviour
{

    private float _attackTimer;
    // projectile amount
   

    // spread angle
    [SerializeField]
    private float _spreadAngle = 5;
    // Sprite
    public Sprite _icon;

  

    [SerializeField]
    private GameObject _projectilePrefab;
    public bool _isEquipped = false;

    public delegate void RemoveAmmo(int ammo);
    public static event RemoveAmmo removeAmmo;

    private float timeBtwChop;
    public float startTimeBtwChop;

    public Transform ChopPos;


 //   public LayerMask whatIsTrees;

    public float chopRange;

    // I need to reference the animator 

    //public Animator dudeAnim;


    private void Update()
    {
        

        SwingDelay();

    }



    public void SwingDelay()
    {
        if (timeBtwChop <= 0)
        {

            if (Input.GetButton("click"))
            {
                ChopAttack();
            } timeBtwChop = startTimeBtwChop;
        } else {
            timeBtwChop -= Time.deltaTime;
        }
    }

    public void ChopAttack()
    {
        //Add the animation trigger here

       // anim.SetTrigger("Example");

    


        var CollidersToDamage = Physics2D.OverlapCircleAll(ChopPos.position, chopRange);

        *//* for (int i = 0; i < CollidersToDamage.Length; i++)
         {*//*

        foreach (var hitCollider in CollidersToDamage)
        {
            Debug.Log(hitCollider.name);



            //Check if its an entity

            if (hitCollider.TryGetComponent(out Entity entity))
            {

                Debug.Log(entity.name + "Took" + _damage);
                entity.TakeDamage(_damage, DamageType.Bullet);

                // ADD particle effects

            }


        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, chopRange);
    }




}

*/