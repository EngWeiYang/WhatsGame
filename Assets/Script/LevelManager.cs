using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels;
    // Start is called before the first frame update
    void Start()
    {
        levels[MainMenu.currLevel - 1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
