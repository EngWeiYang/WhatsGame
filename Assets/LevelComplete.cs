using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    public GameObject levelCompleteScreen;
    public Animator levelCompleteAnimator;
    public Button levelCompleteEnable;

    private void Start()
    {
        // Subscribe to the button's onClick event
        levelCompleteEnable.onClick.AddListener(EnableLevelCompletion);
    }

    void EnableLevelCompletion()
    {
        // Enable the level complete screen and trigger the animation
        levelCompleteScreen.SetActive(true);
        levelCompleteAnimator.SetTrigger("LevelComplete");
    }
}
