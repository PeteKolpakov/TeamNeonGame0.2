using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperInteraction : MonoBehaviour
{
    public float _interactionRange = 5f;
    public Transform _player;
    public GameObject shopUI;
    public bool _isShopOpen = false;

    // Update is called once per frame
    void Update()
    {

        // Opens up UI when detects a player in a range, and closes the window, when 
        // player gets too far
        
        if (Vector3.Distance(_player.position, transform.position) <= _interactionRange)
        {
            _isShopOpen = true;
            shopUI.SetActive(true);
        } else
        {
            _isShopOpen = false;
            shopUI.SetActive(false);

        }
    }

    private void OnDrawGizmos()
    {
        // visualisation of a detection range

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _interactionRange);
    }

   
}
