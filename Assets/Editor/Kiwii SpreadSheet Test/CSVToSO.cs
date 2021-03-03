using UnityEngine;
using UnityEditor;
using System.IO;

public class CSVToSO : EditorWindow
{
    private string itemCSVPath = "/Editor/CSV/shopItems3.csv";

    // To see it in action go to the "Utilities" tab and press Generate Items

    [MenuItem("Utilities/CSVWindow")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        CSVToSO window = (CSVToSO)EditorWindow.GetWindow(typeof(CSVToSO));
        window.Show();
    }

    void OnGUI()
    {
        itemCSVPath = EditorGUILayout.TextField("Path", itemCSVPath);

        //if (GUILayout.Button("Browse"))
        //{
        //    itemCSVPath = EditorUtility.OpenFilePanel("Select csv", Application.dataPath, "csv");
        //}

        if (GUILayout.Button("Generate"))
        {
            GenerateItems();
        }
    }

    public void GenerateItems()
    {
        var path = Application.dataPath + itemCSVPath;
        if (File.Exists(path))
        {
            string[] allLines = File.ReadAllLines(Application.dataPath + itemCSVPath);


            for (int i = 0; i < allLines.Length; i++)
            {
                string s = allLines[i];
                string[] splitData = s.Split(',');
                ReworkedItem item = ScriptableObject.CreateInstance<ReworkedItem>();
                item._name = splitData[0];
                item._description = splitData[1];

                ParseInt(i, splitData, 2, ref item._price);
                ParseInt(i, splitData, 3, ref item._damage);

                ParseItemType(i, splitData, 4, ref item.itemType);

                ParseFloat(i, splitData, 5, ref item._fireRate);
                ParseInt(i, splitData, 6, ref item._projectileAmount);
                //ParseFloat(i, splitData, 7, ref item._spreadAngle);
                ParseInt(i, splitData, 8, ref item.WeaponID);

                AssetDatabase.CreateAsset(item, $"Assets/Resources/WeaponPrefabs/Test/{item._name}.asset");
            }

            AssetDatabase.SaveAssets();
        }
    }

    private static bool ParseItemType(int line, string[] splitData, int index, ref ItemType reference)
    {
        if (System.Enum.TryParse(splitData[index], out ItemType value))
        {
            reference = value;
            return true;
        }
        else
        {
            Debug.LogError("Failed parsing data: Line: " + line + " Value: " + splitData[2] + " expected integer");
            return false;
        }
    }

    private static bool ParseInt(int line, string[] splitData, int index, ref int reference)
    {
        if (int.TryParse(splitData[index], out int value))
        {
            reference = value;
            return true;
        }
        else
        {
            Debug.LogError("Failed parsing data: Line: " + line + " Value: " + splitData[index] + " expected integer");
            return false;
        }
    }

    private static bool ParseFloat(int line, string[] splitData, int index, ref float reference)
    {
        if (float.TryParse(splitData[index], out float value))
        {
            reference = value;
            return true;
        }
        else
        {
            Debug.LogError("Failed parsing data: Line: " + line + " Value: " + splitData[index] + " expected integer");
            return false;
        }
    }
}
