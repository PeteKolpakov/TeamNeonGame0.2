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


    public PlayerData()
    {

    }



    /* public PlayerData(float playermaxhealth, float playermaxarmor, float playermaxammo, int playermoney, int shotgunn, int gunn, int katanaa) {

         playerMaxHealth = playermaxhealth;
         playerMaxArmor = playermaxarmor;
         playerMaxAmmo = playermaxammo;
         playerCurrentMoney = playermoney;
         shotgun = shotgunn;
         gun = gunn;
         katana = katanaa;




             }*/
    /* public int CurrentLvl
     {
         get
         {
             return currentLvl;
         }
         set
         {
             currentLvl = value;
         }
     } 

     public float PlayerMaxHealth
     {
         get
         {
             return playerMaxHealth;
         }
         set
         {
             playerMaxHealth = value;
         }
     }  
     public float PlayerMaxArmor
     {
         get
         {
             return playerMaxArmor;
         }
         set
         {
             playerMaxArmor = value;
         }
     } 
     public float PlayerMaxAmmo
     {
         get
         {
             return playerMaxAmmo;
         }
         set
         {
             playerMaxAmmo = value;
         }
     }
         public int PlayerMoney
     {
         get
         {
             return playerCurrentMoney;
         }
         set
         {
             playerCurrentMoney = value;
         }
     }  
    
     public int Lvl1
     {
         get
         {
             return lvl1;
         }
         set
         {
             lvl1 = value;
         }

 */

}
