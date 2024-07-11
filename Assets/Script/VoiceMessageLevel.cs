using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;


public class VoiceMessageLevel : MonoBehaviour
{
    //Logic flow
    //Player will start at the Chat page
    //Player will need to press and hold the record button. Firefly will explain the other button shown
    //Player can let go after firefly explain finish
    //Level Finish 🎉


    //Code flow
    //Count up timer to show in input box
    //Alert pop up when player let go
    //Dynamic chat bubble?


    public GameObject recordButton;
    public GameObject currentlyRecordingButton;
    public TMP_Text timer;
    public GameObject firefly;
    public TMP_Text fireflyExplanation;


    
}
