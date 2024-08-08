using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    [Serializable]
    public class LevelName
    {
        public int levelId;
        public string En;
        public string Cn;
    }

    [Serializable]
    public class Level
    {
        public int levelId;
        public List<LevelName> levelNames;
    }

    [Serializable]
    public class LevelsData
    {
        public List<Level> levels;
    }

    public TextAsset jsonFile; // Assign the JSON file in the Inspector

    void Start()
    {
        // Load and parse the JSON data
        LevelsData data = JsonUtility.FromJson<LevelsData>(jsonFile.text);

        // Example: Accessing level names for levelId 0
        foreach (var level in data.levels)
        {
            if (level.levelId == 0)
            {
                foreach (var levelName in level.levelNames)
                {
                    Debug.Log($"Level ID: {levelName.levelId}, En: {levelName.En}, Cn: {levelName.Cn}");
                }
            }
        }
    }
}
