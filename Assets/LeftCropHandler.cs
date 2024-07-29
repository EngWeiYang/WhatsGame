using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftCropHandler : MonoBehaviour, IDragHandler, IPointerDownHandler
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
        Vector2 newSize = new Vector2(initialSize.x - mouseDelta.x, initialSize.y);
        Vector2 newPosition = new Vector2(initialPosition.x + mouseDelta.x / 2, initialPosition.y);

        // Ensure new size is positive
        newSize.x = Mathf.Max(newSize.x, 10f);

        // Clamp position and size to ensure it stays within the photo display bounds
        RectTransform photoRectTransform = photoDisplay.GetComponent<RectTransform>();
        Vector2 photoSize = photoRectTransform.rect.size;

        newPosition.x = Mathf.Clamp(newPosition.x, photoRectTransform.rect.xMin + newSize.x / 2, photoRectTransform.rect.xMax - initialSize.x / 2);
        newSize.x = Mathf.Clamp(newSize.x, 10f, photoSize.x - (newPosition.x + initialSize.x / 2 - photoRectTransform.rect.xMin));

        cropArea.sizeDelta = newSize;
        cropArea.anchoredPosition = newPosition;
    }
}
