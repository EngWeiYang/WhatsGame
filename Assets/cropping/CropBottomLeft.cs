using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropBottomLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform leftHandle;
    public Transform topLeftHandle;
    public Transform bottomHandle;
    public Transform topHandle;
    public Transform rightHandle;
    public Transform bottomRightHandle;
    private RectTransform cropBoxRectTransform;
    private RectTransform handleBottomLeftRectTransform;
    private RectTransform topLeftHandleRectTransform;
    private RectTransform bottomHandleRectTransform;
    private RectTransform topHandleRectTransform;
    private RectTransform bottomRightRectTransform;
    private RectTransform leftHandleRectTransform;
    private RectTransform rightHandleRectTransform;
    private Canvas canvas;

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = -420;
    private float maxValueX = 420;
    private float originalYPos = -340;
    private float maxValueY = 340;
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
        topHandleRectTransform = topHandle.GetComponent<RectTransform>();
        rightHandleRectTransform = rightHandle.GetComponent<RectTransform>();
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;
        originalYPos = handleBottomLeftRectTransform.localPosition.y;
        originalXPos = handleBottomLeftRectTransform.localPosition.x;
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;
        heightManager.reScaleTopBottomHandleWidth = bottomHandleRectTransform.sizeDelta.x;
        heightManager.reScaleTopBottomHandleWidth = topHandleRectTransform.sizeDelta.x;
        heightManager.reScaleRightHandleHeight = rightHandleRectTransform.sizeDelta.y;
        heightManager.reScaleRightHandleHeight = leftHandleRectTransform.sizeDelta.y;
        heightManager.height = cropBoxRectTransform.sizeDelta.y;
        heightManager.width = cropBoxRectTransform.sizeDelta.x;
        heightManager.originalYPosCropBox = cropBoxRectTransform.localPosition.y;
        heightManager.originalXPosCropBox = cropBoxRectTransform.localPosition.x;
    }

    private void Update()
    {
        if (dragStarted)
        {
            cropBoxRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            leftHandleRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            rightHandleRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            bottomHandleRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            topHandleRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);

            cropBoxRectTransform.sizeDelta = new Vector2(heightManager.width - differencex, heightManager.height - differencey);
            bottomHandleRectTransform.sizeDelta = new Vector2(heightManager.reScaleTopBottomHandleWidth - differencex, bottomHandleRectTransform.sizeDelta.y);
            topHandleRectTransform.sizeDelta = new Vector2(heightManager.reScaleTopBottomHandleWidth - differencex, topHandleRectTransform.sizeDelta.y);
            leftHandleRectTransform.sizeDelta = new Vector2(leftHandleRectTransform.sizeDelta.x, heightManager.reScaleRightHandleHeight - differencey);
            rightHandleRectTransform.sizeDelta = new Vector2(rightHandleRectTransform.sizeDelta.x, heightManager.reScaleRightHandleHeight - differencey);

            differencex = -(originalXPos - handleBottomLeftRectTransform.anchoredPosition.x);
            differencey = -(originalYPos - handleBottomLeftRectTransform.anchoredPosition.y);

            bottomHandleRectTransform.anchoredPosition = new Vector2(bottomHandleRectTransform.anchoredPosition.x, handleBottomLeftRectTransform.anchoredPosition.y);
            topHandleRectTransform.anchoredPosition = new Vector2(topHandleRectTransform.anchoredPosition.x, topLeftHandleRectTransform.anchoredPosition.y);
            leftHandleRectTransform.anchoredPosition = new Vector2(handleBottomLeftRectTransform.anchoredPosition.x, leftHandleRectTransform.anchoredPosition.y);
            rightHandleRectTransform.anchoredPosition = new Vector2(bottomRightRectTransform.anchoredPosition.x, rightHandleRectTransform.anchoredPosition.y);
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

        handleBottomLeftRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(localMousePosition.x, -420, maxValueX), Mathf.Clamp(localMousePosition.y, -340, maxValueY));
        
        

    }
}
