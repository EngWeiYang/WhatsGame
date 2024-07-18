using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VoiceMessageActive : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    public TMP_Text timer;
    public TMP_Text lockedTimer;
    public Image recordingImage;
    public Image recordingImage2;
    public GameObject HintIndicating;
    public Image recordingImage3;
    private Coroutine recordingCoroutine;
    private Vector2 startDragPosition;
    private bool isRecording = false;
    private bool isDraggedLeft = false;
    private bool isLocked = false;
    private float elapsedTime = 0f;
    private float elapsedTimeLocked = 0f;
    public GameObject defaultRecordingState;
    public GameObject activeRecordingState;
    public GameObject fireFlyClickOnRecordingIcon;
    public GameObject fireFlyExplanation;
    public GameObject stateRecordVoice;
    public GameObject stateLocked;
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
                LockRecording();
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
        elapsedTime = 0f;
        recordingCoroutine = StartCoroutine(UpdateTimer(timer));
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

    // Lock the recording process
    private void LockRecording()
    {
        SetTransparency(recordingImage, 0f);
        SetTransparency(recordingImage2, 0f);
        HintIndicating.SetActive(false);
        SetTransparency(recordingImage3, 0f);
        stateLocked.SetActive(true);
        isLocked = true;
        StopRecordingCoroutine();
        recordingCoroutine = StartCoroutine(UpdateTimer(lockedTimer));
    }

    // Coroutine to update the timer
    private IEnumerator UpdateTimer(TMP_Text textComponent)
    {
        while (isRecording || isLocked)
        {
            elapsedTime += Time.deltaTime; // Increment elapsed time
            UpdateTimerText(textComponent, elapsedTime);
            yield return null; // Wait for the next frame
        }
    }

    // Update timer text
    private void UpdateTimerText(TMP_Text textComponent, float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F); // Calculate minutes
        int seconds = Mathf.FloorToInt(time % 60F); // Calculate seconds
        textComponent.text = string.Format("{0:00}:{1:00}", minutes, seconds); // Update timer text
    }

    // Reset the UI to the default state
    private void ResetUI()
    {
        SetTransparency(recordingImage, 100f);
        SetTransparency(recordingImage2, 100f);
        SetTransparency(recordingImage3, 100f);
        HintIndicating.SetActive(true);
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

    private void StopRecordingCoroutine()
    {
        if (recordingCoroutine != null)
        {
            StopCoroutine(recordingCoroutine);
            recordingCoroutine = null;
        }
    }
    void SetTransparency(Image img, float alpha)
    {
        if (img != null)
        {
            Color color = img.color;
            color.a = alpha;
            img.color = color;
        }
    }
}
