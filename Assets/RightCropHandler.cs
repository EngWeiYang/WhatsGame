using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightCropHandler : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public RectTransform cropArea; // The crop area to resize
    public RectTransform photoDisplay; // The photo display to ensure boundaries

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
        Vector2 newSize = new Vector2(initialSize.x + mouseDelta.x, initialSize.y);

        // Ensure new size is positive and reasonable
        newSize.x = Mathf.Max(newSize.x, 10f); // Minimum width

        // Clamp to photo boundaries
        RectTransform photoRectTransform = photoDisplay.GetComponent<RectTransform>();
        Vector2 photoSize = photoRectTransform.rect.size;

        // Ensure the crop area stays within the photo bounds
        newSize.x = Mathf.Min(newSize.x, photoSize.x - (cropArea.anchoredPosition.x - initialPosition.x + initialSize.x));

        cropArea.sizeDelta = newSize;
        cropArea.anchoredPosition = initialPosition;

        // Update initial values for continuous resizing
        initialMousePosition = eventData.position;
        initialSize = newSize;
    }
}
