using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class VoiceMessageLevel : MonoBehaviour, IPointerDownHandler
{
    public GameObject recordButton;
    public GameObject currentlyRecordingButton;
    public GameObject fireFlyClickOnRecordingIcon;
    public GameObject fireFlyExplanation;

    public void OnPointerDown(PointerEventData eventData)
    {
        recordButton.SetActive(false);
        currentlyRecordingButton.SetActive(true);
        fireFlyClickOnRecordingIcon.SetActive(false);
        fireFlyExplanation.SetActive(true);
    }

}
