using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropTopRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform topHandle;
    public Transform topLeftHandle;
    public Transform bottomRightHandle;
    public Transform rightHandle;
    private RectTransform cropBoxRectTransform;
    private RectTransform handleTopRightRectTransform;
    private RectTransform topHandleRectTransform;
    private RectTransform rightHandleRectTransform;
    private RectTransform topLeftHandleRectTransform;
    private RectTransform bottomRightHandleRectTransform;
    private Canvas canvas;

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = 413.4666f;
    private float originalYPos = 330.2446f;
    private float differencex;
    private float differencey;

    private void Awake()
    {
        handleTopRightRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cropBoxRectTransform = cropBox.GetComponent<RectTransform>();
        topHandleRectTransform = topHandle.GetComponent<RectTransform>();
        rightHandleRectTransform = rightHandle.GetComponent<RectTransform>();
        topLeftHandleRectTransform = topLeftHandle.GetComponent<RectTransform>();
        bottomRightHandleRectTransform = bottomRightHandle.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;

        heightManager.height = cropBoxRectTransform.sizeDelta.y;
        originalYPos = handleTopRightRectTransform.localPosition.y;
        heightManager.originalYPosCropBox = cropBoxRectTransform.localPosition.y;

        heightManager.width = cropBoxRectTransform.sizeDelta.x;
        originalXPos = handleTopRightRectTransform.localPosition.x;
        heightManager.originalXPosCropBox = cropBoxRectTransform.localPosition.x;
    }

    private void Update()
    {
        if (dragStarted)
        {
            differencex = -(originalXPos - handleTopRightRectTransform.anchoredPosition.x);
            differencey = -(originalYPos - handleTopRightRectTransform.anchoredPosition.y);
            
            cropBoxRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2) , (heightManager.originalYPosCropBox + differencey / 2), 0);

            cropBoxRectTransform.sizeDelta = new Vector2(heightManager.width + differencex, heightManager.height + differencey);

            topHandleRectTransform.anchoredPosition = new Vector2(topHandleRectTransform.anchoredPosition.x, handleTopRightRectTransform.anchoredPosition.y);
            rightHandleRectTransform.anchoredPosition = new Vector2(handleTopRightRectTransform.anchoredPosition.x, rightHandleRectTransform.anchoredPosition.y);
            topLeftHandleRectTransform.anchoredPosition = new Vector2(topLeftHandleRectTransform.anchoredPosition.x, handleTopRightRectTransform.anchoredPosition.y);
            bottomRightHandleRectTransform.anchoredPosition = new Vector2(handleTopRightRectTransform.anchoredPosition.x, bottomRightHandleRectTransform.anchoredPosition.y);

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
