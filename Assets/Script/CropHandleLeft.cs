using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropHandleLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
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

    public HeightCropManager heightCropManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = -334;
    private float maxValue = 334;
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
        heightCropManager.originalXPosCropBox = cropBoxRectTransform.anchoredPosition.x;
        originalXPos = handleLeftRectTransform.anchoredPosition.x;
        Debug.Log(originalXPos);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;
        heightCropManager.reScaleTopBottomHandleWidth = topHandleRectTransform.sizeDelta.x;
        heightCropManager.reScaleTopBottomHandleWidth = bottomHandleRectTransform.sizeDelta.x;
        heightCropManager.width = cropBoxRectTransform.sizeDelta.x;
    }

    private void Update()
    {
        if (dragStarted)
        {
            //Position
            cropBoxRectTransform.anchoredPosition = new Vector3(heightCropManager.originalXPosCropBox + difference / 2, cropBoxRectTransform.anchoredPosition.y, 0);
            topHandleRectTransform.anchoredPosition = new Vector3(heightCropManager.originalXPosCropBox + difference / 2, topHandleRectTransform.anchoredPosition.y, 0);
            bottomHandleRectTransform.anchoredPosition = new Vector3(heightCropManager.originalXPosCropBox + difference / 2, bottomHandleRectTransform.anchoredPosition.y, 0);

            //Scale
            cropBoxRectTransform.sizeDelta = new Vector2(heightCropManager.width - difference, cropBoxRectTransform.sizeDelta.y);
            topHandleRectTransform.sizeDelta = new Vector2(heightCropManager.reScaleTopBottomHandleWidth - difference, topHandleRectTransform.sizeDelta.y);
            bottomHandleRectTransform.sizeDelta = new Vector2(heightCropManager.reScaleTopBottomHandleWidth - difference, bottomHandleRectTransform.sizeDelta.y);

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

        handleLeftRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(localMousePosition.x, -334, maxValue), handleLeftRectTransform.anchoredPosition.y);

    }
}
