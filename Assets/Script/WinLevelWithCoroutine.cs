using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLevelWithCoroutine : MonoBehaviour
{
    public GameObject levelCompleteScreen;
    public Animator levelCompleteAnimator;
    public Button levelCompleteEnable;
    private CoroutineManager coroutineManager;
    // Start is called before the first frame update
    private void Start()
    {
        // Subscribe to the button's onClick event
        levelCompleteEnable.onClick.AddListener(EnableLevelCompletion);
        GameObject coroutineManagerObject = GameObject.Find("CoroutineManager");
        coroutineManager = coroutineManagerObject.GetComponent<CoroutineManager>();
    }

    void EnableLevelCompletion()
    {
        // Enable the level complete screen and trigger the animatio
        coroutineManager.StartManagedCoroutine(WinScreenCoroutine());
    }
    IEnumerator WinScreenCoroutine()
    {
        // Wait for the animation to finish (assumes the animation length is 2 seconds)
        yield return new WaitForSeconds(2.0f);
        levelCompleteScreen.SetActive(true);
        levelCompleteAnimator.SetTrigger("LevelCompleted");
        
    }
}