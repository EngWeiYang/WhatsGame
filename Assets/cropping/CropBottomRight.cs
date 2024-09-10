using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropBottomRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform rightHandle;
    public Transform leftHandle;
    public Transform bottomHandle;
    public Transform topHandle;
    public Transform topRightHandle;
    public Transform bottomLeftHandle;
    private RectTransform cropBoxRectTransform;
    private RectTransform rightHandleRectTransform;
    private RectTransform topRightHandleRectTransform;
    private RectTransform bottomLeftHandleRectTransform;
    private RectTransform handleBottomRightRectTransform;
    private RectTransform bottomHandleRectTransform;
    private RectTransform topHandleRectTransform;
    private RectTransform leftHandleRectTransform;
    private Canvas canvas;

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = 420;
    private float minValueX = -420;
    private float originalYPos = -340;
    private float maxValueY = 340;
    private float differencex;
    private float differencey;

    private void Awake()
    {
        handleBottomRightRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cropBoxRectTransform = cropBox.GetComponent<RectTransform>();
        rightHandleRectTransform = rightHandle.GetComponent<RectTransform>();
        topRightHandleRectTransform = topRightHandle.GetComponent<RectTransform>();
        bottomLeftHandleRectTransform = bottomLeftHandle.GetComponent<RectTransform>();
        bottomHandleRectTransform = bottomHandle.GetComponent<RectTransform>();
        leftHandleRectTransform = leftHandle.GetComponent<RectTransform>();
        topHandleRectTransform = topHandle.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;
        originalYPos = handleBottomRightRectTransform.anchoredPosition.y;
        originalXPos = handleBottomRightRectTransform.anchoredPosition.x;
        heightManager.originalYPosCropBox = cropBoxRectTransform.anchoredPosition.y;
        heightManager.originalXPosCropBox = cropBoxRectTransform.anchoredPosition.x;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;
        heightManager.reScaleRightHandleHeight = rightHandleRectTransform.sizeDelta.y;
        heightManager.reScaleRightHandleHeight = leftHandleRectTransform.sizeDelta.y;
        heightManager.reScaleTopBottomHandleWidth = bottomHandleRectTransform.sizeDelta.x;
        heightManager.reScaleTopBottomHandleWidth = bottomHandleRectTransform.sizeDelta.x;
        heightManager.height = cropBoxRectTransform.sizeDelta.y;
        heightManager.width = cropBoxRectTransform.sizeDelta.x;
        
    }

    private void Update()
    {
        if (dragStarted)
        {
            cropBoxRectTransform.anchoredPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            rightHandleRectTransform.anchoredPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            bottomHandleRectTransform.anchoredPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            leftHandleRectTransform.anchoredPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            topHandleRectTransform.anchoredPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);


            cropBoxRectTransform.sizeDelta = new Vector2(heightManager.width + differencex, heightManager.height - differencey);
            rightHandleRectTransform.sizeDelta = new Vector2(rightHandleRectTransform.sizeDelta.x, heightManager.reScaleRightHandleHeight - differencey);
            leftHandleRectTransform.sizeDelta = new Vector2(leftHandleRectTransform.sizeDelta.x, heightManager.reScaleRightHandleHeight - differencey);
            bottomHandleRectTransform.sizeDelta = new Vector2(heightManager.reScaleTopBottomHandleWidth + differencex, bottomHandleRectTransform.sizeDelta.y);
            topHandleRectTransform.sizeDelta = new Vector2(heightManager.reScaleTopBottomHandleWidth + differencex, topHandleRectTransform.sizeDelta.y);

            differencex = -(originalXPos - handleBottomRightRectTransform.anchoredPosition.x);
            differencey = -(originalYPos - handleBottomRightRectTransform.anchoredPosition.y);

            bottomHandleRectTransform.anchoredPosition = new Vector2(bottomHandleRectTransform.anchoredPosition.x, handleBottomRightRectTransform.anchoredPosition.y);
            leftHandleRectTransform.anchoredPosition = new Vector2(bottomLeftHandleRectTransform.anchoredPosition.x, leftHandleRectTransform.anchoredPosition.y);
            topHandleRectTransform.anchoredPosition = new Vector2(topHandleRectTransform.anchoredPosition.x, topRightHandleRectTransform.anchoredPosition.y);
            rightHandleRectTransform.anchoredPosition = new Vector2(handleBottomRightRectTransform.anchoredPosition.x, rightHandleRectTransform.anchoredPosition.y);
            topRightHandleRectTransform.anchoredPosition = new Vector2(handleBottomRightRectTransform.anchoredPosition.x, topRightHandleRectTransform.anchoredPosition.y);
            bottomLeftHandleRectTransform.anchoredPosition = new Vector2(bottomLeftHandleRectTransform.anchoredPosition.x, handleBottomRightRectTransform.anchoredPosition.y);

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

        handleBottomRightRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(localMousePosition.x, minValueX, 420), Mathf.Clamp(localMousePosition.y, -340, maxValueY));

        
    }
}
