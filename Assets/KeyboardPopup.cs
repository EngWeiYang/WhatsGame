using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;

public class KeyboardPopup : MonoBehaviour
{
    public GameObject textViewArea;
    public GameObject textView;
    private Text textViewText;

    private bool isDeviceMobile;

    #region WebGL is on mobile check

    [DllImport("__Internal")]
    private static extern bool IsMobile();

    public bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            return IsMobile();
#endif
        return false;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        textViewText = textView.GetComponent<Text>();

        if (isMobile() == true)
        {
            isDeviceMobile = true;
        }
        else
        {
            isDeviceMobile = false;
        }
    }

    public void UpdateTextView(string text)
    {
        if (isDeviceMobile == true)
        {
            //Update the text view using updated string text
            textViewText.text = text;
        }
    }

    public void EnableTextViewArea()
    {
        if (isDeviceMobile == true)
        {
            Debug.Log("Enabled");

            textViewText.text = EventSystem.current.currentSelectedGameObject.GetComponent<InputField>().text;

            textViewArea.SetActive(true);
        }
    }

    public void DisableTextViewArea()
    {
        if (isDeviceMobile == true)
        {
            Debug.Log("Disabled");

            textViewArea.SetActive(false);
        }
    }
}
