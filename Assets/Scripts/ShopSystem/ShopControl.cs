using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopControl : MonoBehaviour
{
    int moneyAmount;

    int itemAmount;

    private bool itemToSell;

    //   private GameObject[] items;

    //  private List<Item> itemList;

    public TextAlignment moneyAmountText;
    public Text GunPrice;

    public Button buyButton;
    public Button CompanionBuyButton;

    public LevelManager LM;

    private Inventory inventory;

    [SerializeField]
    private GameObject itemButton;

    [SerializeField]
    private GameObject CompanionItem;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        buyButton.interactable = false;
        CompanionBuyButton.interactable = false;
    }

    private void Update()
    {


        if (LM.currency >= 50)
        {
            buyButton.interactable = true;
            CompanionBuyButton.interactable = true;
        }

    }


    public void BuyGun()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {

            if ((inventory.isFull[i] == false))

            {
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);

                LM.currency -= 100;

                itemToSell = false;

                Debug.Log("Buying");
                return;
                //      buyButton.gameObject.SetActive(false);
            }
        }
    }

    public void BuyCompanion()
    {
        for (int i = 0; i < inventory.companionSlots.Length; i++)
        {

            if ((inventory.isFull2[i] == false))

            {
                inventory.isFull2[i] = true;
                Instantiate(CompanionItem, inventory.companionSlots[i].transform, false);

                LM.currency -= 50;

                itemToSell = false;
                CompanionBuyButton.gameObject.SetActive(false);
                Debug.Log("Buying2");
                return;

            }

        }
    }
}



