using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropBottom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform bottomRightHandle;
    public Transform bottomLeftHandle;
    public Transform rightHandle;
    public Transform leftHandle;
    private RectTransform cropBoxRectTransform;
    private RectTransform handleBottomRectTransform;
    private RectTransform bottomRightHandleRectTransform;
    private RectTransform bottomLeftHandleRectTransform;
    private RectTransform leftHandleRectTransform;
    private RectTransform rightHandleRectTransform;
    private Canvas canvas;

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalYPos = -342;
    private float maxValue = 342;
    private float difference;

    private void Awake()
    {
        handleBottomRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cropBoxRectTransform = cropBox.GetComponent<RectTransform>();
        bottomLeftHandleRectTransform = bottomLeftHandle.GetComponent<RectTransform>();
        bottomRightHandleRectTransform = bottomRightHandle.GetComponent<RectTransform>();
        leftHandleRectTransform = leftHandle.GetComponent<RectTransform>();
        rightHandleRectTransform = rightHandle.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;

        originalYPos = handleBottomRectTransform.anchoredPosition.y;
        heightManager.originalYPosCropBox = cropBoxRectTransform.anchoredPosition.y;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;

        heightManager.height = cropBoxRectTransform.sizeDelta.y;
        heightManager.reScaleRightHandleHeight = rightHandleRectTransform.sizeDelta.y;
        heightManager.reScaleRightHandleHeight = leftHandleRectTransform.sizeDelta.y;
    }

    private void Update()
    {
        if (dragStarted)
        {
            //Position
            cropBoxRectTransform.anchoredPosition = new Vector3(cropBoxRectTransform.anchoredPosition.x, heightManager.originalYPosCropBox + difference / 2, 0);
            rightHandleRectTransform.anchoredPosition = new Vector3(rightHandleRectTransform.anchoredPosition.x, heightManager.originalYPosCropBox + difference / 2, 0);
            leftHandleRectTransform.anchoredPosition = new Vector3(leftHandleRectTransform.anchoredPosition.x, heightManager.originalYPosCropBox + difference / 2, 0);
            //Scale
            cropBoxRectTransform.sizeDelta = new Vector2(cropBoxRectTransform.sizeDelta.x, heightManager.height - difference);
            rightHandleRectTransform.sizeDelta = new Vector2(rightHandleRectTransform.sizeDelta.x, heightManager.reScaleRightHandleHeight - difference);
            leftHandleRectTransform.sizeDelta = new Vector2(leftHandleRectTransform.sizeDelta.x, heightManager.reScaleRightHandleHeight - difference);

            difference = -(originalYPos - handleBottomRectTransform.anchoredPosition.y);


            bottomRightHandleRectTransform.anchoredPosition = new Vector2(rightHandleRectTransform.anchoredPosition.x, handleBottomRectTransform.anchoredPosition.y);
            bottomLeftHandleRectTransform.anchoredPosition = new Vector2(leftHandleRectTransform.anchoredPosition.x, handleBottomRectTransform.anchoredPosition.y);
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

        handleBottomRectTransform.anchoredPosition = new Vector2(handleBottomRectTransform.anchoredPosition.x, Mathf.Clamp(localMousePosition.y, -342, maxValue));
    }
}
