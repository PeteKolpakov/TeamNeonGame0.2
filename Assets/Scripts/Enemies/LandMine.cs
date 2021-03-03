using Assets.Scripts.GameManager;
using SpriteGlow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    [SerializeField]
    private Gradient _gradient;
    [SerializeField]
    private GameObject _landMineBoom;

    SpriteGlowEffect glowEffect;

    public float LineOfSight = 4f;

    public float t = 0;

    public float ExplosionDelay = 2f;

    public float MaxDamage = 80f;
   

    public float SplashRange = 1;

    public bool Triggered;

    private void Start()
    {
        glowEffect = GetComponent<SpriteGlowEffect>();
    }
    private void Update()
    {
        if (!Triggered)
        {
            DetectPlayer();

        }
        else
        {
            ChangeMaterialColorAndGlow();
        }

    }

    // Check if player is in Line of sight
    public void DetectPlayer()
    {
        Vector3 playerPos = PlayerTracker.Instance.Player.transform.position;
        float distanceFromPlayer = Vector2.Distance(playerPos, transform.position);
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
    private void ChangeMaterialColorAndGlow()
    {
        float value = Mathf.Lerp(0f, 1f, t);
        t += Time.deltaTime / ExplosionDelay;
        glowEffect.GlowBrightness += 0.2f;
        Color color = _gradient.Evaluate(value);
        GetComponent<Renderer>().material.color = color;
    }

    //Explode and deal damage after being triggered
    public void Explode()
    {
        if(_landMineBoom !=null)
        {
            Instantiate(_landMineBoom, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);

        //Add damage to anything inside this range

        var hitColliders = Physics2D.OverlapCircleAll(transform.position, SplashRange); //Does this work???

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