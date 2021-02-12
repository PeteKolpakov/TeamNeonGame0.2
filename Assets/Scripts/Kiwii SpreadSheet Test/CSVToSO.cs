using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVToSO
{
    private static string itemCSVPath = "/Editor/CSV/shopItems2.csv";

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
            //  item._price = int.Parse(splitData[2]);
            item._damage = int.TryParse(out splitData[3]);
            // item.itemType = splitData[4];
            item._fireRate = float.Parse(splitData[5]);

            item._projectileAmount = int.Parse(splitData[6]);
            item._spreadAngle = float.Parse(splitData[7]);



            AssetDatabase.CreateAsset(item, "Assets/Resources/WeaponPrefabs/Test" + item.name);

        }
        AssetDatabase.SaveAssets();

    }
}
