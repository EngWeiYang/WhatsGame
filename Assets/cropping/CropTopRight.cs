using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropTopRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
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

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = 420;
    private float originalYPos = 340;
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
        originalYPos = handleTopRightRectTransform.localPosition.y;
        originalXPos = handleTopRightRectTransform.localPosition.x;
        

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;
        heightManager.reScaleTopBottomHandleWidth = topHandleRectTransform.sizeDelta.x;
        heightManager.reScaleTopBottomHandleWidth = bottomHandleRectTransform.sizeDelta.x;
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
            differencex = -(originalXPos - handleTopRightRectTransform.anchoredPosition.x);
            differencey = -(originalYPos - handleTopRightRectTransform.anchoredPosition.y);

            //Position
            cropBoxRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2) , (heightManager.originalYPosCropBox + differencey / 2), 0);
            topHandleRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            rightHandleRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            bottomHandleRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            leftHandleRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);

            //scaling
            cropBoxRectTransform.sizeDelta = new Vector2(heightManager.width + differencex, heightManager.height + differencey);
            topHandleRectTransform.sizeDelta = new Vector2(heightManager.reScaleTopBottomHandleWidth + differencex, topHandleRectTransform.sizeDelta.y);
            rightHandleRectTransform.sizeDelta = new Vector2(rightHandleRectTransform.sizeDelta.x, heightManager.reScaleRightHandleHeight + differencey);
            bottomHandleRectTransform.sizeDelta = new Vector2(heightManager.reScaleTopBottomHandleWidth + differencex, bottomHandleRectTransform.sizeDelta.y);
            leftHandleRectTransform.sizeDelta = new Vector2(leftHandleRectTransform.sizeDelta.x, heightManager.reScaleRightHandleHeight + differencey);

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

        handleTopRightRectTransform.anchoredPosition = new Vector2(localMousePosition.x, localMousePosition.y);
    }
}
