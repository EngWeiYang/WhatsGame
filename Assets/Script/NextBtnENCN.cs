using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextBtnENCN : MonoBehaviour
{
    public GameObject nextBtnEn;
    public GameObject nextBtnCn;
    public GameObject levelSelectCanvas;
    public GameObject mainMenu;
    public GameObject playButton;
    public Button playBtn;

    public void Start()
    {
        playBtn.onClick.AddListener(PlayBtn);
    }
    
    private void PlayBtn()
    {
        if (Checker.isEnglish)
        {
            nextBtnEn.SetActive(true);
            nextBtnCn.SetActive(false);
            levelSelectCanvas.SetActive(true);
            mainMenu.SetActive(false);
            playButton.SetActive(false);
        }
        else
        {
            nextBtnEn.SetActive(false);
            nextBtnCn.SetActive(true);
            levelSelectCanvas.SetActive(true);
        }
    }
}
