using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VoiceMessageLevel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    //Storing microphone initial size
    private Vector3 initialScale;

    // UI Elements
    public TMP_Text timer;
    public TMP_Text lockedTimer;
    public TMP_Text sentTimer;
    public Image recordingImage;
    public Image recordingImage2;
    public Image recordingImage3;
    public GameObject HintIndicating;
    public GameObject HintIndicatorDefault;
    public GameObject HintIndicatorLocked;
    public GameObject defaultRecordingState;
    public GameObject sendVoiceMessage;
    public GameObject stateRecordVoice;
    public GameObject stateLocked;
    public Vector2 lockPosition;
    public float lockThresholdY = 200f;
    public float cancelThresholdX = -400f;
    public RectTransform phoneSizeBoundary;
    public List<Image> imagesToChangeTransparency;
    public TMP_Text textToDisable;
    public Button sendBtn;
    public GameObject fireflyHelp1;
    public GameObject fireflyHelp2;
    public GameObject fireflyHelp3;

    // Internal State
    private Coroutine recordingCoroutine;
    private Vector2 startDragPosition;
    private bool isRecording = false;
    private bool isDraggedLeft = false;
    private bool isLocked = false;
    private bool isHorizontalDrag = false;
    private bool isVerticalDrag = false;
    private float elapsedTime = 0f;
    private const float returnThreshold = 15f;
    private Vector2 initialButtonPosition;

    public LevelInstructionManager levelInstructionManager;

    private bool isCalled = false;
    public ShrinkWithDrag objectToShrink;

    void Start()
    {
        isCalled = false;
        ActivateDefaultState(); // Ensure initial state is set to default
        HintIndicating.SetActive(false);
        sendBtn.onClick.AddListener(SendMessage);
        fireflyHelp1.gameObject.SetActive(true);
        initialButtonPosition = GetComponent<RectTransform>().anchoredPosition;

        // Store the initial scale of the microphone button
        initialScale = transform.localScale;
    }

    // Called when the pointer is pressed down
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isRecording)
        {
            startDragPosition = eventData.position;
            StartRecording();
        }
        isHorizontalDrag = false;
        isVerticalDrag = false;

        levelInstructionManager.NextInstruction();
    }

    // Called when the pointer is released
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isRecording)
        {
            if (!isLocked)
            {
                levelInstructionManager.PreviousInstruction();
                StopRecording();
            }
            else
            {
                Debug.Log("Recording continues in locked position");
            }
        }
        ActivateDefaultState(); // Switch back to default recording state
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

            // Scale the microphone as it's dragged upward
            if (dragDelta.y > 0) // Only scale down when dragging up
            {
                float scaleFactor = Mathf.Max(0.5f, 1 - (dragDelta.y / lockThresholdY) * 0.5f); // Scale between 1 and 0.5
                transform.localScale = initialScale * scaleFactor;

                // Adjust the height of the other object
                objectToShrink.AdjustHeight(dragDelta.y);
            }

            // Check if dragged to the left beyond the threshold
            if (dragDelta.x < cancelThresholdX)
            {
                levelInstructionManager.PreviousInstruction();
                isDraggedLeft = true;
                StopRecording();
                Debug.Log("Recording cancelled due to drag left");
            }
            // Check if dragged upwards beyond the lock threshold
            else if (dragDelta.y > lockThresholdY)
            {
                if (!isCalled)
                {
                    isCalled = true;
                    levelInstructionManager.NextInstruction();
                }
                LockRecording();
                Debug.Log("Recording locked in position");
            }
            else if (!isLocked)
            {
                // Calculate the new position with constraints
                Vector2 newPosition = startDragPosition + dragDelta;

                RectTransform rectTransform = GetComponent<RectTransform>();
                float buttonWidth = rectTransform.rect.width;
                float buttonHeight = rectTransform.rect.height;

                RectTransform boundaryRectTransform = phoneSizeBoundary.GetComponent<RectTransform>();
                Vector2 boundaryMin = boundaryRectTransform.TransformPoint(boundaryRectTransform.rect.min);
                Vector2 boundaryMax = boundaryRectTransform.TransformPoint(boundaryRectTransform.rect.max);

                // Clamp the position to prevent moving out of the screen bounds
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
            }
        }
    }

    // Start the recording process
    private void StartRecording()
    {
        isRecording = true;
        isDraggedLeft = false;
        fireflyHelp2.gameObject.SetActive(true);
        fireflyHelp1.gameObject.SetActive(false);
        isLocked = false;
        elapsedTime = 0f;
        recordingCoroutine = StartCoroutine(UpdateTimer(timer));
        ActivateRecordingState();// Switch to active recording state
        HintIndicating.SetActive(true);
        //Debug.Log("Recording started");
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
            ResetUI();
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
        fireflyHelp2.gameObject.SetActive(false);
        fireflyHelp3.gameObject.SetActive(true);
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
        HintIndicating.SetActive(false);
        fireflyHelp1.gameObject.SetActive(true);
        fireflyHelp2.gameObject.SetActive(false);
        fireflyHelp3.gameObject.SetActive(false);
        ActivateDefaultState(); // Switch to default recording state
        isDraggedLeft = false; // Reset the dragged left flag
        isLocked = false; // Reset the locked state
        elapsedTime = 0f; // Reset elapsed time
        ResetButtonPosition(); // Reset the button position

        // Reset the scale of the microphone button to its original scale
        transform.localScale = initialScale;

        // Reset the height of the other object
        objectToShrink.ResetHeight();
    }

    private void StopRecordingCoroutine()
    {
        if (recordingCoroutine != null)
        {
            StopCoroutine(recordingCoroutine);
            recordingCoroutine = null;
        }
    }

    private void ActivateDefaultState()
    {
        // Adjust transparency for images and TMP texts within the default recording state
        SetTransparencyForAllImagesAndText(defaultRecordingState, 1f);
        SetTransparencyForAllImagesAndText(stateRecordVoice, 0f);
        //Debug.Log("Activated Default State");
    }

    private void ActivateRecordingState()
    {
        // Adjust transparency for images and TMP texts within the active recording state
        SetTransparencyForAllImagesAndText(defaultRecordingState, 0f);
        SetTransparencyForAllImagesAndText(stateRecordVoice, 1f);
        //Debug.Log("Activated Recording State");
    }

    private void SetTransparencyForAllImagesAndText(GameObject obj, float alpha)
    {
        // Set transparency for all Image components
        Image[] images = obj.GetComponentsInChildren<Image>();
        foreach (var img in images)
        {
            SetTransparency(img, alpha);
        }

        // Set transparency for all TMP_Text components
        TMP_Text[] texts = obj.GetComponentsInChildren<TMP_Text>();
        foreach (var txt in texts)
        {
            SetTextTransparency(txt, alpha);
        }
    }

    private void SetTransparency(Image img, float alpha)
    {
        if (img != null)
        {
            Color color = img.color;
            color.a = alpha;
            img.color = color;
        }
    }

    private void SetTextTransparency(TMP_Text text, float alpha)
    {
        if (text != null)
        {
            Color color = text.color;
            color.a = alpha;
            text.color = color;
        }
    }

    public void SendMessage()
    {
        sendVoiceMessage.SetActive(true);
        fireflyHelp1.gameObject.SetActive(false);
        fireflyHelp2.gameObject.SetActive(false);
        fireflyHelp3.gameObject.SetActive(false);
        stateLocked.SetActive(false);
        SetTransparencyForAllImagesAndText(stateRecordVoice, 0f);
        DisableTMPText(textToDisable);
        SetTransparencyForMultipleImages(imagesToChangeTransparency, 0f);
        SetTransparencyForAllImagesAndText(defaultRecordingState, 1f);
        HintIndicatorLocked.SetActive(false);
        HintIndicatorDefault.SetActive(false);
        sentTimer.text = lockedTimer.text;
        timer.text = "00:00";
        lockedTimer.text = "00:00";
    }

    private void SetTransparencyForMultipleImages(List<Image> images, float alpha)
    {
        foreach (var img in images)
        {
            SetTransparency(img, alpha);
        }
    }

    private void DisableTMPText(TMP_Text tmpText)
    {
        if (tmpText != null)
        {
            tmpText.enabled = false;
        }
    }

    private void ResetButtonPosition()
    {
        GetComponent<RectTransform>().anchoredPosition = initialButtonPosition;
        GetComponent<Button>().interactable = true;
    }
}
