using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SavingGameData : MonoBehaviour
{
    //Currently it will override the previous save

    private string DATA_PATH = "/Neonsave.dat";

    private PlayerData myPlayer;

    // We always need to reference it in the inspector which is a problem when switching scenes

    [SerializeField]
    private Entity entity;  
    
    [SerializeField]
    private PlayerStatManager PStats;


    private void Start()
    {
       // TO SAVE

       // SavingData();


        //    print("DATA PATH IS" + Application.persistentDataPath + DATA_PATH);

        // LOADING DATA

      /*  LoadingData();

        if (myPlayer != null)
        {
            print("Player Health:" + myPlayer.PlayerMaxHealth);
            print("Player Armor:" + myPlayer.PlayerMaxArmor);
            print("Player Ammo:" + myPlayer.PlayerMaxAmmo);
            print("Player $:" + myPlayer.PlayerCurrentMoney);
            print("Player Shotgun:" + myPlayer.Shotgun);
            print("Player Gun:" + myPlayer.Gun);
            print("Player Katana:" + myPlayer.Katana);
        }*/

        // Here I am inputting these values into the scripts that handles them

        entity._maxHealth = myPlayer.PlayerMaxHealth;
        PStats._maxxArmorPoints = myPlayer.PlayerMaxArmor;
        PStats._maxxAmmoCount = myPlayer.PlayerMaxAmmo;
        PStats._moneyAmount = myPlayer.PlayerCurrentMoney;

      
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



            // Encrypting the data

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



        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Open(Application.persistentDataPath + DATA_PATH, FileMode.Open);

            // Decrypting the data

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


