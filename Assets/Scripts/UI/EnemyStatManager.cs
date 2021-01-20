using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatManager : MonoBehaviour
{
    public int _maxHealth;
    public int _currentHealth;
    
    void Start()
    {
        _currentHealth = _maxHealth;
    }

   
    void Update()
    {
       if(_currentHealth <= 0)
        {
            Destroy(gameObject);
        } 
    }

    public void HurtEnemy(int damage)
    {
        _currentHealth -= damage;
    }
}
