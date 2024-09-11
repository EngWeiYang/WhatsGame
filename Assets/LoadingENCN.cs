using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingENCN : MonoBehaviour
{
    public GameObject loadEn;
    public GameObject loadCn;

    public void Start()
    {
        if (Checker.isEnglish)
        {
            loadEn.SetActive(true);
            loadCn.SetActive(false);
        }
        else
        {
            loadEn.SetActive(false);
            loadCn.SetActive(true);
        }
    }
}

