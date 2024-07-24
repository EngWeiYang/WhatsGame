using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;
using TMPro;

public class AddingMembers : MonoBehaviour
{
    public GameObject defaultNames;
    public Animator animator;
    public Button AnnieBtn;
    public Button ThommasBtn;
    public GameObject profilePics;
    public GameObject names;
    public Image[] profile;
    private List<string> chatmembers = new List<string>();
    private int peopleAdded = 0;
    public TMP_Text[] namesRef;
    bool AnnieClicked;
    bool ThommasClicked;
    public GameObject currentScreen;
    public GameObject groupDetailsScreen;
    public TMP_Text[] targetScreenTexts;
    public Button nextButton;

    private void Start()
    {
        animator = defaultNames.GetComponent<Animator>();
        AnnieBtn.onClick.AddListener(updateAnnie);
        ThommasBtn.onClick.AddListener(updateThommas);
        nextButton.onClick.AddListener(OnSubmitButtonClick);
        profile = profilePics.GetComponentsInChildren<Image>();
        Debug.Log(namesRef[0]);
    }

    private void addMember()
    {
        if (peopleAdded == 0)
        {
            animator.SetTrigger("Click");
        }
        peopleAdded += 1;
        //UpdateUI();
        
    }
    private void removeMember()
    {
        peopleAdded -= 1;
        if (peopleAdded == 0)
        {
            animator.SetTrigger("Unclick");
        }
        //UpdateUI();
    }
    private void updateAnnie()
    {
        if(!AnnieClicked)
        {
            AnnieClicked = true;
            addMember();
            chatmembers.Add("Annie");
        }
        else
        {
            AnnieClicked = false;
            removeMember();
            chatmembers.Remove("Annie");
        }
        UpdateUI();
    }
    private void updateThommas()
    {
        if (!ThommasClicked)
        {
            ThommasClicked = true;
            addMember();
            chatmembers.Add("Thommas");
        }
        else
        {
            ThommasClicked = false;
            removeMember();
            chatmembers.Remove("Thommas");
        }
        UpdateUI();
    }
    private void UpdateUI()
    {
        for (int i = 0; i < 2; i++)
        {
            if( i <= peopleAdded - 1)
            {
                Color tempColor = profile[i].color;
                tempColor.a = 1f;
                profile[i].color = tempColor;
            }
            else
            {
                Color tempColor = profile[i].color;
                tempColor.a = 0f;
                profile[i].color = tempColor;
            }
        }
        for (int i = 0;i < 2; i++)
        {
            if( i <= peopleAdded - 1)
            {
                namesRef[i].text = chatmembers[i];
            }
            else
            {
                namesRef[i].text = string.Empty;
            }
        }
        
    }
    private void OnSubmitButtonClick()
    {
        if (chatmembers.Count == 2)
        {
            // Call the UpdateDynamicTexts method
            UpdateDynamicTexts(chatmembers);
            currentScreen.SetActive(false);
            groupDetailsScreen.SetActive(true);
        }
        else
        {
            Debug.Log("You must select exactly 2 contacts to proceed.");
        }
    }

    // Update the texts on the next screen with the selected members' names
    void UpdateDynamicTexts(List<string> chatMembers)
    {
        for (int i = 0; i < targetScreenTexts.Length; i++)
        {
            if (i < chatMembers.Count)
            {
                targetScreenTexts[i].text = chatMembers[i];
            }
            else
            {
                targetScreenTexts[i].text = string.Empty; // Clear text if not enough members
            }
        }
    }
}
