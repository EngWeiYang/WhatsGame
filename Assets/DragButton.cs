using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragButton : MonoBehaviour, IDragHandler
{
    private Vector2 startDragPosition;
    private bool isRecording = false;
    private bool isDraggedLeft = false;
    private bool isLocked = false;
    public float lockThresholdY = 200f;
    public float cancelThresholdX = -400f;
    private float elapsedTime = 0f;
    private float elapsedTimeLocked = 0f;
    public GameObject defaultRecordingState;
    public GameObject activeRecordingState;
    public GameObject fireFlyClickOnRecordingIcon;
    public GameObject fireFlyExplanation;
    public GameObject stateRecordVoice;
    public GameObject stateLocked;
    public Vector2 lockPosition;
    public TMP_Text timer;
    public TMP_Text lockedTimer;

    void Start()
    {
        // Initialize the start position
        startDragPosition = transform.position;
    }

    // Called when the pointer is dragged
    public void OnDrag(PointerEventData eventData)
    {
        if (isRecording)
        {
            Vector2 currentDragPosition = eventData.position;
            Vector2 dragDelta = currentDragPosition - startDragPosition;

            // Check if dragged to the left beyond the threshold
            if (dragDelta.x < cancelThresholdX)
            {
                isDraggedLeft = true;
                StopRecording();
                ResetUI();
                Debug.Log("Recording cancelled due to drag left");
            }
            // Check if dragged upwards beyond the lock threshold
            else if (dragDelta.y > lockThresholdY)
            {
                //LockRecording();
                Debug.Log("Recording locked in position");
            }
            // Allow dragging freely before locking
            else if (!isLocked)
            {
                transform.position = currentDragPosition;
            }
        }
    }
    private void StopRecording()
    {
        isRecording = false;

        if (isDraggedLeft)
        {
            Debug.Log("Recording cancelled");
        }
        else if (!isLocked)
        {
            Debug.Log("Recording finished");
            ResetUI();
        }
    }
    private void ResetUI()
    {
        defaultRecordingState.SetActive(true);
        activeRecordingState.SetActive(false);
        fireFlyClickOnRecordingIcon.SetActive(true);
        fireFlyExplanation.SetActive(false);
        timer.text = "00:00";
        lockedTimer.text = "00:00";
        transform.position = startDragPosition;
        isDraggedLeft = false; // Reset the dragged left flag
        isLocked = false; // Reset the locked state
        elapsedTime = 0f; // Reset elapsed time
    }
}
