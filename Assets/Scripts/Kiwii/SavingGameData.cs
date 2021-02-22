using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SavingGameData : MonoBehaviour
{
    private string DATA_PATH = "/Neonsave.dat";

    private PlayerData myPlayer;

    /*private float playerCurrentMaxHealth;
    private float playerCurrentMaxAmmo;
    private float playerCurrentMaxArmor;
    private int playerCurrentMoney;*/

    [SerializeField]
    private Entity entity;  
    
   

    [SerializeField]
    private PlayerStatManager PStats;

    
    private void Start()
    {
     /*   playerCurrentMaxHealth = entity._maxHealth; 
        playerCurrentMaxArmor = PStats._maxxArmorPoints;
        playerCurrentMaxAmmo = PStats._maxxAmmoCount;
        playerCurrentMoney = PStats._moneyAmount;*/



       // SavingData();


        //    print("DATA PATH IS" + Application.persistentDataPath + DATA_PATH);


        LoadingData();
        entity._maxHealth = myPlayer.PlayerMaxHealth;
        PStats._maxxArmorPoints = myPlayer.PlayerMaxArmor;
        PStats._maxxAmmoCount = myPlayer.PlayerMaxAmmo;
        PStats._moneyAmount = myPlayer.PlayerCurrentMoney;

        if (myPlayer != null)
        {
            print("Player Health:" + myPlayer.PlayerMaxHealth);
            print("Player Armor:" + myPlayer.PlayerMaxArmor);
            print("Player Ammo:" + myPlayer.PlayerMaxAmmo);
            print("Player $:" + myPlayer.PlayerCurrentMoney);
            print("Player Shotgun:" + myPlayer.Shotgun);
            print("Player Gun:" + myPlayer.Gun);
            print("Player Katana:" + myPlayer.Katana);
        }
    }


    void SavingData()
    {
        FileStream file = null;

        PlayerData p = new PlayerData();

        p.PlayerMaxHealth = entity._maxHealth;
        p.PlayerMaxArmor = PStats._maxxArmorPoints;
        p.PlayerMaxAmmo = PStats._maxxAmmoCount;
        p.PlayerCurrentMoney = PStats._moneyAmount;

        try
        {

            BinaryFormatter BF = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + DATA_PATH );

           // PlayerData p = new PlayerData(playerCurrentMaxHealth, playerCurrentMaxArmor, playerCurrentMaxAmmo, playerCurrentMoney, 1, 0, 1);




            BF.Serialize(file, p);

        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
           

        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }

        }
    }

    void LoadingData()
    {
        FileStream file = null;

      /*  entity._maxHealth = myPlayer.PlayerMaxHealth;
        PStats._maxxArmorPoints = myPlayer.PlayerMaxArmor;
        PStats._maxxAmmoCount = myPlayer.PlayerMaxAmmo;
        PStats._moneyAmount = myPlayer.PlayerCurrentMoney;*/


        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Open(Application.persistentDataPath + DATA_PATH, FileMode.Open);
            // Decrypting

            myPlayer = bf.Deserialize(file) as PlayerData;
        }

        catch (Exception e)
        {
            Debug.LogError(e.Message);

        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }



    } 
}

    // THIS GOES IN THE PLAYER CLASS AND ALL OTHER CLASSES THAT NEED THE INFORMATION CONTAINED WITHIN THE PLAYERDATA FILE

/*public void LoadPlayerData()
{
    PlayerData data = PlayerData();
    maxhealth = data.PlayerMaxHealth; // Like this for all variables

}*/



