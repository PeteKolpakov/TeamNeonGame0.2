using Assets.Scripts.EntityClass;
using Assets.Scripts.GameManager;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float Speed;

    public float LineOfSight = 8f;

    public float KamikazeRange = 6f;

    public float SplashRange = 3f;

    private Vector2 _direction;

    [SerializeField]
    private GameObject _flyingEnemyBoom;

    // This is the speed the normal Speed will be multiplied for (Speed * Kamikaze)
    [SerializeField]
    private float _kamikaze = 4;

    [SerializeField]
    private float _maxDamage = 60;

    private void Update()
    {
        DetectPlayer();
    }

    public void Explode()
    {
        //Add damage to anything inside this range

        var hitColliders = Physics2D.OverlapCircleAll(transform.position, SplashRange);

        // Detect all colliders inside the SplashRange
        foreach (var hitCollider in hitColliders)
        {
            //Check if its an Player
            if (hitCollider.TryGetComponent(out Entity entity))
            {
                var closestPoint = hitCollider.ClosestPoint(transform.position);

                var distance = Vector3.Distance(closestPoint, transform.position);

                //The damage percent depends on how close you are to the center of the explosion

                var damage = Mathf.Lerp(_maxDamage, 0, distance / SplashRange);

                //Deal damage to all Entities inside the range based on percentage related distance
                int damageInt = (int)Mathf.Round(damage);

                if (_flyingEnemyBoom != null)
                {
                    Instantiate(_flyingEnemyBoom, transform.position, Quaternion.identity);
                }
                if(entity.canTakeDamage == true)
                    entity.TakeDamage(damageInt, DamageType.Bullet);
            }
        }

        Destroy(gameObject);
    }

    // Detect if player is within range of sight and KamikazeRange

    public void DetectPlayer()
    {
        Vector3 playerPos = PlayerTracker.Instance.Player.transform.position;

        float distanceFromPlayer = Vector2.Distance(playerPos, transform.position);

        _direction = playerPos - transform.position;

        float angle = Mathf.Atan2(_direction.y, _direction.x);

        if (distanceFromPlayer < LineOfSight && distanceFromPlayer > KamikazeRange) // Should you be able to just walk out of its range when detected? Show Visual Test Scene to demonstrate
        {

            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle * Mathf.Rad2Deg - 90f));
            transform.position = Vector2.MoveTowards(this.transform.position, playerPos, Speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= KamikazeRange)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle * Mathf.Rad2Deg - 90f));
            transform.position = Vector2.MoveTowards(this.transform.position, playerPos, (Speed * _kamikaze) * Time.deltaTime);
        }

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.TryGetComponent(out PlayerBase health)) // Should it not be Player Base? Ask in meeting c:
        {

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