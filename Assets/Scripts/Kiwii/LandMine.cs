using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{

   private SpriteRenderer rend;
    public Color changeColor = Color.red;

    public float LineOfSight = 4f;

    public float ExplosionDelay = 2f;

    public float MaxDamage = 80f;
   
    private Transform player;

    public float SplashRange = 1;

    public bool Triggered;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rend = gameObject.GetComponent<SpriteRenderer>();

       
    }

    private void Update()
    {
        if (!Triggered)
        {
            DetectPlayer();
        }

    }

    // Check if player is in Line of sight
    public void DetectPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer <= LineOfSight)
        {

            StartCoroutine(TriggerExplode());
          
            Debug.Log("Explosion Range");

        }
     
    }

    private IEnumerator TriggerExplode()
    {
        Triggered = true;

        rend.color = changeColor;
        yield return new WaitForSeconds(ExplosionDelay);

        Explode();


    }


    //Explode and deal damage after being triggered
    public void Explode()
    {

        Debug.Log("Explodeee");

        Destroy(gameObject);

        //Add damage to anything inside this range

        var hitColliders = Physics2D.OverlapCircleAll(transform.position, SplashRange);

        // Detect all colliders inside the SplashRange
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.name);

            //Check if its an entity

            if (hitCollider.TryGetComponent(out Entity entity))
            {

                var closestPoint = hitCollider.ClosestPoint(transform.position);

                var distance = Vector3.Distance(closestPoint, transform.position);


                //The damage percent depends on how close you are to the center of the explosion
                var damage = Mathf.Lerp(MaxDamage, 0, distance / SplashRange);

                //Deal damage to all Entities inside the range based on percentage related distance

                

                Debug.Log(entity.name + "Took" + damage);

                entity.TakeDamage(damage);
            }

        }

    }

    //If player walks over mine it explodes and deals damage

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.TryGetComponent(out Entity health))
        {
            Debug.Log("inside");

        

            Explode();


        }

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, LineOfSight);
    }

}