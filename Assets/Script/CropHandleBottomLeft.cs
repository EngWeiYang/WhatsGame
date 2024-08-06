using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropHandleBottomLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
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

    public HeightCropManager heightCropManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = -325;
    private float maxValueX = 325;
    private float originalYPos = -242;
    private float maxValueY = 242;
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
        originalYPos = handleBottomLeftRectTransform.anchoredPosition.y;
        originalXPos = handleBottomLeftRectTransform.anchoredPosition.x;
        heightCropManager.originalYPosCropBox = cropBoxRectTransform.anchoredPosition.y;
        heightCropManager.originalXPosCropBox = cropBoxRectTransform.anchoredPosition.x;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;
        heightCropManager.reScaleTopBottomHandleWidth = bottomHandleRectTransform.sizeDelta.x;
        heightCropManager.reScaleTopBottomHandleWidth = topHandleRectTransform.sizeDelta.x;
        heightCropManager.reScallingRightHandleHeight = rightHandleRectTransform.sizeDelta.y;
        heightCropManager.reScallingRightHandleHeight = leftHandleRectTransform.sizeDelta.y;
        heightCropManager.height = cropBoxRectTransform.sizeDelta.y;
        heightCropManager.width = cropBoxRectTransform.sizeDelta.x;

    }

    private void Update()
    {
        if (dragStarted)
        {
            cropBoxRectTransform.anchoredPosition = new Vector3((heightCropManager.originalXPosCropBox + differencex / 2), (heightCropManager.originalYPosCropBox + differencey / 2), 0);
            leftHandleRectTransform.anchoredPosition = new Vector3((heightCropManager.originalXPosCropBox + differencex / 2), (heightCropManager.originalYPosCropBox + differencey / 2), 0);
            rightHandleRectTransform.anchoredPosition = new Vector3((heightCropManager.originalXPosCropBox + differencex / 2), (heightCropManager.originalYPosCropBox + differencey / 2), 0);
            bottomHandleRectTransform.anchoredPosition = new Vector3((heightCropManager.originalXPosCropBox + differencex / 2), (heightCropManager.originalYPosCropBox + differencey / 2), 0);
            topHandleRectTransform.anchoredPosition = new Vector3((heightCropManager.originalXPosCropBox + differencex / 2), (heightCropManager.originalYPosCropBox + differencey / 2), 0);

            cropBoxRectTransform.sizeDelta = new Vector2(heightCropManager.width - differencex, heightCropManager.height - differencey);
            bottomHandleRectTransform.sizeDelta = new Vector2(heightCropManager.reScaleTopBottomHandleWidth - differencex, bottomHandleRectTransform.sizeDelta.y);
            topHandleRectTransform.sizeDelta = new Vector2(heightCropManager.reScaleTopBottomHandleWidth - differencex, topHandleRectTransform.sizeDelta.y);
            leftHandleRectTransform.sizeDelta = new Vector2(leftHandleRectTransform.sizeDelta.x, heightCropManager.reScallingRightHandleHeight - differencey);
            rightHandleRectTransform.sizeDelta = new Vector2(rightHandleRectTransform.sizeDelta.x, heightCropManager.reScallingRightHandleHeight - differencey);

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

        handleBottomLeftRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(localMousePosition.x, -325, maxValueX), Mathf.Clamp(localMousePosition.y, -242, maxValueY));



    }
}
