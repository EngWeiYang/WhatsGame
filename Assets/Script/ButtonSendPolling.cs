using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSendPolling : MonoBehaviour
{
    public InputField[] userInputFields; // Array of input fields
    public Text[] dynamicTexts;         // Array of text components to update
    public Button confirmPollingDetails;     // Button to confirm and submit
    private List<string> PollingOptionsAndNames; // List of polling options and names (not used in this version)
    //public GameObject disablePollingDetailsScreen;
    public GameObject pollingText;
    public GameObject levelCompleteScreen;
    public Animator levelCompleteAnimator;
    public Animator levelCompleteCNAnimator;
    public Button levelCompleteEnable;
    private CoroutineManager coroutineManager;
    public GameObject hintIndicatorQuestion;
    public GameObject hintIndicatorOption1;
    public GameObject hintIndicatorOption2;
    public GameObject hintIndicatorbuttonSend;
    public SlideAnim_NoSetInActive slideanim;
    //public GameObject thisButton;

    public MsgMoveUp msgMoveUp;

    public LevelInstructionManager levelInstructionManager;
    private bool isCalled = false;
    
    // Start is called before the first frame update
    void Start()
    {
        hintIndicatorbuttonSend.gameObject.SetActive(false);
        GameObject coroutineManagerObject = GameObject.Find("CoroutineManager");
        coroutineManager = coroutineManagerObject.GetComponent<CoroutineManager>();
        confirmPollingDetails.onClick.AddListener(OnConfirmButtonClick);
        if (userInputFields.Length > 0)
        {
            userInputFields[0].onValueChanged.AddListener(CheckAllFieldsFilled);
            userInputFields[1].onValueChanged.AddListener(CheckAllFieldsFilled);
            userInputFields[2].onValueChanged.AddListener(CheckAllFieldsFilled);

            userInputFields[0].onValueChanged.AddListener(QuestionFieldSelect);
            userInputFields[1].onValueChanged.AddListener(Option1Select);
            userInputFields[2].onValueChanged.AddListener(Option2Select);
        }
        levelCompleteEnable.onClick.AddListener(WinScreenAfterInputsAreFilled);
    }
    private void OnConfirmButtonClick()
    {
        //disablePollingDetailsScreen.SetActive(false);
        pollingText.SetActive(true);
        List<string> userInputs = new List<string>();

        // Collect all user inputs from input fields
        foreach (var inputField in userInputFields)
        {
            if (inputField != null)
            {
                userInputs.Add(inputField.text);
            }
        }

        // Update dynamic texts with the collected user inputs
        UpdateDynamicTexts(userInputs);

        msgMoveUp.PollMoveUp();
    }

    public void UpdateDynamicTexts(List<string> inputs)
    {
        // Update TMP_Text components with user inputs
        for (int i = 0; i < dynamicTexts.Length; i++)
        {
            if (i < inputs.Count)
            {
                dynamicTexts[i].text = inputs[i];
            }
            else
            {
                dynamicTexts[i].text = string.Empty; // Clear text if not enough inputs
            }
        }
    }


    void Option1Select(string text)
    {
        string Option1Text = text;
        if (!string.IsNullOrEmpty(Option1Text))
        {
            hintIndicatorOption1.SetActive(false);
        }
        else
        {
            hintIndicatorOption1.SetActive(true);
        }
        
    }
    
    void Option2Select(string text)
    {
        string Option2Text = text;
        if (!string.IsNullOrEmpty(Option2Text))
        {
            hintIndicatorOption2.SetActive(false);
        }
        else
        {
            hintIndicatorOption2.SetActive(true);
        }
        
    }
    
    void QuestionFieldSelect(string text)
    {
        string questionFieldText = text;
        if (!string.IsNullOrEmpty(questionFieldText))
        {
            hintIndicatorQuestion.SetActive(false);
        }
        else
        {
            hintIndicatorQuestion.SetActive(true);
        }
        
    }
    
    void CheckAllFieldsFilled(string newText)
    {
        // Check if all fields are filled
        string Option1Text = userInputFields[0].text;
        string Option2Text = userInputFields[1].text;
        string questionFieldText = userInputFields[2].text;

        if (!string.IsNullOrEmpty(Option1Text) &&
            !string.IsNullOrEmpty(Option2Text) &&
            !string.IsNullOrEmpty(questionFieldText))
        {
            // All fields are filled, show the hint indicator
            hintIndicatorbuttonSend.SetActive(true);
            
            if (!isCalled)
            {
                levelInstructionManager.NextInstruction();
                isCalled = true;
            }

        }
        else
        {
            // Not all fields are filled, hide the hint indicator
            hintIndicatorbuttonSend.SetActive(false);
            
        }
    }

    void WinScreenAfterInputsAreFilled()
    {
        string Option1Text = userInputFields[0].text;
        string Option2Text = userInputFields[1].text;
        string questionFieldText = userInputFields[2].text;

        if (!string.IsNullOrEmpty(Option1Text) &&
            !string.IsNullOrEmpty(Option2Text) &&
            !string.IsNullOrEmpty(questionFieldText))
        {
            // Enable the level complete screen and trigger the animatio
            coroutineManager.StartManagedCoroutine(WinScreenCoroutine());
            slideanim.TriggerSlideDownAnimation();
            levelInstructionManager.NextInstruction();
        }
    }
    IEnumerator WinScreenCoroutine()
    {
        // Wait for the animation to finish (assumes the animation length is 2 seconds)
        yield return new WaitForSeconds(2f);
        levelCompleteScreen.SetActive(true);
        levelCompleteAnimator.SetTrigger("LevelCompleted");
        levelCompleteCNAnimator.SetTrigger("LevelCompleted");

    }
}
