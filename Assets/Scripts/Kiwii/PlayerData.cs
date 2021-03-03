using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData
{
    public int CurrentLvl;

    public int PlayerMaxHealth;

    public int PlayerCurrentMoney;


    public List<int> PurchasedGunsInt;

 //   public List<int> CompletedLevelsInt;


    public PlayerData()
    {

    }
}