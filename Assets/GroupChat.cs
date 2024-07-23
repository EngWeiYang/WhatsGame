using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GroupChat : MonoBehaviour
{
    public Button[] contactButtons; // Assign these in the Inspector
    public TMP_Text[] selectedMembersTexts;
    private List<string> chatMembersNames = new List<string> { "Auntie Anne", "Uncle Tom" };
    private List<string> selectedMembers = new List<string>();

    private void Start()
    {
        // Initialize contact buttons
        for (int i = 0; i < contactButtons.Length; i++)
        {
            int index = i; // Cache index for lambda
            contactButtons[i].onClick.AddListener(() => OnContactButtonClick(index));
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

    private void UpdateSelectedMembersUI()
    {
        // Update UI for selected members
        for (int i = 0; i < selectedMembersTexts.Length; i++)
        {
            if (i < selectedMembers.Count)
            {
                selectedMembersTexts[i].text = selectedMembers[i];
                selectedMembersTexts[i].gameObject.SetActive(true);
            }
            else
            {
                selectedMembersTexts[i].gameObject.SetActive(false);
            }
        }
    }

}
