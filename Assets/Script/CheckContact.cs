using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckContact : MonoBehaviour
{
    public TMP_InputField Firstname;
    public TMP_InputField Lastname;
    public TMP_InputField Phone;
    public GameObject SelectContact;
    public GameObject SelectContactObjectsActive;
    public GameObject HitIndicatorFirstName;
    public GameObject HitIndicatorLastName;
    public GameObject HitIndicatorPhoneText;
    public GameObject DeactivateAddContact;
    public Animator firstNameAnimator;
    public Animator lastNameAnimator;
    public Animator phoneAnimator;
    public GameObject DeactivateHintIndicatorSelectContact;
    public TMP_Text[] dynamicTexts;
    public GameObject ErrorMessageTextFirstName;
    public GameObject ErrorMessageTextLastName;
    public GameObject ErrorMessageTextPhoneText;
    public GameObject HintIndicatorSaveContact;
    public GameObject FireflyStep3;
    public GameObject FireflyStep4;
    private CoroutineManager coroutineManager;
    public GameObject levelCompleteScreen;
    public Animator levelCompleteAnimator;
    public Button levelCompleteEnable;
    public GameObject keyboard;
    void Start()
    {
        // Set the Phone input field to only accept numeric values
        Phone.contentType = TMP_InputField.ContentType.IntegerNumber;
        Phone.characterLimit = 8;
        Firstname.characterLimit = 20;
        Lastname.characterLimit = 20;
        Firstname.contentType = TMP_InputField.ContentType.Name;
        Lastname.contentType = TMP_InputField.ContentType.Name;
        GameObject coroutineManagerObject = GameObject.Find("CoroutineManager");
        coroutineManager = coroutineManagerObject.GetComponent<CoroutineManager>();
        Firstname.onSelect.AddListener(OnFirstNameSelect);
        Lastname.onSelect.AddListener(OnLastNameSelect);
        Phone.onSelect.AddListener(OnPhoneSelect);
        Firstname.onDeselect.AddListener(OnFirstNameUnSelect);
        Lastname.onDeselect.AddListener(OnLastNameUnSelect);
        Phone.onDeselect.AddListener(OnPhoneUnSelect);
        Firstname.onValueChanged.AddListener(UpdateDynamicTexts);
        Lastname.onValueChanged.AddListener(UpdateDynamicTexts);
        Phone.onValueChanged.AddListener(UpdateDynamicTexts);
        Firstname.onValueChanged.AddListener(CheckAllFieldsFilled);
        Lastname.onValueChanged.AddListener(CheckAllFieldsFilled);
        Phone.onValueChanged.AddListener(CheckAllFieldsFilled);
        levelCompleteEnable.onClick.AddListener(WinScreenAfterInputsAreFilled);
    }
    public void CheckInput()
    {
        // Check Firstname
        string firstNameText = Firstname.text.Trim(); // Get the input text and trim any extra spaces
        if (string.IsNullOrEmpty(firstNameText))
        {
            Debug.Log("Firstname is empty");
            ErrorMessageTextFirstName.SetActive(true);
            return; // Exit the method if any field is empty
        }

        // Check Lastname
        string lastNameText = Lastname.text.Trim(); // Get the input text and trim any extra spaces
        if (string.IsNullOrEmpty(lastNameText))
        {
            Debug.Log("Lastname is empty");
            ErrorMessageTextLastName.SetActive(true);
            return; // Exit the method if any field is empty
        }
        
        // Check Phone
        string phoneText = Phone.text.Trim(); // Get the input text and trim any extra spaces
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
        keyboard.gameObject.SetActive(false);
    }

    //private void ClearAllFields()
    //{
    //    Firstname.text = string.Empty; // Clear Firstname field
    //    Lastname.text = string.Empty; // Clear Lastname field
    //    Phone.text = string.Empty; // Clear Phone field
    //}

    void OnFirstNameSelect(string text)
    {
        string firstNameText = Firstname.text.Trim();
        if (string.IsNullOrEmpty(firstNameText))
        {
            firstNameAnimator.SetTrigger("FirstName_Selected");
            HitIndicatorFirstName.SetActive(true);
        }
        
    }

    void OnLastNameSelect(string text)
    {
        string lastNameText = Lastname.text.Trim();
        if (string.IsNullOrEmpty(lastNameText))
        {
            lastNameAnimator.SetTrigger("LastName_Selected");
            HitIndicatorLastName.SetActive(true);
        }
        
    }

    void OnPhoneSelect(string text)
    {
        string phoneText = Phone.text.Trim();
        if (string.IsNullOrEmpty(phoneText))
        {
            phoneAnimator.SetTrigger("Phone_Selected");
            HitIndicatorPhoneText.SetActive(true);
        }
        
    }
    
    void CheckAllFieldsFilled(string newText)
    {
        // Check if all fields are filled
        string firstNameText = Firstname.text.Trim();
        string lastNameText = Lastname.text.Trim();
        string phoneText = Phone.text.Trim();

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
        string firstNameText = Firstname.text.Trim();
        string lastNameText = Lastname.text.Trim();
        string phoneText = Phone.text.Trim();

        if (!string.IsNullOrEmpty(firstNameText) &&
            !string.IsNullOrEmpty(lastNameText) &&
            !string.IsNullOrEmpty(phoneText))
        {
            // Enable the level complete screen and trigger the animatio
            coroutineManager.StartManagedCoroutine(WinScreenCoroutine());
        }
    }
    IEnumerator WinScreenCoroutine()
    {
        // Wait for the animation to finish (assumes the animation length is 2 seconds)
        yield return new WaitForSeconds(1f);
        levelCompleteScreen.SetActive(true);
        levelCompleteAnimator.SetTrigger("LevelCompleted");

    }
    void OnFirstNameUnSelect(string text)
    {
        string firstNameText = Firstname.text.Trim();
        if (!string.IsNullOrEmpty(firstNameText))
        {
            firstNameAnimator.SetBool("HaveText", true);
            HitIndicatorFirstName.SetActive(false);
        }
        else
        {
            firstNameAnimator.SetBool("HaveText", false);
            HitIndicatorFirstName.SetActive(true);
            firstNameAnimator.SetTrigger("FirstName_Unselect");
        }
    }

    void OnLastNameUnSelect(string text)
    {
        string lastNameText = Lastname.text.Trim();
        if (!string.IsNullOrEmpty(lastNameText))
        {
            lastNameAnimator.SetBool("HaveText", true);
            HitIndicatorLastName.SetActive(false);
        }
        else
        {
            lastNameAnimator.SetBool("HaveText", false);
            HitIndicatorLastName.SetActive(true);
            lastNameAnimator.SetTrigger("LastName_Unselect");
        }
        
    }

    void OnPhoneUnSelect(string text)
    {
        string phoneText = Phone.text.Trim();
        if (!string.IsNullOrEmpty(phoneText))
        {
            phoneAnimator.SetBool("HaveText", true);
            HitIndicatorPhoneText.SetActive(false);
        }
        else
        {
            phoneAnimator.SetBool("HaveText", false);
            HitIndicatorPhoneText.SetActive(true);
            phoneAnimator.SetTrigger("Phone_Unselect");
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
