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
    private SlideAnim slideAnim;

    public LevelInstructionManager levelInstructionManager;
    void Start()
    {
        callButton.onClick.AddListener(CallingandAnswering);
        GameObject coroutineManagerObject = GameObject.Find("CoroutineManager");
        coroutineManager = coroutineManagerObject.GetComponent<CoroutineManager>();
        slideAnim = profilepic.GetComponent<SlideAnim>();
    }

    // Update is called once per frame
    void CallingandAnswering()
    {
        fireflyhelp.SetActive(false);
        screenRinging.SetActive(true);
        coroutineManager.StartManagedCoroutine(FromRingingToAnswering());
    }
    IEnumerator FromRingingToAnswering()
    {
        // Wait for the animation to finish (ADD IN AUDIO LATER)
        yield return new WaitForSeconds(5f);
        slideAnim.TriggerSlideAnimation();
        yield return new WaitForSeconds(0.9f);
        screenRinging.SetActive(false);
        screenAnswer.SetActive(true);
        fireflyhelp.SetActive(false);
        fireflyhelp2.SetActive(true);
        levelInstructionManager.NextInstruction();

    }
}
