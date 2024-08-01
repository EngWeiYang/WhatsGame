using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropBottomLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform leftHandle;
    public Transform topLeftHandle;
    public Transform bottomHandle;
    public Transform bottomRightHandle;
    private RectTransform cropBoxRectTransform;
    private RectTransform handleBottomLeftRectTransform;
    private RectTransform topLeftHandleRectTransform;
    private RectTransform bottomHandleRectTransform;
    private RectTransform bottomRightRectTransform;
    private RectTransform leftHandleRectTransform;
    private Canvas canvas;

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = -414.8619f;
    private float originalYPos = -331.2695f;
    private float differencex;
    private float differencey;

    private void Awake()
    {
        handleBottomLeftRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cropBoxRectTransform = cropBox.GetComponent<RectTransform>();
        leftHandleRectTransform = leftHandle.GetComponent<RectTransform>();
        topLeftHandleRectTransform = topLeftHandle.GetComponent<RectTransform>();
        bottomHandleRectTransform = bottomHandle.GetComponent<RectTransform>();
        bottomRightRectTransform = bottomRightHandle.GetComponent<RectTransform>();
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;

        heightManager.height = cropBoxRectTransform.sizeDelta.y;
        originalYPos = handleBottomLeftRectTransform.localPosition.y;
        heightManager.originalYPosCropBox = cropBoxRectTransform.localPosition.y;

        heightManager.width = cropBoxRectTransform.sizeDelta.x;
        originalXPos = handleBottomLeftRectTransform.localPosition.x;
        heightManager.originalXPosCropBox = cropBoxRectTransform.localPosition.x;
    }

    private void Update()
    {
        if (dragStarted)
        {
            cropBoxRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);

            cropBoxRectTransform.sizeDelta = new Vector2(heightManager.width - differencex, heightManager.height - differencey);

            differencex = -(originalXPos - handleBottomLeftRectTransform.anchoredPosition.x);
            differencey = -(originalYPos - handleBottomLeftRectTransform.anchoredPosition.y);

            bottomHandleRectTransform.anchoredPosition = new Vector2(bottomHandleRectTransform.anchoredPosition.x, handleBottomLeftRectTransform.anchoredPosition.y);
            leftHandleRectTransform.anchoredPosition = new Vector2(handleBottomLeftRectTransform.anchoredPosition.x, leftHandleRectTransform.anchoredPosition.y);
            topLeftHandleRectTransform.anchoredPosition = new Vector2(handleBottomLeftRectTransform.anchoredPosition.x, topLeftHandleRectTransform.anchoredPosition.y);
            bottomRightRectTransform.anchoredPosition = new Vector2(bottomRightRectTransform.anchoredPosition.x, handleBottomLeftRectTransform.anchoredPosition.y);
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

        handleBottomLeftRectTransform.anchoredPosition = new Vector2(localMousePosition.x, localMousePosition.y);
    }
}
