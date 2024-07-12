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
    void Start()
    {
        // Set the Phone input field to only accept numeric values
        Phone.contentType = TMP_InputField.ContentType.IntegerNumber;
        Phone.characterLimit = 8;

        Firstname.onSelect.AddListener(OnFirstNameSelect);
        Lastname.onSelect.AddListener(OnLastNameSelect);
        Phone.onSelect.AddListener(OnPhoneSelect);
        Firstname.onDeselect.AddListener(OnFirstNameUnSelect);
        Lastname.onDeselect.AddListener(OnLastNameUnSelect);
        Phone.onDeselect.AddListener(OnPhoneUnSelect);

        Firstname.onValueChanged.AddListener(UpdateDynamicTexts);
        Lastname.onValueChanged.AddListener(UpdateDynamicTexts);
        Phone.onValueChanged.AddListener(UpdateDynamicTexts);
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
        ErrorMessageTextPhoneText.SetActive(false);
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
