using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GroupChat : MonoBehaviour
{
    public Button[] contactButtons; // Assign these in the Inspector
    public TMP_Text[] selectedMembersTexts;
    public GameObject[] popUpContacts;
    public GameObject addGroupDetails;
    public GameObject addMembers;
    public Button submitButton; // Assign this in the Inspector for the submit button
    private List<string> chatMembersNames = new List<string> { "Auntie Anne", "Uncle Tom"};
    private List<string> selectedMembers = new List<string>();
    public TMP_Text[] targetScreenTexts;

    private void Start()
    {
        // Initialize contact buttons
        for (int i = 0; i < contactButtons.Length; i++)
        {
            int index = i; // Cache index for lambda
            contactButtons[i].onClick.AddListener(() => OnContactButtonClick(index));
        }
        
        // Initialize submit button
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(OnSubmitButtonClick);
        }

        UpdateSelectedMembersUI();
    }

    private void OnContactButtonClick(int index)
    {
        string memberName = chatMembersNames[index];
        if (selectedMembers.Contains(memberName))
        {
            selectedMembers.Remove(memberName);
        }
        else
        {
            selectedMembers.Add(memberName);
        }
        UpdateSelectedMembersUI();
    }


    //This whole chunk of code firstly disable the popUpUI contacts and the text for them.
    //Secondly, when I click the button to add either Uncle Tom or Aunty annie, it will enable the gameobject with the text of either aunty annie or uncle tom
    //Lastly, I added onclick functions to the default names so that when i press either uncle tom or aunty annie, it will disable the default names and enable the selected names
    //A few issues, since it is only like thru an index, when i click uncle tom first, it will only show the gameobject and not the text with his name. it is only when I click aunty annie then it will display it
    //Another one is that when both contacts are removed, the selected names are not disabled and the default names are not enabled.
    private void UpdateSelectedMembersUI()
    {
        foreach (var popUpContact in popUpContacts)
        {
            popUpContact.SetActive(false);
        }

        foreach (var text in selectedMembersTexts)
        {
            text.gameObject.SetActive(false);
        }
        // Update UI for selected members
        for (int i = 0; i < selectedMembers.Count; i++)
        {
            if (i < selectedMembersTexts.Length)
            {
                selectedMembersTexts[i].text = selectedMembers[i];
                selectedMembersTexts[i].gameObject.SetActive(true);

                // Find the index of the member name in chatMembersNames
                int contactIndex = chatMembersNames.IndexOf(selectedMembers[i]);
                if (contactIndex >= 0 && contactIndex < popUpContacts.Length)
                {
                    popUpContacts[contactIndex].SetActive(true);
                }
            }
        }
    }

    private void OnSubmitButtonClick()
    {
        if (selectedMembers.Count == 2)
        {
            // Call the UpdateDynamicTexts method
            UpdateDynamicTexts(selectedMembers);
            addMembers.SetActive(false);
            addGroupDetails.SetActive(true);
        }
        else
        {
            Debug.Log("You must select exactly 2 contacts to proceed.");
        }
    }

    // Update the texts on the next screen with the selected members' names
    void UpdateDynamicTexts(List<string> selectedMembers)
    {
        for (int i = 0; i < targetScreenTexts.Length; i++)
        {
            if (i < selectedMembers.Count)
            {
                targetScreenTexts[i].text = selectedMembers[i];
            }
            else
            {
                targetScreenTexts[i].text = string.Empty; // Clear text if not enough members
            }
        }
    }
}



