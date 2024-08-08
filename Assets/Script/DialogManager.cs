using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static DialogManager;

public class DialogManager : MonoBehaviour
{

    public static DialogManager Instance { get; private set; }

    // Other existing fields...

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances
        }
    }

    [System.Serializable]
    public class Instruction
    {
        public int instructionId;
        public string En;
        public string Cn;
    }

    [System.Serializable]
    public class Level
    {
        public int levelId;
        public List<Instruction> instructions;
    }   

    [System.Serializable]
    public class LevelsData
    {
        public List<Level> levels;
    }

    private static bool hasShownPopup = false;

    public TextAsset jsonFile;
    public TextMeshProUGUI textMeshPro; 
    public Button actionButton;
    public LevelsData levelsData;
    public GameObject popup;
    //public Button toggleButton;

    private bool showChinese = false;  // Start by showing Chinese

    void Start()
    {
        LoadDialogs();
        DisplayTextByLevelAndId(0, 0);  // Display the first instruction by default

        if (!hasShownPopup)
        {
            ShowPopup();
            hasShownPopup = true;
        }
        else
        {
            DisablePopup();
        }

        if (actionButton != null)
        {
            actionButton.onClick.AddListener(() => OnNextButtonClicked(0, 1)); // Example: Move to the next instruction
        }
    }

    void LoadDialogs()
    {
        if (jsonFile != null)
        {
            string jsonData = jsonFile.text;
            levelsData = JsonUtility.FromJson<LevelsData>(jsonData);
        }
        else
        {
            Debug.LogError("JSON file not assigned!");
        }
    }

    public void DisplayTextByLevelAndId(int levelId, int instructionId)
    {
        Debug.Log($"Attempting to display instruction ID: {instructionId} for level ID: {levelId}");

        if (levelsData != null)
        {
            Level level = levelsData.levels.Find(l => l.levelId == levelId);

            if (level != null)
            {
                Instruction instruction = level.instructions.Find(i => i.instructionId == instructionId);

                if (instruction != null)
                {
                    if (textMeshPro != null)
                    {
                        textMeshPro.text = showChinese ? instruction.Cn : instruction.En;
                        Debug.Log($"Displayed Instruction: {instructionId} - {textMeshPro.text}");
                    }
                    else
                    {
                        Debug.LogError("TextMeshPro component not assigned!");
                    }

                    if (actionButton != null)
                    {
                        actionButton.onClick.RemoveAllListeners(); // Clear previous listeners

                        if (instructionId + 1 < level.instructions.Count)
                        {
                            Debug.Log($"Setting up Next button for instruction ID: {instructionId + 1}");
                            actionButton.onClick.AddListener(() => OnNextButtonClicked(levelId, instructionId + 1));
                        }
                        else
                        {
                            Debug.Log("Last instruction displayed. Disabling popup.");
                            actionButton.onClick.AddListener(() => DisablePopup());
                        }
                    }
                }
                else
                {
                    Debug.LogError($"Instruction with ID '{instructionId}' not found in Level '{levelId}'!");
                }
            }
            else
            {
                Debug.LogError($"Level with ID '{levelId}' not found!");
            }
        }
        else
        {
            Debug.LogError("No levels data found!");
        }
    }

    public void OnNextButtonClicked(int levelId, int instructionId)
    {
        Debug.Log($"Next button clicked. Moving to instruction ID: {instructionId}");
        DisplayTextByLevelAndId(levelId, instructionId);
    }

    void ToggleLanguage()
    {
        showChinese = !showChinese;
        //DisplayTextByLevelAndId(0, 0);  // Re-display the first instruction as an example
    }

    void ShowPopup()
    {
        if (popup != null)
        {
            popup.SetActive(true);  // Show the popup
            Debug.Log("Popup has been shown.");
        }
        else
        {
            Debug.LogError("Popup GameObject not assigned!");
        }
    }

    public void DisablePopup()
    {
        if (popup != null)
        {
            popup.SetActive(false);  // Disables the popup
            Debug.Log("Popup has been disabled.");
        }
        else
        {
            Debug.LogError("Popup GameObject not assigned!");
        }
    }
}
