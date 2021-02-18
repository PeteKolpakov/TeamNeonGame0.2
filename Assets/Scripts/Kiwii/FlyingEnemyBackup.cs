/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    public float Speed;

    public float LineOfSight;

    public float ShootingRange;

    public float ExplosionDamage;

    [SerializeField]
    private float kamikaze = 4;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        DetectPlayer();

    }

    public void DetectPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < LineOfSight && distanceFromPlayer > ShootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, Speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= ShootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, (Speed * kamikaze) * Time.deltaTime);

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.TryGetComponent(out Entity health))
        {
            Debug.Log("Hitting Player");

            health.TakeDamage(ExplosionDamage);

            Destroy(gameObject);
        }

    }



    //Instantiate explosion
    //    GameObject e = Instantiate(explosion) as GameObject;
    //   e.transform.position = transform.position;

    // deal damage to player here




    public void AlBaghdadi()
    {


    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, LineOfSight);
        Gizmos.DrawWireSphere(transform.position, ShootingRange);
    }

}*/