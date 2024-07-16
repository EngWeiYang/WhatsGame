using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.EventSystems;

public class VoiceMessageActive : MonoBehaviour, IPointerUpHandler, IDragHandler
{
    public TMP_Text timer;
    private Coroutine recordingCoroutine;
    private Vector2 startDragPosition;
    private bool isRecording = false;
    private bool isDraggedLeft = false;

    void OnEnable()
    {
        startDragPosition = transform.position;
        isRecording = true;
        recordingCoroutine = StartCoroutine(UpdateTimer());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isRecording)
        {
            StopCoroutine(recordingCoroutine);
            isRecording = false;

            if (isDraggedLeft)
            {
                // Handle the cancellation of the recording
                Debug.Log("Recording cancelled");
            }
            else
            {
                // Handle the end of the recording
                Debug.Log("Recording finished");
            }

            ResetUI();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isRecording)
        {
            Vector2 currentDragPosition = eventData.position;
            Vector2 dragDelta = currentDragPosition - startDragPosition;

            // Check if dragged to the left
            if (dragDelta.x < -400)
            {
                isDraggedLeft = true;
                StopCoroutine(recordingCoroutine);
                isRecording = false;
                ResetUI();
                Debug.Log("Recording cancelled due to drag left");
            }
            // Allow dragging upwards to a certain distance
            else if (dragDelta.y > 0 && dragDelta.y < 200)
            {
                transform.position = currentDragPosition;
            }
        }
    }

    private IEnumerator UpdateTimer()
    {
        float elapsedTime = 0f;
        while (isRecording)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60F);
            int seconds = Mathf.FloorToInt(elapsedTime % 60F);
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return null;
        }
    }

    private void ResetUI()
    {
        gameObject.SetActive(false);
        timer.text = "00:00";
        transform.position = startDragPosition;
        isDraggedLeft = false; // Reset the dragged left flag
    }
}
