using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Array of GameObjects representing the levels
    public GameObject[] levels;

    // Start is called before the first frame update
    void Start()
    {
        // Activate the current level based on the selected level in LevelSelect
        levels[LevelSelect.currLevel].SetActive(true);
    }
}
