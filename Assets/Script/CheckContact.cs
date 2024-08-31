using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckContact : MonoBehaviour
{
    public InputField Firstname;
    public InputField Lastname;
    public InputField Phone;
    public GameObject SelectContact;
    public GameObject SelectContactObjectsActive;
    public GameObject HitIndicatorFirstName;
    public GameObject HitIndicatorLastName;
    public GameObject HitIndicatorPhoneText;
    public GameObject DeactivateAddContact;
    public GameObject DeactivateHintIndicatorSelectContact;
    public Text[] dynamicTexts;
    public GameObject ErrorMessageTextFirstName;
    public GameObject ErrorMessageTextLastName;
    public GameObject ErrorMessageTextPhoneText;
    public GameObject HintIndicatorSaveContact;
    public GameObject FireflyStep3;
    public GameObject FireflyStep4;
    private CoroutineManager coroutineManager;
    public GameObject levelCompleteScreen;
    public Animator levelCompleteAnimator;
    public Animator levelCompleteCNAnimator;
    public Button levelCompleteEnable;
    public Button checkInput;
    public LevelInstructionManager levelInstructionManager;
    public SlideAnim slideAnim;
    
    void Start()
    {
        // Set the Phone input field to only accept numeric values
        Phone.contentType = InputField.ContentType.IntegerNumber;
        Phone.characterLimit = 8;
        Firstname.characterLimit = 20;
        Lastname.characterLimit = 20;
        Firstname.contentType = InputField.ContentType.Name;
        Lastname.contentType = InputField.ContentType.Name;
        GameObject coroutineManagerObject = GameObject.Find("CoroutineManager");
        coroutineManager = coroutineManagerObject.GetComponent<CoroutineManager>();

        Firstname.onValueChanged.AddListener(OnFirstNameSelect);
        Lastname.onValueChanged.AddListener(OnLastNameSelect);
        Phone.onValueChanged.AddListener(OnPhoneSelect);

        Firstname.onEndEdit.AddListener(OnFirstNameUnSelect);
        Lastname.onEndEdit.AddListener(OnLastNameUnSelect);
        Phone.onEndEdit.AddListener(OnPhoneUnSelect);

        Firstname.onValueChanged.AddListener(UpdateDynamicTexts);
        Lastname.onValueChanged.AddListener(UpdateDynamicTexts);
        Phone.onValueChanged.AddListener(UpdateDynamicTexts);

        Firstname.onValueChanged.AddListener(CheckAllFieldsFilled);
        Lastname.onValueChanged.AddListener(CheckAllFieldsFilled);
        Phone.onValueChanged.AddListener(CheckAllFieldsFilled);

        levelCompleteEnable.onClick.AddListener(WinScreenAfterInputsAreFilled);
        checkInput.onClick.AddListener(CheckInput);

    }
    public void CheckInput()
    {
        // Check Firstname
        string firstNameText = Firstname.text; // Get the input text and trim any extra spaces
        if (string.IsNullOrEmpty(firstNameText))
        {
            Debug.Log("Firstname is empty");
            ErrorMessageTextFirstName.SetActive(true);
            return; // Exit the method if any field is empty
        }

        // Check Lastname
        string lastNameText = Lastname.text; // Get the input text and trim any extra spaces
        if (string.IsNullOrEmpty(lastNameText))
        {
            Debug.Log("Lastname is empty");
            ErrorMessageTextLastName.SetActive(true);
            return; // Exit the method if any field is empty
        }
        
        // Check Phone
        string phoneText = Phone.text; // Get the input text and trim any extra spaces
        if (string.IsNullOrEmpty(phoneText))
        {
            Debug.Log("Phone is empty");
            ErrorMessageTextPhoneText.SetActive(true);
            return; // Exit the method if any field is empty
        }

        SelectContact.SetActive(true);
        SelectContactObjectsActive.SetActive(true);
        DeactivateAddContact.SetActive(false);
        DeactivateHintIndicatorSelectContact.SetActive(false);
        ErrorMessageTextFirstName.SetActive(false);
        ErrorMessageTextLastName.SetActive(false);
        FireflyStep4.gameObject.SetActive(false);
        ErrorMessageTextPhoneText.SetActive(false);
    }

    void OnFirstNameSelect(string text)
    {
        string firstNameText = Firstname.text;
        if (!string.IsNullOrEmpty(firstNameText))
        {

            HitIndicatorFirstName.SetActive(false);
        }
        else
        {
            HitIndicatorFirstName.SetActive(true);
        }

    }

    void OnLastNameSelect(string text)
    {
        string lastNameText = Lastname.text;
        if (!string.IsNullOrEmpty(lastNameText))
        {

            HitIndicatorLastName.SetActive(false);
        }
        else
        {
            HitIndicatorLastName.SetActive(true);
        }
    }

    void OnPhoneSelect(string text)
    {
        string phoneText = Phone.text;
        if (!string.IsNullOrEmpty(phoneText))
        {

            HitIndicatorPhoneText.SetActive(false);
        }
        else
        {
            HitIndicatorPhoneText.SetActive(true);
        }
    }

    void CheckAllFieldsFilled(string newText)
    {
        // Check if all fields are filled
        string firstNameText = Firstname.text;
        string lastNameText = Lastname.text;
        string phoneText = Phone.text;

        if (!string.IsNullOrEmpty(firstNameText) &&
            !string.IsNullOrEmpty(lastNameText) &&
            !string.IsNullOrEmpty(phoneText))
        {
            // All fields are filled, show the hint indicator
            HintIndicatorSaveContact.SetActive(true);
            FireflyStep4.gameObject.SetActive(true);
            FireflyStep3.gameObject.SetActive(false);

        }
        else
        {
            // Not all fields are filled, hide the hint indicator
            HintIndicatorSaveContact.SetActive(false);
            FireflyStep4.gameObject.SetActive(false);
            FireflyStep3.gameObject.SetActive(true);
        }
    }

    void WinScreenAfterInputsAreFilled()
    {
        string firstNameText = Firstname.text;
        string lastNameText = Lastname.text;
        string phoneText = Phone.text;

        if (!string.IsNullOrEmpty(firstNameText) &&
            !string.IsNullOrEmpty(lastNameText) &&
            !string.IsNullOrEmpty(phoneText))
        {
            // Enable the level complete screen and trigger the animatio
            coroutineManager.StartManagedCoroutine(WinScreenCoroutine());
            slideAnim.TriggerSlideAnimation();
            levelInstructionManager.NextInstruction();
        }
    }
    IEnumerator WinScreenCoroutine()
    {
        // Wait for the animation to finish (assumes the animation length is 2 seconds)
        yield return new WaitForSeconds(1f);
        levelCompleteScreen.SetActive(true);
        levelCompleteAnimator.SetTrigger("LevelCompleted");
        levelCompleteCNAnimator.SetTrigger("LevelCompleted");

    }
    void OnFirstNameUnSelect(string text)
    {
        string firstNameText = Firstname.text;
        if (!string.IsNullOrEmpty(firstNameText))
        {
            
            HitIndicatorFirstName.SetActive(false);
        }
        else
        {
            
            HitIndicatorFirstName.SetActive(true);
            
        }
    }

    void OnLastNameUnSelect(string text)
    {
        string lastNameText = Lastname.text;
        if (!string.IsNullOrEmpty(lastNameText))
        {
            
            HitIndicatorLastName.SetActive(false);
        }
        else
        {
           
            HitIndicatorLastName.SetActive(true);
            
        }
        
    }

    void OnPhoneUnSelect(string text)
    {
        string phoneText = Phone.text;
        if (!string.IsNullOrEmpty(phoneText))
        {
            
            HitIndicatorPhoneText.SetActive(false);
        }
        else
        {
            
            HitIndicatorPhoneText.SetActive(true);
           
        }
        
    }

    void UpdateDynamicTexts(string newText)
    {
        string fullText = $"{Firstname.text} {Lastname.text}";
        foreach (var text in dynamicTexts)
        {
            text.text = fullText;
        }
    }
}
