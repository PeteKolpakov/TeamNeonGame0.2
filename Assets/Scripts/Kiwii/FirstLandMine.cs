/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLandMine : MonoBehaviour
{

    private SpriteRenderer rend;
    public Color changeColor = Color.red;

    public float lineOfSight;


    //  public float fireRate = 1f;

    //  private float nextFireTime;

    //   public GameObject bullet;
    //public GameObject bulletParent;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        DetectPlayer();

    }

    public void DetectPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight)
        {
            rend.color = changeColor;
            Debug.Log("Explosion");
        }
        else if (distanceFromPlayer > lineOfSight)
        {
            rend.color = Color.black;
        }

    }

    *//*void OnCollisionEnter2D(Collision2D collision)
     {

        // if (collision.TryGetComponent(out Entity health))// && collision.TryGetComponent(out player))
        if (collision.gameObject.tag == "Player")
         {
             Debug.Log("Hitting Player");
             health.TakeDamage(50);

             Destroy(gameObject);
         }*/

    /*   private void OnCollisionEnter2D(Collision2D collision)
       {
           if (collision.gameObject.tag == "Player" && (collision.collider.TryGetComponent(out Entity health)))
               {
                   Debug.Log("Hitting Player");
                  health.TakeDamage(50);
               Destroy(gameObject);
           }

       }   
       *//*
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.TryGetComponent(out Entity health))
        {
            Debug.Log("Hitting Player");

            health.TakeDamage(100);

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
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }

}*/