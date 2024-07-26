using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropMovement : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public RectTransform cropArea; // The crop area to move
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
        cropArea.anchoredPosition = initialPosition + mouseDelta;
    }
}
