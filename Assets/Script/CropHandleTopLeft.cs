using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropHandleTopLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform topHandle;
    public Transform bottomHandle;
    public Transform leftHandle;
    public Transform rightHandle;
    public Transform topRightHandle;
    public Transform bottomLeftHandle;
    private RectTransform cropBoxRectTransform;
    private RectTransform topHandleRectTransform;
    private RectTransform bottomHandleRectTransform;
    private RectTransform leftHandleRectTransform;
    private RectTransform rightHandleRectTransform;
    private RectTransform handleTopLeftRectTransform;
    private RectTransform topRightHandleRectTransform;
    private RectTransform bottomLeftHandleRectTransform;
    private Canvas canvas;

    public HeightCropManager heightCropManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = -325;
    private float originalYPos = 242;
    private float maxValueX = 325;
    private float minValueY = -242;
    private float differencex;
    private float differencey;

    private void Awake()
    {
        handleTopLeftRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cropBoxRectTransform = cropBox.GetComponent<RectTransform>();
        topHandleRectTransform = topHandle.GetComponent<RectTransform>();
        leftHandleRectTransform = leftHandle.GetComponent<RectTransform>();
        topRightHandleRectTransform = topRightHandle.GetComponent<RectTransform>();
        bottomLeftHandleRectTransform = bottomLeftHandle.GetComponent<RectTransform>();
        bottomHandleRectTransform = bottomHandle.GetComponent<RectTransform>();
        rightHandleRectTransform = rightHandle.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;
        originalYPos = handleTopLeftRectTransform.anchoredPosition.y;
        heightCropManager.originalXPosCropBox = cropBoxRectTransform.anchoredPosition.x;
        originalXPos = handleTopLeftRectTransform.anchoredPosition.x;
        heightCropManager.originalYPosCropBox = cropBoxRectTransform.anchoredPosition.y;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;
        heightCropManager.reScaleTopBottomHandleWidth = topHandleRectTransform.sizeDelta.x;
        heightCropManager.reScaleTopBottomHandleWidth = bottomHandleRectTransform.sizeDelta.x;
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
            topHandleRectTransform.anchoredPosition = new Vector3((heightCropManager.originalXPosCropBox + differencex / 2), (heightCropManager.originalYPosCropBox + differencey / 2), 0);
            bottomHandleRectTransform.anchoredPosition = new Vector3((heightCropManager.originalXPosCropBox + differencex / 2), (heightCropManager.originalYPosCropBox + differencey / 2), 0);
            rightHandleRectTransform.anchoredPosition = new Vector3((heightCropManager.originalXPosCropBox + differencex / 2), (heightCropManager.originalYPosCropBox + differencey / 2), 0);

            cropBoxRectTransform.sizeDelta = new Vector2(heightCropManager.width - differencex, heightCropManager.height + differencey);
            topHandleRectTransform.sizeDelta = new Vector2(heightCropManager.reScaleTopBottomHandleWidth - differencex, topHandleRectTransform.sizeDelta.y);
            bottomHandleRectTransform.sizeDelta = new Vector2(heightCropManager.reScaleTopBottomHandleWidth - differencex, topHandleRectTransform.sizeDelta.y);
            leftHandleRectTransform.sizeDelta = new Vector2(leftHandleRectTransform.sizeDelta.x, heightCropManager.reScallingRightHandleHeight + differencey);
            rightHandleRectTransform.sizeDelta = new Vector2(rightHandleRectTransform.sizeDelta.x, heightCropManager.reScallingRightHandleHeight + differencey);

            differencex = -(originalXPos - handleTopLeftRectTransform.anchoredPosition.x);
            differencey = -(originalYPos - handleTopLeftRectTransform.anchoredPosition.y);

            topHandleRectTransform.anchoredPosition = new Vector2(topHandleRectTransform.anchoredPosition.x, handleTopLeftRectTransform.anchoredPosition.y);
            rightHandleRectTransform.anchoredPosition = new Vector2(topRightHandleRectTransform.anchoredPosition.x, rightHandleRectTransform.anchoredPosition.y);
            leftHandleRectTransform.anchoredPosition = new Vector2(handleTopLeftRectTransform.anchoredPosition.x, leftHandleRectTransform.anchoredPosition.y);
            topRightHandleRectTransform.anchoredPosition = new Vector2(topRightHandleRectTransform.anchoredPosition.x, handleTopLeftRectTransform.anchoredPosition.y);
            bottomLeftHandleRectTransform.anchoredPosition = new Vector2(handleTopLeftRectTransform.anchoredPosition.x, bottomLeftHandleRectTransform.anchoredPosition.y);
            bottomHandleRectTransform.anchoredPosition = new Vector2(bottomHandleRectTransform.anchoredPosition.x, bottomLeftHandleRectTransform.anchoredPosition.y);
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

        handleTopLeftRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(localMousePosition.x, -325, maxValueX), Mathf.Clamp(localMousePosition.y, minValueY, 242));

    }
}
