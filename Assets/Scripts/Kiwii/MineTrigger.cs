using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineTrigger : MonoBehaviour
{
  //  public float ExplosionDelay = 1f;
    public Entity entity;
    public LandMine landmine;


    public float Damage;

    public float SplashRange = 2;



    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Entity health))
        {
            Debug.Log("aaaaaaaaa");
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, SplashRange);

            // Detect all colliders inside the SplashRange
            foreach (var hitCollider in hitColliders)
            {
                var closestPoint = hitCollider.ClosestPoint(transform.position);
                var distance = Vector3.Distance(closestPoint, transform.position);


                //The damage percent depends on how close you are to the center of the explosion
                var damagePercent = Mathf.InverseLerp(SplashRange, 0, distance);

                //Deal damage to all Entities inside the range

                //TODO NEED TO ADD SAME DELAY BEFORE DEALING DAMAGE AS DETROYING
                health.TakeDamage(damagePercent * Damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, SplashRange);
    }


}


