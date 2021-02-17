using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SableTest : MonoBehaviour
{
    // Name
    public string _name;
    // Description
    public string _description;
    // Price
    public int _price;

    public int _damage = 1;
    public ItemType itemType;
    public float _fireRate = 1f;



    private float _attackTimer;
    // projectile amount
    [SerializeField]
    public int _projectileAmount = 1;
    // spread angle
    [SerializeField]
    private float _spreadAngle = 5;
    // Sprite
    public Sprite _icon;

    // projectile spawn point
   /* [SerializeField]
    private Transform _HitPoint;*/

    // Bullet prefab


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


 

    public enum ItemType
    {
        Ranged,
        Melee,
        Consumable
    }


    private void Update()
    {
        if (_attackTimer < _fireRate)
        {
            _attackTimer += Time.deltaTime;
        }


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

        /* for (int i = 0; i < CollidersToDamage.Length; i++)
         {*/

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


    // Weapon attack
    
    public void Attack(bool isPlayer)
    {

        if (_attackTimer < _fireRate)
            return;

        for (int i = 0; i < _projectileAmount; i++)
        {
            float angle = Random.Range(-_spreadAngle, _spreadAngle);
            Instantiate(_projectilePrefab, ChopPos.position, transform.rotation * Quaternion.Euler(0, 0, angle));
        }
        if (isPlayer == true)
        {
            removeAmmo(_projectileAmount);

        }
        _attackTimer -= _attackTimer;


    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, chopRange);
    }




}

