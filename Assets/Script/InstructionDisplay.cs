using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Instruction
{
    public int levelId;
    public int instructionId;
    public string En;
    public string Cn;
}

[System.Serializable]
public class InstructionsData
{
    public Instruction[] instructions;
}

public class InstructionDisplay : MonoBehaviour
{
    public TextMeshProUGUI instructionText; // Assign this in the Inspector
    public Button nextButton; // Assign this in the Inspector
    public TextAsset jsonFile; // Assign this in the Inspector
    public GameObject panel;

    private Instruction[] levelInstructions;
    private int currentInstructionIndex = 0;

    void Start()
    {
        LevelSelect.currLevel = -1;
        // Load and parse JSON data
        InstructionsData level0 = JsonUtility.FromJson<InstructionsData>(jsonFile.text);
        levelInstructions = level0.instructions
    .Where(instr => instr.levelId == LevelSelect.currLevel)
    .ToArray();

        foreach (var instruction in levelInstructions)
        {
            Debug.Log($"Instruction ID: {instruction.instructionId}, En: {instruction.En}, Cn: {instruction.Cn}");
        }

        // Display the first instruction
        DisplayInstruction();

        // Add a listener to the button to call the NextInstruction method
        nextButton.onClick.AddListener(NextInstruction);
    }

    void DisplayInstruction()
    {
        if (currentInstructionIndex < levelInstructions.Length)
        {
            Instruction currentInstruction = levelInstructions[currentInstructionIndex];
            instructionText.text = currentInstruction.En; // Display in English
            // instructionText.text = currentInstruction.Cn; // Uncomment to display in Chinese
        }
        else
        {
            panel.SetActive(false);
        }
    }

    void NextInstruction()
    {
        if (currentInstructionIndex < levelInstructions.Length - 1)
        {
            currentInstructionIndex++;
            DisplayInstruction();
        }
        else
        {
            panel.SetActive(false);
        }
    }
}