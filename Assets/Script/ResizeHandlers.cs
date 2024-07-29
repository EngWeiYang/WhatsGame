using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResizeHandlers : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public RectTransform cropArea; // The crop area to resize
    public RectTransform photoDisplay; // The photo display to ensure boundaries
    public bool maintainAspectRatio = false; // Option to maintain aspect ratio
    public float aspectRatio = 1.0f; // Aspect ratio to maintain if needed

    private Vector2 initialMousePosition;
    private Vector2 initialSize;
    private Vector2 initialPosition;

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

        // Calculate new position based on the change in size
        Vector2 newPosition = initialPosition + new Vector2(mouseDelta.x / 2, -mouseDelta.y / 2);

        // Clamp to photo boundaries
        RectTransform photoRectTransform = photoDisplay.GetComponent<RectTransform>();
        Vector2 photoSize = photoRectTransform.rect.size;

        // Calculate the bounds of the cropArea after resizing
        Vector2 cropAreaMin = newPosition - newSize / 2;
        Vector2 cropAreaMax = newPosition + newSize / 2;

        // Ensure the crop area stays within the photo bounds
        cropAreaMin.x = Mathf.Max(cropAreaMin.x, photoRectTransform.rect.xMin);
        cropAreaMax.x = Mathf.Min(cropAreaMax.x, photoRectTransform.rect.xMax);
        cropAreaMin.y = Mathf.Max(cropAreaMin.y, photoRectTransform.rect.yMin);
        cropAreaMax.y = Mathf.Min(cropAreaMax.y, photoRectTransform.rect.yMax);

        // Update newSize and newPosition based on clamped values
        newSize = cropAreaMax - cropAreaMin;
        newPosition = cropAreaMin + newSize / 2;

        cropArea.sizeDelta = newSize;
        cropArea.anchoredPosition = newPosition;

        // Update initial values for continuous resizing
        initialMousePosition = eventData.position;
        initialSize = newSize;
        initialPosition = newPosition;
    }
}
