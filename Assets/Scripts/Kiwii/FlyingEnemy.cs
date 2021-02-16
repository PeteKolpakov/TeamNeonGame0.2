using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    public float Speed;

    public float LineOfSight = 8f;

    public float KamikazeRange = 6f;

    public float SplashRange = 3f;


    // This is the speed the normal Speed will be multiplied for (Speed * Kamikaze)
    [SerializeField]
    private float Kamikaze = 4;

    private float MaxDamage = 80f;



    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        DetectPlayer();

    }

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

    // Detect if player is within range of sight and KamikazeRange

        public void DetectPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < LineOfSight && distanceFromPlayer > KamikazeRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, Speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= KamikazeRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, (Speed * Kamikaze) * Time.deltaTime);
           
        }

    }

 
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
       if (collision.collider.TryGetComponent(out Entity health))
            {
                Debug.Log("Hitting Player");
          

            Explode();
        }
      
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, LineOfSight);
        Gizmos.DrawWireSphere(transform.position, KamikazeRange);
        Gizmos.DrawWireSphere(transform.position, SplashRange);
    }

}