using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Currency")]
    public int currency = 0;
    public Text Currency;

  
    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        Currency.text = "$" + currency;
        //  EXP.text = "$" + exp;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
        Currency.text = "$" + currency;
    }
}

