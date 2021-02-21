using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SavingGameData : MonoBehaviour
{
    private string DATA_PATH = "/MyGame.dat";

    private PlayerData myPlayer;

    private void Start()
    {
        // SavingData();

        // print("DATA PATH IS" + Application.persistentDataPath + DATA_PATH);

        LoadingData();

        if (myPlayer != null)
        {
            print("Player Health:" + myPlayer.PlayerMaxHealth);
            print("Player Armor:" + myPlayer.PlayerMaxArmor);
            print("Player Ammo:" + myPlayer.PlayerMaxAmmo);
            print("Player $:" + myPlayer.PlayerMoney);
            print("Player Shotgun:" + myPlayer.Shotgun);
            print("Player Gun:" + myPlayer.Gun);
            print("Player Katana:" + myPlayer.Katana);
        }
    }


    void SavingData()
    {
        FileStream file = null;
        try
        {

            BinaryFormatter BF = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/MyGame.dat");

            PlayerData p = new PlayerData(10, 10, 10, 100, 1, 0, 1);

            BF.Serialize(file, p);

        }
        catch (Exception e)
        {
            if (e != null)
            {

            }

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
            // Decrypting

            myPlayer = bf.Deserialize(file) as PlayerData;
        }

        catch (Exception e)
        {

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



