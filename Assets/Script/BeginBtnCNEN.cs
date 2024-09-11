using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginBtnCNEN : MonoBehaviour
{
    public GameObject beginBtnEn;
    public GameObject beginBtnCn;

    public void Start()
    {
        if (Checker.isEnglish)
        {
            beginBtnEn.SetActive(true);
            beginBtnCn.SetActive(false);
        }
        else
        {
            beginBtnEn.SetActive(false);
            beginBtnCn.SetActive(true);
        }
    }
}
