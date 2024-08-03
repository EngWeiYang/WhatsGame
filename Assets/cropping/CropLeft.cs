using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform bottomLeftHandle;
    public Transform topLeftHandle;
    public Transform topHandle;
    public Transform bottomHandle;
    private RectTransform cropBoxRectTransform;
    private RectTransform handleLeftRectTransform;
    private RectTransform bottomLeftHandleRectTransform;
    private RectTransform topLeftHandleRectTransform;
    private RectTransform topHandleRectTransform;
    private RectTransform bottomHandleRectTransform;
    private Canvas canvas;

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = -420;
    private float maxValue = 420;
    private float difference;

    private void Awake()
    {
        handleLeftRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cropBoxRectTransform = cropBox.GetComponent<RectTransform>();
        bottomHandleRectTransform = bottomHandle.GetComponent<RectTransform>();
        bottomLeftHandleRectTransform = bottomLeftHandle.GetComponent<RectTransform>();
        topLeftHandleRectTransform = topLeftHandle.GetComponent<RectTransform>();   
        topHandleRectTransform = topHandle.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;

        originalXPos = handleLeftRectTransform.localPosition.x;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;
        heightManager.reScaleTopBottomHandleWidth = topHandleRectTransform.sizeDelta.x;
        heightManager.reScaleTopBottomHandleWidth = bottomHandleRectTransform.sizeDelta.x;
        heightManager.width = cropBoxRectTransform.sizeDelta.x;
        heightManager.originalXPosCropBox = cropBoxRectTransform.localPosition.x;
    }

    private void Update()
    {
        if (dragStarted)
        {
            //Position
            cropBoxRectTransform.localPosition = new Vector3(heightManager.originalXPosCropBox + difference / 2, cropBoxRectTransform.anchoredPosition.y, 0);
            topHandleRectTransform.localPosition = new Vector3(heightManager.originalXPosCropBox + difference / 2, topHandleRectTransform.anchoredPosition.y, 0);
            bottomHandleRectTransform.localPosition = new Vector3(heightManager.originalXPosCropBox + difference / 2, bottomHandleRectTransform.anchoredPosition.y, 0);

            //Scale
            cropBoxRectTransform.sizeDelta = new Vector2(heightManager.width - difference, cropBoxRectTransform.sizeDelta.y);
            topHandleRectTransform.sizeDelta = new Vector2(heightManager.reScaleTopBottomHandleWidth - difference, topHandleRectTransform.sizeDelta.y);
            bottomHandleRectTransform.sizeDelta = new Vector2(heightManager.reScaleTopBottomHandleWidth - difference, bottomHandleRectTransform.sizeDelta.y);

            difference = -(originalXPos - handleLeftRectTransform.anchoredPosition.x);

            bottomLeftHandleRectTransform.anchoredPosition = new Vector2(handleLeftRectTransform.anchoredPosition.x, bottomHandleRectTransform.anchoredPosition.y);
            topLeftHandleRectTransform.anchoredPosition = new Vector2(handleLeftRectTransform.anchoredPosition.x, topHandleRectTransform.anchoredPosition.y);
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

        handleLeftRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(localMousePosition.x, -420, maxValue), handleLeftRectTransform.anchoredPosition.y);
        
    }
}
