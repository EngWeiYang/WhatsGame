using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    private RectTransform cropBoxRectTransform;
    private RectTransform handleRightRectTransform;
    private Canvas canvas;

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = 428.1f;
    private float difference;

    private void Awake()
    {
        handleRightRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cropBoxRectTransform = cropBox.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;

        heightManager.width = cropBoxRectTransform.sizeDelta.x;
        originalXPos = handleRightRectTransform.localPosition.x;
        heightManager.originalXPosCropBox = cropBoxRectTransform.localPosition.x;
    }

    private void Update()
    {
        if (dragStarted)
        {
            //Position
            cropBoxRectTransform.localPosition = new Vector3(heightManager.originalXPosCropBox + difference / 2, cropBoxRectTransform.anchoredPosition.y, 0);
    
            //Scale
            cropBoxRectTransform.sizeDelta = new Vector2(heightManager.width + difference, cropBoxRectTransform.sizeDelta.y);
    
            difference = -(originalXPos - handleRightRectTransform.anchoredPosition.x);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localMousePosition);

        handleRightRectTransform.anchoredPosition = new Vector2(localMousePosition.x, handleRightRectTransform.anchoredPosition.y);
    }
}
