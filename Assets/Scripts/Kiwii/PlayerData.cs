using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public int CurrentLvl;

    public float PlayerMaxHealth;
    public int PlayerMaxArmor;
    public int PlayerMaxAmmo;

    public int PlayerCurrentMoney;


    public List<int> PurchasedGunsInt;

 //   public List<int> CompletedLevelsInt;


    public PlayerData()
    {

    }
}