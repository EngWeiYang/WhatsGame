using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropHandleTopRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform topHandle;
    public Transform bottomHandle;
    public Transform topLeftHandle;
    public Transform bottomRightHandle;
    public Transform rightHandle;
    public Transform leftHandle;
    private RectTransform cropBoxRectTransform;
    private RectTransform handleTopRightRectTransform;
    private RectTransform topHandleRectTransform;
    private RectTransform bottomHandleRectTransform;
    private RectTransform rightHandleRectTransform;
    private RectTransform leftHandleRectTransform;
    private RectTransform topLeftHandleRectTransform;
    private RectTransform bottomRightHandleRectTransform;
    private Canvas canvas;

    public HeightCropManager heightCropManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = 325;
    private float originalYPos = 242;
    private float minValueX = -325;
    private float minValueY = -242;
    private float differencex;
    private float differencey;

    private void Awake()
    {
        handleTopRightRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cropBoxRectTransform = cropBox.GetComponent<RectTransform>();
        topHandleRectTransform = topHandle.GetComponent<RectTransform>();
        rightHandleRectTransform = rightHandle.GetComponent<RectTransform>();
        bottomHandleRectTransform = bottomHandle.GetComponent<RectTransform>();
        leftHandleRectTransform = leftHandle.GetComponent<RectTransform>();
        topLeftHandleRectTransform = topLeftHandle.GetComponent<RectTransform>();
        bottomRightHandleRectTransform = bottomRightHandle.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;
        originalYPos = handleTopRightRectTransform.anchoredPosition.y;
        originalXPos = handleTopRightRectTransform.anchoredPosition.x;
        heightCropManager.originalYPosCropBox = cropBoxRectTransform.anchoredPosition.y;
        heightCropManager.originalXPosCropBox = cropBoxRectTransform.anchoredPosition.x;

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
            differencex = -(originalXPos - handleTopRightRectTransform.anchoredPosition.x);
            differencey = -(originalYPos - handleTopRightRectTransform.anchoredPosition.y);

            //Position
            cropBoxRectTransform.anchoredPosition = new Vector3((heightCropManager.originalXPosCropBox + differencex / 2), (heightCropManager.originalYPosCropBox + differencey / 2), 0);
            topHandleRectTransform.anchoredPosition = new Vector3((heightCropManager.originalXPosCropBox + differencex / 2), (heightCropManager.originalYPosCropBox + differencey / 2), 0);
            rightHandleRectTransform.anchoredPosition = new Vector3((heightCropManager.originalXPosCropBox + differencex / 2), (heightCropManager.originalYPosCropBox + differencey / 2), 0);
            bottomHandleRectTransform.anchoredPosition = new Vector3((heightCropManager.originalXPosCropBox + differencex / 2), (heightCropManager.originalYPosCropBox + differencey / 2), 0);
            leftHandleRectTransform.anchoredPosition = new Vector3((heightCropManager.originalXPosCropBox + differencex / 2), (heightCropManager.originalYPosCropBox + differencey / 2), 0);

            //scaling
            cropBoxRectTransform.sizeDelta = new Vector2(heightCropManager.width + differencex, heightCropManager.height + differencey);
            topHandleRectTransform.sizeDelta = new Vector2(heightCropManager.reScaleTopBottomHandleWidth + differencex, topHandleRectTransform.sizeDelta.y);
            rightHandleRectTransform.sizeDelta = new Vector2(rightHandleRectTransform.sizeDelta.x, heightCropManager.reScallingRightHandleHeight + differencey);
            bottomHandleRectTransform.sizeDelta = new Vector2(heightCropManager.reScaleTopBottomHandleWidth + differencex, bottomHandleRectTransform.sizeDelta.y);
            leftHandleRectTransform.sizeDelta = new Vector2(leftHandleRectTransform.sizeDelta.x, heightCropManager.reScallingRightHandleHeight + differencey);

            //achoredPosition
            topHandleRectTransform.anchoredPosition = new Vector2(topHandleRectTransform.anchoredPosition.x, handleTopRightRectTransform.anchoredPosition.y);
            bottomHandleRectTransform.anchoredPosition = new Vector2(bottomHandleRectTransform.anchoredPosition.x, bottomRightHandleRectTransform.anchoredPosition.y);
            rightHandleRectTransform.anchoredPosition = new Vector2(handleTopRightRectTransform.anchoredPosition.x, rightHandleRectTransform.anchoredPosition.y);
            topLeftHandleRectTransform.anchoredPosition = new Vector2(topLeftHandleRectTransform.anchoredPosition.x, handleTopRightRectTransform.anchoredPosition.y);
            bottomRightHandleRectTransform.anchoredPosition = new Vector2(handleTopRightRectTransform.anchoredPosition.x, bottomRightHandleRectTransform.anchoredPosition.y);
            leftHandleRectTransform.anchoredPosition = new Vector2(topLeftHandleRectTransform.anchoredPosition.x, leftHandleRectTransform.anchoredPosition.y);
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

        handleTopRightRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(localMousePosition.x, minValueX, 325), Mathf.Clamp(localMousePosition.y, minValueY, 242));

    }
}
