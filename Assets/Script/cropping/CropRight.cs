using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform topHandle;
    public Transform bottomHandle;
    public Transform topRightHandle;
    public Transform bottomRightHandle;
    private RectTransform cropBoxRectTransform;
    private RectTransform topRightHandleRectTransform;
    private RectTransform handleRightRectTransform;
    private RectTransform bottomRightHandleRectTransform;
    private RectTransform topHandleRectTransform;
    private RectTransform bottomHandleRectTransform;
    private Canvas canvas;

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = 420;
    private float minValue = -420;
    private float difference;

    private void Awake()
    {
        handleRightRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cropBoxRectTransform = cropBox.GetComponent<RectTransform>();
        topRightHandleRectTransform = topRightHandle.GetComponent<RectTransform>();
        bottomRightHandleRectTransform = bottomRightHandle.GetComponent<RectTransform>();
        topHandleRectTransform = topHandle.GetComponent<RectTransform>();
        bottomHandleRectTransform = bottomHandle.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;
        heightManager.originalXPosCropBox = cropBoxRectTransform.anchoredPosition.x;
        originalXPos = handleRightRectTransform.anchoredPosition.x;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;
        heightManager.reScaleTopBottomHandleWidth = topHandleRectTransform.sizeDelta.x;
        heightManager.reScaleTopBottomHandleWidth = bottomHandleRectTransform.sizeDelta.x;
        heightManager.width = cropBoxRectTransform.sizeDelta.x;
        
    }

    private void Update()
    {
        if (dragStarted)
        {
            //Position
            cropBoxRectTransform.anchoredPosition = new Vector3(heightManager.originalXPosCropBox + difference / 2, cropBoxRectTransform.anchoredPosition.y, 0);
            topHandleRectTransform.anchoredPosition = new Vector3(heightManager.originalXPosCropBox + difference / 2, topHandleRectTransform.anchoredPosition.y,  0);
            bottomHandleRectTransform.anchoredPosition = new Vector3(heightManager.originalXPosCropBox + difference / 2, bottomHandleRectTransform.anchoredPosition.y,  0);

            //Scale
            cropBoxRectTransform.sizeDelta = new Vector2(heightManager.width + difference, cropBoxRectTransform.sizeDelta.y);
            topHandleRectTransform.sizeDelta = new Vector2(heightManager.reScaleTopBottomHandleWidth + difference, topHandleRectTransform.sizeDelta.y);
            bottomHandleRectTransform.sizeDelta = new Vector2(heightManager.reScaleTopBottomHandleWidth + difference, bottomHandleRectTransform.sizeDelta.y);

            difference = -(originalXPos - handleRightRectTransform.anchoredPosition.x);

            bottomRightHandleRectTransform.anchoredPosition = new Vector2(handleRightRectTransform.anchoredPosition.x, bottomHandleRectTransform.anchoredPosition.y);
            topRightHandleRectTransform.anchoredPosition = new Vector2(handleRightRectTransform.anchoredPosition.x, topHandleRectTransform.anchoredPosition.y);
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

        handleRightRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(localMousePosition.x, minValue, 420), handleRightRectTransform.anchoredPosition.y);

       
    }
}
