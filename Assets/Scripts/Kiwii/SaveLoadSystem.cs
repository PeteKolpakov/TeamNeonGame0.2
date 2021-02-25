using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveLoadSystem : MonoBehaviour
{
    //Currently it will override the previous save

    private string DATA_PATH = "/Neonsave.dat";

    private PlayerData MyCurrentPlayerData;

    // We always need to reference it in the inspector which is a problem when switching scenes

    [SerializeField]
    private Entity entity;

    [SerializeField]
    private PlayerStatManager PStats;


    private void Start()
    {
        // TO SAVE

        //   SavingData();


        //    print("DATA PATH IS" + Application.persistentDataPath + DATA_PATH);

        // LOADING DATA

    //    LoadingData();

      
        // Here I am inputting these values into the scripts that handles them
        // AKA LOADING THE DATA

        entity.maxHealth = MyCurrentPlayerData.PlayerMaxHealth;
        PStats._maxxArmorPoints = MyCurrentPlayerData.PlayerMaxArmor;
        PStats._maxxAmmoCount = MyCurrentPlayerData.PlayerMaxAmmo;
        PStats._moneyAmount = MyCurrentPlayerData.PlayerCurrentMoney;
        PStats.LoadWeapons(MyCurrentPlayerData.PurchasedGunsInt);

        // This is being called and loads the data even if you dont want to. now that doesnt happen anymore for some reason



    }


   public void SavingData()
    {
        FileStream file = null;

        PlayerData p = new PlayerData();

        p.PlayerMaxHealth = entity.maxHealth;
        p.PlayerMaxArmor = PStats._maxxArmorPoints;
        p.PlayerMaxAmmo = PStats._maxxAmmoCount;
        p.PlayerCurrentMoney = PStats._moneyAmount;
        p.PurchasedGunsInt = PStats.BoughtGunsInt;

        Debug.Log("Im trying to save weapons");
        Debug.Log(PStats.BoughtGunsInt.Count);

        try
        {
            BinaryFormatter BFormatter = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + DATA_PATH);
        
            // Encrypting the data
            BFormatter.Serialize(file, p);

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
   public void LoadingData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter BinaryF = new BinaryFormatter();

            file = File.Open(Application.persistentDataPath + DATA_PATH, FileMode.Open);

            // Decrypting the data
            MyCurrentPlayerData = BinaryF.Deserialize(file) as PlayerData;
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


