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

    public int Shotgun;
    public int Gun;
    public int Katana;


  //  public int[] PurchasedGuns = new int[3];



    public int companion1;
    public int companion2;

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
     public int Shotgun
     {
         get
         {
             return shotgun;
         }
         set
         {
             shotgun = value;
         }
     }  
     public int Gun
     {
         get
         {
             return gun;
         }
         set
         {
             gun = value;
         }
     } 
     public int Katana
     {
         get
         {
             return katana;
         }
         set
         {
             katana = value;
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
     } 
     public int Lvl2
     {
         get
         {
             return lvl2;
         }
         set
         {
             lvl2 = value;
         }
     }  

     public int Lvl3    {
         get
         {
             return lvl3;
         }
         set
         {
             lvl3 = value;
         }
     }
     public int Companion1    {
         get
         {
             return companion1;
         }
         set
         {
             companion1 = value;
         }
     }
     public int Companion2    {
         get
         {
             return companion2;
         }
         set
         {
             companion2 = value;
         }
     }

 */

}
