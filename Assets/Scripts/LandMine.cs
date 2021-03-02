using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    [SerializeField]
    private Gradient _gradient;

    public Color changeColor = Color.red;

    public float LineOfSight = 4f;

    public float t = 0;

    public float ExplosionDelay = 2f;

    public float MaxDamage = 80f;
   
    private Transform player;

    public float SplashRange = 1;

    public bool Triggered;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;       
    }

    private void Update()
    {
        if (!Triggered)
        {
            DetectPlayer();

        }
        else
        {
            ChangeMaterialColor();
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

       
        
        yield return new WaitForSeconds(ExplosionDelay);

        Explode();
    }
    private void ChangeMaterialColor()
    {
        float value = Mathf.Lerp(0f, 1f, t);
        t += Time.deltaTime / ExplosionDelay;
        
        Color color = _gradient.Evaluate(value);
        this.GetComponent<Renderer>().material.color = color;
        Debug.Log(color);
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
                int damageInt = (int)Mathf.Round(damage);
                

                Debug.Log(entity.name + "Took" + damage);

                entity.TakeDamage(damageInt, DamageType.Bullet);
            }
        }

    }

    //If player walks over mine it explodes and deals damage

    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.TryGetComponent(out Entity entity))
        {
            Debug.Log("inside");

        

            Explode();


        }

    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, LineOfSight);
        Gizmos.DrawWireSphere(transform.position, SplashRange);
    }

}