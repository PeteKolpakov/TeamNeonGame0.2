using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopTriggerCollider : MonoBehaviour
{

    [SerializeField]
    private Shop_UI uiShop;

    [SerializeField]
    private GameObject player;

    private GameObject shopCustomer;

    private void Start()
    {
        shopCustomer = player;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        uiShop.Show();
        /* if (shopCustomer != null)
         {
             uiShop.Show();
         }*/
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        uiShop.Hide();
    }

}
