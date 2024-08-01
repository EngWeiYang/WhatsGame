using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropTopLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
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

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = -420;
    private float originalYPos = 340;
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
        originalYPos = handleTopLeftRectTransform.localPosition.y;
        
        originalXPos = handleTopLeftRectTransform.localPosition.x;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;
        heightManager.originalYPosCropBox = cropBoxRectTransform.localPosition.y;
        heightManager.reScaleTopBottomHandleWidth = topHandleRectTransform.sizeDelta.x;
        heightManager.reScaleTopBottomHandleWidth = bottomHandleRectTransform.sizeDelta.x;
        heightManager.reScaleRightHandleHeight = rightHandleRectTransform.sizeDelta.y;
        heightManager.reScaleRightHandleHeight = leftHandleRectTransform.sizeDelta.y;
        heightManager.height = cropBoxRectTransform.sizeDelta.y;
        heightManager.width = cropBoxRectTransform.sizeDelta.x;
        heightManager.originalXPosCropBox = cropBoxRectTransform.localPosition.x;
    }

    private void Update()
    {
        if (dragStarted)
        {
            cropBoxRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            leftHandleRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            topHandleRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            bottomHandleRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);
            rightHandleRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);

            cropBoxRectTransform.sizeDelta = new Vector2(heightManager.width - differencex, heightManager.height + differencey);
            topHandleRectTransform.sizeDelta = new Vector2(heightManager.reScaleTopBottomHandleWidth - differencex, topHandleRectTransform.sizeDelta.y);
            bottomHandleRectTransform.sizeDelta = new Vector2(heightManager.reScaleTopBottomHandleWidth - differencex, topHandleRectTransform.sizeDelta.y);
            leftHandleRectTransform.sizeDelta = new Vector2(leftHandleRectTransform.sizeDelta.x, heightManager.reScaleRightHandleHeight + differencey);
            rightHandleRectTransform.sizeDelta = new Vector2(rightHandleRectTransform.sizeDelta.x, heightManager.reScaleRightHandleHeight + differencey);

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

        handleTopLeftRectTransform.anchoredPosition = new Vector2(localMousePosition.x, localMousePosition.y);
    }
}
