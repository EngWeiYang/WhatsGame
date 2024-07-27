using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [System.Serializable]
    public class Dialog
    {
        public string id;
        public string en;
        public string zh;
    }

    [System.Serializable]
    public class Dialogs
    {
        public List<Dialog> dialogs;
    }

    public TextAsset jsonFile;
    public TextMeshProUGUI textMeshPro;
    public Dialogs dialogs;
    public Button toggleButton;

    private bool showChinese = true;  // Start by showing Chinese

    void Start()
    {
        LoadDialogs();
        DisplayTextById("test");

        // Add a listener to the toggle button
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(ToggleLanguage);
        }
        else
        {
            Debug.LogError("Toggle button not assigned!");
        }
    }

    void LoadDialogs()
    {
        if (jsonFile != null)
        {
            string jsonData = jsonFile.text;
            dialogs = JsonUtility.FromJson<Dialogs>(jsonData);
            Debug.Log(jsonData);
            foreach (var dialog in dialogs.dialogs)
            {
                Debug.Log($"ID: {dialog.id}, EN: {dialog.en}, ZH: {dialog.zh}");
            }
        }
        else
        {
            Debug.LogError("JSON file not assigned!");
        }
    }

    void DisplayTextById(string id)
    {
        if (dialogs != null)
        {
            Dialog dialog = dialogs.dialogs.Find(d => d.id == id);

            if (dialog != null)
            {
                // Display the text based on the current language setting
                if (textMeshPro != null)
                {
                    textMeshPro.text = showChinese ? dialog.zh : dialog.en;
                }
                else
                {
                    Debug.LogError("TextMeshPro component not assigned!");
                }
            }
            else
            {
                Debug.LogError($"Dialog with ID '{id}' not found!");
            }
        }
        else
        {
            Debug.LogError("No dialogs found!");
        }
    }

    void ToggleLanguage()
    {
        showChinese = !showChinese;
        DisplayTextById("test");
    }
}
