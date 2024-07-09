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
    public GameObject DeactivateAddContact;

    void Start()
    {
        // Set the Phone input field to only accept numeric values
        Phone.contentType = TMP_InputField.ContentType.IntegerNumber;
    }
    public void CheckInput()
    {
        // Check Firstname
        string firstNameText = Firstname.text.Trim(); // Get the input text and trim any extra spaces
        if (string.IsNullOrEmpty(firstNameText))
        {
            Debug.Log("Firstname is empty");
            //ClearAllFields();
            return; // Exit the method if any field is empty
        }

        // Check Lastname
        string lastNameText = Lastname.text.Trim(); // Get the input text and trim any extra spaces
        if (string.IsNullOrEmpty(lastNameText))
        {
            Debug.Log("Lastname is empty");
            //ClearAllFields();
            return; // Exit the method if any field is empty
        }

        // Check Phone
        string phoneText = Phone.text.Trim(); // Get the input text and trim any extra spaces
        if (string.IsNullOrEmpty(phoneText))
        {
            Debug.Log("Phone is empty");
            //ClearAllFields();
            return; // Exit the method if any field is empty
        }

        // If all fields are filled, proceed with your logic here
        Debug.Log("All fields are filled. Proceed with your logic.");

        SelectContact.SetActive(true);
        SelectContactObjectsActive.SetActive(true);
        DeactivateAddContact.SetActive(false);
    }

    private void ClearAllFields()
    {
        Firstname.text = string.Empty; // Clear Firstname field
        Lastname.text = string.Empty; // Clear Lastname field
        Phone.text = string.Empty; // Clear Phone field
    }
}
