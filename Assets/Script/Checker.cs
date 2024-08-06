using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checker : MonoBehaviour
{
    public static bool firstTimeInScene = true;
    public GameObject levelSelectCanvas;
    public GameObject mainMenuCanvas;

    private void Start()
    {
        if (firstTimeInScene)
        {
            levelSelectCanvas.SetActive(false);
        }
        else
        {
            levelSelectCanvas.SetActive(true);
            mainMenuCanvas.SetActive(false);
        }
    }
}
