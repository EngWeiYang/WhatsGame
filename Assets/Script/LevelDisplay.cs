using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Represents a level name with English and Chinese translations
[System.Serializable]
public class LevelName
{
    public int levelId; // ID of the level
    public string En;   // English name of the level
    public string Cn;   // Chinese name of the level
}

// Represents a collection of level names associated with a specific level
[System.Serializable]
public class Levels
{
    public int levelId; // ID of the level
    public List<LevelName> levelNames; // List of names for that level
}

// Represents the data structure for multiple levels
[System.Serializable]
public class LevelsData
{
    public List<Levels> levels; // List of levels
}

public class LevelDisplay : MonoBehaviour
{
    // TextMeshProUGUI objects to display level names, assigned in the Inspector
    public TextMeshProUGUI[] levelTextObjects;

    // JSON file containing the level names, assigned in the Inspector
    public TextAsset jsonFile;

    // List to store level names loaded from JSON
    public List<LevelName> levelNames;

    void Start()
    {
        // Parse the JSON data to get all levels data
        LevelsData data = JsonUtility.FromJson<LevelsData>(jsonFile.text);

        // Load level names from the first level in the JSON data
        levelNames = data.levels[0].levelNames;
    }

    void Update()
    {
        // Assign the level names to the TextMeshProUGUI objects
        for (int i = 0; i < levelTextObjects.Length; i++)
        {
            if (i < levelNames.Count) // Check if there are enough level names
            {
                if (Checker.isEnglish) // If English is selected
                {
                    levelTextObjects[i].text = levelNames[i].En; // Assign English names
                }
                else // If another language (Chinese) is selected
                {
                    levelTextObjects[i].text = levelNames[i].Cn; // Assign Chinese names
                }
            }
            else // Fallback if there are not enough level names to display
            {
                levelTextObjects[i].text = "N/A"; // Display "N/A" for unavailable levels
            }
        }
    }
}
