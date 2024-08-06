using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropTop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform rightHandle;
    public Transform leftHandle;
    public Transform topRightHandle;
    public Transform topLeftHandle;
    private RectTransform rightHandleRectTransform;
    private RectTransform leftHandleRectTransform;
    private RectTransform topRightHandleRectTransform;
    private RectTransform topLeftHandleRectTransform;
    private RectTransform cropBoxRectTransform;
    private RectTransform handleTopRectTransform;
    private Canvas canvas;

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalYPos = 342;
    private float difference;
    private float minValue = -342;

    private void Awake()
    {
        handleTopRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cropBoxRectTransform = cropBox.GetComponent<RectTransform>();
        rightHandleRectTransform = rightHandle.GetComponent<RectTransform>();
        leftHandleRectTransform = leftHandle.GetComponent<RectTransform>();
        topRightHandleRectTransform = topRightHandle.GetComponent<RectTransform>();
        topLeftHandleRectTransform = topLeftHandle.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;
        originalYPos = handleTopRectTransform.anchoredPosition.y;
        heightManager.originalYPosCropBox = cropBoxRectTransform.anchoredPosition.y;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;
        heightManager.reScaleRightHandleHeight = rightHandleRectTransform.sizeDelta.y;
        heightManager.reScaleRightHandleHeight = leftHandleRectTransform.sizeDelta.y;
        heightManager.height = cropBoxRectTransform.sizeDelta.y;
        
    }

    private void Update()
    {
        if (dragStarted)
        {
            //position
            cropBoxRectTransform.anchoredPosition = new Vector3(cropBoxRectTransform.anchoredPosition.x, heightManager.originalYPosCropBox + difference / 2, 0);
            rightHandleRectTransform.anchoredPosition = new Vector3(rightHandleRectTransform.anchoredPosition.x, heightManager.originalYPosCropBox + difference / 2, 0);
            leftHandleRectTransform.anchoredPosition = new Vector3(leftHandleRectTransform.anchoredPosition.x, heightManager.originalYPosCropBox + difference / 2, 0);
           


            //scaling
            cropBoxRectTransform.sizeDelta = new Vector2(cropBoxRectTransform.sizeDelta.x, heightManager.height + difference);
            rightHandleRectTransform.sizeDelta = new Vector2(rightHandleRectTransform.sizeDelta.x, heightManager.reScaleRightHandleHeight + difference);
            leftHandleRectTransform.sizeDelta = new Vector2(leftHandleRectTransform.sizeDelta.x, heightManager.reScaleRightHandleHeight + difference);


            difference = -(originalYPos - handleTopRectTransform.anchoredPosition.y);

            topRightHandleRectTransform.anchoredPosition = new Vector2(rightHandleRectTransform.anchoredPosition.x, handleTopRectTransform.anchoredPosition.y);

            topLeftHandleRectTransform.anchoredPosition = new Vector2(leftHandleRectTransform.anchoredPosition.x, handleTopRectTransform.anchoredPosition.y);
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

        handleTopRectTransform.anchoredPosition = new Vector2(handleTopRectTransform.anchoredPosition.x, Mathf.Clamp(localMousePosition.y, minValue, 342));
       
    }
}
