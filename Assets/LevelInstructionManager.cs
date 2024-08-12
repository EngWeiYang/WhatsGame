using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DialogManager;

public class LevelInstructionManager : MonoBehaviour
{
    public TextAsset jsonFile_Instructions;
    public TextAsset jsonFile_LevelName;// Assign this in the Inspector
    private Instruction[] levelInstructions;

    public GameObject introduction;
    public GameObject instruction;

    public TextMeshProUGUI scenarioIntroTitleTextBox;
    public TextMeshProUGUI scenarioIntroTextBox;
    public TextMeshProUGUI chatBubbleTitle;
    public TextMeshProUGUI chatBubbleBody;

    private void Start()
    {
        introduction.SetActive(true);
        instruction.SetActive(false);

        LevelsData data = JsonUtility.FromJson<LevelsData>(jsonFile_LevelName.text);
        List<LevelName>  levelNames = data.levels[0].levelNames;

        scenarioIntroTitleTextBox.text = levelNames[LevelSelect.currLevel].En;
        chatBubbleTitle.text = levelNames[LevelSelect.currLevel].En;

        InstructionsData level = JsonUtility.FromJson<InstructionsData>(jsonFile_Instructions.text);
        levelInstructions = level.instructions
    .Where(instr => instr.levelId == LevelSelect.currLevel)
    .ToArray();

        foreach (var instruction in levelInstructions)
        {
            Debug.Log($"Instruction ID: {instruction.instructionId}, En: {instruction.En}, Cn: {instruction.Cn}");
        }
    }
}
