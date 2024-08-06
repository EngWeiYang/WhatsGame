using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropHandleBottom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
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

    public HeightCropManager heightCropManager;

    private bool dragStarted = false;

    //Calculation
    private float originalYPos = -250;
    private float maxValue = 250;
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
        heightCropManager.originalYPosCropBox = cropBoxRectTransform.anchoredPosition.y;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;

        heightCropManager.height = cropBoxRectTransform.sizeDelta.y;
        heightCropManager.reScallingRightHandleHeight = rightHandleRectTransform.sizeDelta.y;
        heightCropManager.reScallingRightHandleHeight = leftHandleRectTransform.sizeDelta.y;
    }

    private void Update()
    {
        if (dragStarted)
        {
            //Position
            cropBoxRectTransform.anchoredPosition = new Vector3(cropBoxRectTransform.anchoredPosition.x, heightCropManager.originalYPosCropBox + difference / 2, 0);
            rightHandleRectTransform.anchoredPosition = new Vector3(rightHandleRectTransform.anchoredPosition.x, heightCropManager.originalYPosCropBox + difference / 2, 0);
            leftHandleRectTransform.anchoredPosition = new Vector3(leftHandleRectTransform.anchoredPosition.x, heightCropManager.originalYPosCropBox + difference / 2, 0);
            //Scale
            cropBoxRectTransform.sizeDelta = new Vector2(cropBoxRectTransform.sizeDelta.x, heightCropManager.height - difference);
            rightHandleRectTransform.sizeDelta = new Vector2(rightHandleRectTransform.sizeDelta.x, heightCropManager.reScallingRightHandleHeight - difference);
            leftHandleRectTransform.sizeDelta = new Vector2(leftHandleRectTransform.sizeDelta.x, heightCropManager.reScallingRightHandleHeight - difference);

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

        handleBottomRectTransform.anchoredPosition = new Vector2(handleBottomRectTransform.anchoredPosition.x, Mathf.Clamp(localMousePosition.y, -250, maxValue));
    }
}
