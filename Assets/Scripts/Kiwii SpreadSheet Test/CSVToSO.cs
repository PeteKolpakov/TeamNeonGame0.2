using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVToSO
{
    private static string itemCSVPath = "/Editor/CSV/shopItems3.csv";


    // To see it in action go to the "Utilities" tab and press Generate Items
    
    [MenuItem("Utilities/Generate Items")]

    public static void GenerateItems()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + itemCSVPath);

        foreach (string s in allLines)
        {
            string[] splitData = s.Split(',');
            Item2 item = ScriptableObject.CreateInstance<Item2>();
            item._name = splitData[0];
            item._description = splitData[1];
            item._price = int.Parse(splitData[2]);
            item._damage = int.Parse(splitData[3]);

            // I dont know yet how to input itemType data from the CSV

            // item.itemType = splitData[4];
            item._fireRate = float.Parse(splitData[5]);

            item._projectileAmount = int.Parse(splitData[6]);
            item._spreadAngle = float.Parse(splitData[7]);



        //    AssetDatabase.CreateAsset(item, "Assets/Resources/WeaponPrefabs/Test" + item._name);

            //They will be store here, IF YOU GENERATE WEAPONS MAKE SURE YOU DELETE THEM 
            AssetDatabase.CreateAsset(item, $"Assets/Resources/WeaponPrefabs/Test/{item._name}.asset" );

        }
        AssetDatabase.SaveAssets();

    }
}
