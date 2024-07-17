using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.EventSystems;

public class VoiceMessageActive : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    public TMP_Text timer;
    private Coroutine recordingCoroutine;
    private Vector2 startDragPosition;
    private bool isRecording = false;
    private bool isDraggedLeft = false;
    private bool isLocked = false;
    public GameObject defaultRecordingState;
    public GameObject activeRecordingState;
    public GameObject fireFlyClickOnRecordingIcon;
    public GameObject fireFlyExplanation;
    public Vector2 lockPosition;
    public float lockThresholdY = 200f;
    public float cancelThresholdX = -400f;

    void Start()
    {
        // Initialize the start position
        startDragPosition = transform.position;
    }

    // Called when the pointer is pressed down
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isRecording)
        {
            StartRecording();
        }
    }

    // Called when the pointer is released
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isRecording)
        {
            if (!isLocked)
            {
                StopRecording();
            }
            else
            {
                Debug.Log("Recording continues in locked position");
            }
        }
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
                isLocked = true;
                transform.position = lockPosition; // Lock the button in position
                Debug.Log("Recording locked in position");
            }
            else if (dragDelta.y < lockThresholdY)
            {
                isLocked = false;
                transform.position = currentDragPosition; // Lock the button in position
                Debug.Log("Recording locked in position");
            }
            // Allow dragging freely before locking
            else if (!isLocked)
            {
                transform.position = currentDragPosition;
            }
        }
    }

    // Start the recording process
    private void StartRecording()
    {
        isRecording = true;
        isDraggedLeft = false;
        isLocked = false;
        recordingCoroutine = StartCoroutine(UpdateTimer());
        defaultRecordingState.SetActive(false);
        activeRecordingState.SetActive(true);
        Debug.Log("Recording started");
    }

    // Stop the recording process
    private void StopRecording()
    {
        if (recordingCoroutine != null)
        {
            StopCoroutine(recordingCoroutine);
            recordingCoroutine = null;
        }

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

    // Coroutine to update the timer
    private IEnumerator UpdateTimer()
    {
        float elapsedTime = 0f; // Track elapsed time
        while (isRecording)
        {
            elapsedTime += Time.deltaTime; // Increment elapsed time
            int minutes = Mathf.FloorToInt(elapsedTime / 60F); // Calculate minutes
            int seconds = Mathf.FloorToInt(elapsedTime % 60F); // Calculate seconds
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Update timer text
            yield return null; // Wait for the next frame
        }
    }

    // Reset the UI to the default state
    private void ResetUI()
    {
        defaultRecordingState.SetActive(true);
        activeRecordingState.SetActive(false);
        fireFlyClickOnRecordingIcon.SetActive(true);
        fireFlyExplanation.SetActive(false);
        timer.text = "00:00";
        transform.position = startDragPosition;
        isDraggedLeft = false; // Reset the dragged left flag
        isLocked = false; // Reset the locked state
    }
}
