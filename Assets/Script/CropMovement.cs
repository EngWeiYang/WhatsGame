using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropMovement : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public RectTransform cropArea; // The crop area to move
    public RectTransform photoDisplay; // The photo display area

    private Vector2 initialMousePosition;
    private Vector2 initialPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialMousePosition = eventData.position;
        initialPosition = cropArea.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mouseDelta = eventData.position - initialMousePosition;
        Vector2 newPosition = initialPosition + mouseDelta;

        // Get the boundaries of the photo display area
        Vector2 photoMin = photoDisplay.rect.min;
        Vector2 photoMax = photoDisplay.rect.max;

        // Get the size of the crop area
        Vector2 cropSize = cropArea.rect.size;

        // Calculate the boundaries of the crop area within the photo display
        float minX = photoMin.x + cropSize.x / 2;
        float maxX = photoMax.x - cropSize.x / 2;
        float minY = photoMin.y + cropSize.y / 2;
        float maxY = photoMax.y - cropSize.y / 2;

        // Clamp the new position to ensure the crop area stays within the photo display
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        cropArea.anchoredPosition = newPosition;
    }
}
