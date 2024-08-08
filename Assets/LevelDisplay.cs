using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class LevelName
{
    public int levelId;
    public string En;
    public string Cn;
}

[System.Serializable]
public class Levels
{
    public int levelId;
    public List<LevelName> levelNames;
}

[System.Serializable]
public class LevelsData
{
    public List<Levels> levels;
}

public class LevelDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] levelTextObjects; // Assign these in the Inspector
    public TextAsset jsonFile; // Assign this in the Inspector

    void Start()
    {
        LoadLevelNames();
    }

    void LoadLevelNames()
    {
        // Parse the JSON data
        LevelsData data = JsonUtility.FromJson<LevelsData>(jsonFile.text);

        // Assuming you want to load level names from the first level in the JSON
        List<LevelName> levelNames = data.levels[0].levelNames;

        // Assign the level names to the TextMeshPro objects
        for (int i = 0; i < levelTextObjects.Length; i++)
        {
            if (i < levelNames.Count)
            {
                levelTextObjects[i].text = levelNames[i].En; // Assign English names
                // levelTextObjects[i].text = levelNames[i].Cn; // Uncomment to assign Chinese names
            }
            else
            {
                levelTextObjects[i].text = "N/A"; // Fallback if there are not enough levels
            }
        }
    }
}
