using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInstructionManager : MonoBehaviour
{
    // JSON files for instructions and level names (assigned in the Inspector)
    public TextAsset jsonFile_Instructions;
    public TextAsset jsonFile_LevelName;

    // Array to store instructions for the current level
    private Instruction[] levelInstructions;

    // UI elements and canvases for managing instruction and introduction display
    public GameObject fireFlyCanvas;
    public GameObject introduction;
    public GameObject instruction;

    // TextMeshPro UI elements to display scenario titles, intros, and chat bubbles
    public TextMeshProUGUI scenarioIntroTitleTextBox;
    public TextMeshProUGUI scenarioIntroTextBox;
    public TextMeshProUGUI chatBubbleTitle;
    public TextMeshProUGUI chatBubbleBody;

    // List to store level names
    List<LevelName> levelNames;

    // Index to keep track of the current instruction being displayed
    public int currentInstruction = 0;

    private void Start()
    {
        // Initialize UI visibility states
        fireFlyCanvas.SetActive(true);
        introduction.SetActive(true);
        instruction.SetActive(false);

        // Load level names from the JSON file
        LevelsData data = JsonUtility.FromJson<LevelsData>(jsonFile_LevelName.text);
        levelNames = data.levels[0].levelNames;

        // Set the initial chat bubble title to the current level's name in English
        chatBubbleTitle.text = levelNames[LevelSelect.currLevel].En;

        // Load level instructions based on the current level selected
        InstructionsData level = JsonUtility.FromJson<InstructionsData>(jsonFile_Instructions.text);
        levelInstructions = level.instructions
            .Where(instr => instr.levelId == LevelSelect.currLevel) // Filter instructions by the current level
            .ToArray();
    }

    private void Update()
    {
        // Check if there are remaining instructions to display
        if (currentInstruction < levelInstructions.Length)
        {
            Instruction instruction = levelInstructions[currentInstruction]; // Get the current instruction

            // Display different UI elements based on the level and instruction number
            if (LevelSelect.currLevel == 8)  // Special handling for level 8
            {
                if (currentInstruction == 0 || currentInstruction == 4 || currentInstruction == 8 || currentInstruction == 12)
                {
                    // Display scenario introduction or instruction based on the language setting
                    if (Checker.isEnglish)
                    {
                        scenarioIntroTitleTextBox.text = levelNames[LevelSelect.currLevel].En;
                        scenarioIntroTextBox.text = levelInstructions[currentInstruction].En;
                    }
                    else
                    {
                        scenarioIntroTitleTextBox.text = levelNames[LevelSelect.currLevel].Cn;
                        scenarioIntroTextBox.text = levelInstructions[currentInstruction].Cn;
                    }
                }
                else
                {
                    // Display chat bubble with instruction content based on the language setting
                    if (Checker.isEnglish)
                    {
                        chatBubbleTitle.text = levelNames[LevelSelect.currLevel].En;
                        chatBubbleBody.text = levelInstructions[currentInstruction].En;
                    }
                    else
                    {
                        chatBubbleTitle.text = levelNames[LevelSelect.currLevel].Cn;
                        chatBubbleBody.text = levelInstructions[currentInstruction].Cn;
                    }
                }
            }
            else  // For other levels
            {
                if (currentInstruction == 0) // If it's the first instruction, show the scenario intro
                {
                    if (Checker.isEnglish)
                    {
                        scenarioIntroTitleTextBox.text = levelNames[LevelSelect.currLevel].En;
                        scenarioIntroTextBox.text = levelInstructions[currentInstruction].En;
                    }
                    else
                    {
                        scenarioIntroTitleTextBox.text = levelNames[LevelSelect.currLevel].Cn;
                        scenarioIntroTextBox.text = levelInstructions[currentInstruction].Cn;
                    }
                }
                else // Show chat bubble for other instructions
                {
                    if (Checker.isEnglish)
                    {
                        chatBubbleTitle.text = levelNames[LevelSelect.currLevel].En;
                        chatBubbleBody.text = levelInstructions[currentInstruction].En;
                    }
                    else
                    {
                        chatBubbleTitle.text = levelNames[LevelSelect.currLevel].Cn;
                        chatBubbleBody.text = levelInstructions[currentInstruction].Cn;
                    }
                }
            }
        }
        else
        {
            // If there are no more instructions, hide the instruction panel
            instruction.SetActive(false);
        }
    }

    // Method to begin displaying instructions by switching from the introduction screen
    public void BeginLevel()
    {
        introduction.SetActive(false);
        instruction.SetActive(true);
    }

    // Method to move to the next instruction
    public void NextInstruction()
    {
        currentInstruction += 1;
    }

    // Method to move to the previous instruction
    public void PreviousInstruction()
    {
        currentInstruction -= 1;
    }
}
