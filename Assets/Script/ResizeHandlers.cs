using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResizeHandlers : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public RectTransform cropArea; // The crop area to resize
    public bool maintainAspectRatio = false; // Option to maintain aspect ratio
    public float aspectRatio = 1.0f; // Aspect ratio to maintain if needed

    private Vector2 initialMousePosition;
    private Vector2 initialSize;
    private Vector2 initialPosition;

    private void Awake()
    {
        if (GetComponent<RectTransform>() == null)
        {
            Debug.LogError("ResizeHandler requires a RectTransform component.");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        initialMousePosition = eventData.position;
        initialSize = cropArea.sizeDelta;
        initialPosition = cropArea.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mouseDelta = eventData.position - initialMousePosition;

        Vector2 newSize;

        if (maintainAspectRatio)
        {
            // Calculate new size maintaining the aspect ratio
            float widthChange = mouseDelta.x;
            float heightChange = mouseDelta.x / aspectRatio;
            newSize = new Vector2(initialSize.x + widthChange, initialSize.y + heightChange);
        }
        else
        {
            // Calculate new size freely
            newSize = initialSize + new Vector2(mouseDelta.x, -mouseDelta.y);
        }

        // Ensure new size is positive and reasonable
        newSize.x = Mathf.Max(newSize.x, 10f); // Minimum width
        newSize.y = Mathf.Max(newSize.y, 10f); // Minimum height

        cropArea.sizeDelta = newSize;

        // Update initial values for continuous resizing
        initialMousePosition = eventData.position;
        initialSize = newSize;
    }
}
