using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallingWithCoroutine : MonoBehaviour
{
    public GameObject screenRinging;
    public GameObject screenAnswer;
    public GameObject fireflyhelp;
    public GameObject fireflyhelp2;
    public Button callButton;
    private CoroutineManager coroutineManager;
    public GameObject profilepic;

    public LevelInstructionManager levelInstructionManager;

    private ScaleToCenter scale;

    void Start()
    {
        callButton.onClick.AddListener(CallingandAnswering);
        GameObject coroutineManagerObject = GameObject.Find("CoroutineManager");
        coroutineManager = coroutineManagerObject.GetComponent<CoroutineManager>();
        scale = profilepic.GetComponent<ScaleToCenter>();
    }

    void CallingandAnswering()
    {
        fireflyhelp.SetActive(false);
        screenRinging.SetActive(true);
        coroutineManager.StartManagedCoroutine(FromRingingToAnswering());
    }

    private IEnumerator FromRingingToAnswering()
    {
        // Wait for the animation to finish (ADD IN AUDIO LATER)
        yield return new WaitForSeconds(5f);

        // Start scaling to the center of the screen
        scale.StartScaling();

        yield return new WaitForSeconds(0.9f);
        screenRinging.SetActive(false);
        screenAnswer.SetActive(true);
        fireflyhelp.SetActive(false);
        fireflyhelp2.SetActive(true);
        levelInstructionManager.NextInstruction();
    }
}
