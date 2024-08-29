using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Represents a single instruction with level and language-specific text
[System.Serializable]
public class Instruction
{
    public int levelId; // ID of the level the instruction belongs to
    public int instructionId; // ID of the instruction
    public string En; // Instruction text in English
    public string Cn; // Instruction text in Chinese
}

// Represents the collection of instructions
[System.Serializable]
public class InstructionsData
{
    public Instruction[] instructions; // Array of instructions
}

public class InstructionDisplay : MonoBehaviour
{
    // UI elements assigned in the Inspector
    public TextMeshProUGUI instructionText; // Text component to display the instruction
    public Button nextButton; // Button to advance to the next instruction
    public TextAsset jsonFile; // JSON file containing the instructions data
    public GameObject panel; // Panel containing the instruction text and button

    private Instruction[] levelInstructions; // Array to store instructions for the current level
    private int currentInstructionIndex = 0; // Index to track the current instruction being displayed

    void Start()
    {
        // Set the current level to -1 initially (ensure this is set correctly before this point)
        LevelSelect.currLevel = -1;

        // Load and parse JSON data
        InstructionsData level0 = JsonUtility.FromJson<InstructionsData>(jsonFile.text);
        // Filter instructions to include only those for the current level
        levelInstructions = level0.instructions
            .Where(instr => instr.levelId == LevelSelect.currLevel)
            .ToArray();

        // Log instructions for debugging purposes
        foreach (var instruction in levelInstructions)
        {
            Debug.Log($"Instruction ID: {instruction.instructionId}, En: {instruction.En}, Cn: {instruction.Cn}");
        }

        // Add a listener to the button to call the NextInstruction method when clicked
        nextButton.onClick.AddListener(NextInstruction);
    }

    void Update()
    {
        // Display the current instruction
        DisplayInstruction();
    }

    // Displays the current instruction based on the selected language
    void DisplayInstruction()
    {
        if (currentInstructionIndex < levelInstructions.Length)
        {
            Instruction currentInstruction = levelInstructions[currentInstructionIndex];

            // Set instruction text based on the selected language
            if (Checker.isEnglish)
            {
                instructionText.text = currentInstruction.En; // Display instruction in English
            }
            else
            {
                instructionText.text = currentInstruction.Cn; // Display instruction in Chinese
            }
        }
        else
        {
            // Hide the panel if there are no more instructions to display
            panel.SetActive(false);
        }
    }

    // Advances to the next instruction when called
    void NextInstruction()
    {
        if (currentInstructionIndex < levelInstructions.Length - 1)
        {
            currentInstructionIndex++; // Increment the instruction index
            DisplayInstruction(); // Update the display with the new instruction
        }
        else
        {
            // Hide the panel if there are no more instructions
            panel.SetActive(false);
        }
    }
}
