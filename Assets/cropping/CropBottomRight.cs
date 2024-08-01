using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropBottomRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform rightHandle;
    public Transform bottomHandle;
    public Transform topRightHandle;
    public Transform bottomLeftHandle;
    private RectTransform cropBoxRectTransform;
    private RectTransform rightHandleRectTransform;
    private RectTransform topRightHandleRectTransform;
    private RectTransform bottomLeftHandleRectTransform;
    private RectTransform handleBottomRightRectTransform;
    private RectTransform bottomHandleRectTransform;
    private Canvas canvas;

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = 428.4435f;
    private float originalYPos = -333.0896f;
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
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;

        heightManager.height = cropBoxRectTransform.sizeDelta.y;
        originalYPos = handleBottomRightRectTransform.localPosition.y;
        heightManager.originalYPosCropBox = cropBoxRectTransform.localPosition.y;

        heightManager.width = cropBoxRectTransform.sizeDelta.x;
        originalXPos = handleBottomRightRectTransform.localPosition.x;
        heightManager.originalXPosCropBox = cropBoxRectTransform.localPosition.x;
    }

    private void Update()
    {
        if (dragStarted)
        {
            cropBoxRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);

            cropBoxRectTransform.sizeDelta = new Vector2(heightManager.width + differencex, heightManager.height - differencey);

            differencex = -(originalXPos - handleBottomRightRectTransform.anchoredPosition.x);
            differencey = -(originalYPos - handleBottomRightRectTransform.anchoredPosition.y);

            bottomHandleRectTransform.anchoredPosition = new Vector2(bottomHandleRectTransform.anchoredPosition.x, handleBottomRightRectTransform.anchoredPosition.y);
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

        handleBottomRightRectTransform.anchoredPosition = new Vector2(localMousePosition.x, localMousePosition.y);
    }
}
