using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{

   private SpriteRenderer rend;
    public Color changeColor = Color.red;

    public float lineOfSight;

    public float ExplosionDelay = 2f;

    public bool ExplosionRange;
   
    private Transform player;


    public float Damage = 20;
    public float SplashRange = 1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rend = gameObject.GetComponent<SpriteRenderer>();

        ExplosionRange = false;
    }

    private void Update()
    {
        DetectPlayer();

    }

    // Check if player is in Line of sight
    public void DetectPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer <= lineOfSight)
        {
            rend.color = changeColor;
            Debug.Log("Explosion Range");

            ExplosionRange = true;
            AlBaghdadi();
        }
     
    }


    //Explode and deal damage after being triggered
    public void AlBaghdadi()
    {
        if ((ExplosionRange == true))
        {
               Destroy(gameObject, ExplosionDelay);
           
            //Add damage to anything inside this range



        }
    }


    //If player walks over mine it explodes and deals damage

 /*   private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.TryGetComponent(out Entity health))
        {
            Debug.Log("inside");

            //   health.TakeDamage(80);

            Destroy(gameObject, ExplosionDelay);



           *//* var hitColliders = Physics2D.OverlapCircleAll(transform.position, SplashRange);
            foreach (var hitCollider in hitColliders)
            {
                var closestPoint = hitCollider.ClosestPoint(transform.position);
                var distance = Vector3.Distance(closestPoint, transform.position);

                var damagePercent = Mathf.InverseLerp(SplashRange, 0, distance);
                health.TakeDamage(damagePercent * Damage);
            }*//*
        }

    }*/


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }

}