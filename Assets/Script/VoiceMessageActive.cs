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
    private const float returnThreshold = 15f;
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
    private bool isHorizontalDrag = false;
    private bool isVerticalDrag = false;
    public RectTransform phoneSizeBoundary;

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

            if (!isHorizontalDrag && !isVerticalDrag)
            {
                if (Mathf.Abs(dragDelta.x) > Mathf.Abs(dragDelta.y))
                {
                    isHorizontalDrag = true;
                }
                else
                {
                    isVerticalDrag = true;
                }
            }

            if (isHorizontalDrag)
            {
                dragDelta.y = 0; // Only allow horizontal movement
            }
            else if (isVerticalDrag)
            {
                dragDelta.x = 0; // Only allow vertical movement
            }

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
                // Calculate the new position with constraints
                Vector2 newPosition = startDragPosition + dragDelta;

                // Get the button's rect transform and its size
                RectTransform rectTransform = GetComponent<RectTransform>();
                float buttonWidth = rectTransform.rect.width;
                float buttonHeight = rectTransform.rect.height;

                RectTransform boundaryRectTransform = phoneSizeBoundary.GetComponent<RectTransform>();
                Vector2 boundaryMin = boundaryRectTransform.TransformPoint(boundaryRectTransform.rect.min);
                Vector2 boundaryMax = boundaryRectTransform.TransformPoint(boundaryRectTransform.rect.max);

                //Clamp the position to prevent moving out of the screen bounds
                newPosition.x = Mathf.Clamp(newPosition.x, boundaryMin.x + buttonWidth / 2, boundaryMax.x - buttonWidth / 2);
                newPosition.y = Mathf.Clamp(newPosition.y, buttonHeight, Screen.height - buttonHeight);

                // Set the clamped position
                transform.position = newPosition;

                // Check if the button is back to its starting position level
                if (Mathf.Abs(transform.position.x - startDragPosition.x) < returnThreshold)
                {
                    transform.position = new Vector2(startDragPosition.x, transform.position.y);
                    isHorizontalDrag = false;
                    isVerticalDrag = false;
                }

                //// Calculate the new position with constraints
                //Vector2 newPosition = startDragPosition + dragDelta;

                //// Get the button's rect transform and its size
                //RectTransform rectTransform = GetComponent<RectTransform>();
                //float buttonWidth = rectTransform.rect.width;
                //float buttonHeight = rectTransform.rect.height;

                //// Get the boundary image's rect transform and its size
                

                //// Clamp the position to prevent moving out of the boundary image
                //newPosition.x = Mathf.Clamp(newPosition.x, boundaryMin.x + buttonWidth / 2, boundaryMax.x - buttonWidth / 2);
                //newPosition.y = Mathf.Clamp(newPosition.y, buttonHeight, Screen.height - buttonHeight);

                //// Set the clamped position
                //transform.position = newPosition;

                //// Check if the button is back to its starting position level
                //if (Mathf.Abs(transform.position.x - startDragPosition.x) < returnThreshold)
                //{
                //    transform.position = new Vector2(startDragPosition.x, startDragPosition.y);
                //    isHorizontalDrag = false;
                //    isVerticalDrag = false;
                //}
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
        SetTransparency(recordingImage, 255f);
        SetTransparency(recordingImage2, 255f);
        SetTransparency(recordingImage3, 255f);
        HintIndicating.SetActive(true);
        activeRecordingState.SetActive(false);
        defaultRecordingState.SetActive(true);
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
