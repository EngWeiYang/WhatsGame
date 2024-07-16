using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//Logic flow
//Player will start at the Chat page
//Player will need to press and hold the record button. Firefly will explain the other button shown
//Player can let go after firefly explain finish
//Level Finish 🎉


//Code flow
//Count up timer to show in input box
//Alert pop up when player let go
//Dynamic chat bubble?
//Do a coroutine when button is clicked text show up and recording timer increases
//OnDrag to give the voice recording
public class VoiceMessageLevel : MonoBehaviour, IPointerDownHandler
{
    public GameObject recordButton;
    public GameObject currentlyRecordingButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        recordButton.SetActive(false);
        currentlyRecordingButton.SetActive(true);
    }

}
