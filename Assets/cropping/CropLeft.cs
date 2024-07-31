using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform bottomLeftHandle;
    public Transform topLeftHandle;
    public Transform tophandle;
    public Transform bottomhandle;
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
    private float originalXPos = -428f;
    private float difference;

    private void Awake()
    {
        handleLeftRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cropBoxRectTransform = cropBox.GetComponent<RectTransform>();
        bottomHandleRectTransform = bottomLeftHandle.GetComponent<RectTransform>();
        bottomLeftHandleRectTransform = bottomLeftHandle.GetComponent<RectTransform>();
        topLeftHandleRectTransform = topLeftHandle.GetComponent<RectTransform>();   
        topHandleRectTransform = tophandle.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;

        heightManager.width = cropBoxRectTransform.sizeDelta.x;
        originalXPos = handleLeftRectTransform.localPosition.x;
        heightManager.originalXPosCropBox = cropBoxRectTransform.localPosition.x;
    }

    private void Update()
    {
        if (dragStarted)
        {
            //Position
            cropBoxRectTransform.localPosition = new Vector3(heightManager.originalXPosCropBox + difference / 2, cropBoxRectTransform.anchoredPosition.y, 0);

            //Scale
            cropBoxRectTransform.sizeDelta = new Vector2(heightManager.width - difference, cropBoxRectTransform.sizeDelta.y);

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

        handleLeftRectTransform.anchoredPosition = new Vector2(localMousePosition.x, handleLeftRectTransform.anchoredPosition.y);
    }
}
