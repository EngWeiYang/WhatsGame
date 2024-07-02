using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipController : MonoBehaviour
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStop;
    [SerializeField] RectTransform levelPagesRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    public GameObject RightButton;
    public GameObject LeftButton;

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStop;
            MovePage();
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStop;
            MovePage();
        }
    }

    public void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

    public void Update()
    {
        if (currentPage == 1) 
        {
            LeftButton.SetActive(false);
            RightButton.SetActive(true);
        }
        else if (currentPage == 2)
        {
            LeftButton.SetActive(true);
            RightButton.SetActive(true);
        }
        else if (currentPage == 3) 
        { 
            LeftButton.SetActive(true);
            RightButton.SetActive(false);
        }
    }
}
