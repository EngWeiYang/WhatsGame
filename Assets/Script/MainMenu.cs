using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelect;
    public void StartButton()
    {
        levelSelect.SetActive(true);
    }
}
