using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropTopLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform topHandle;
    public Transform leftHandle;
    public Transform topRightHandle;
    public Transform bottomLeftHandle;
    private RectTransform cropBoxRectTransform;
    private RectTransform topHandleRectTransform;
    private RectTransform leftHandleRectTransform;
    private RectTransform handleTopLeftRectTransform;
    private RectTransform topRightHandleRectTransform;
    private RectTransform bottomLeftHandleRectTransform;
    private Canvas canvas;

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = -426.4792f;
    private float originalYPos = 335.4922f;
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
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;

        heightManager.height = cropBoxRectTransform.sizeDelta.y;
        originalYPos = handleTopLeftRectTransform.localPosition.y;
        heightManager.originalYPosCropBox = cropBoxRectTransform.localPosition.y;

        heightManager.width = cropBoxRectTransform.sizeDelta.x;
        originalXPos = handleTopLeftRectTransform.localPosition.x;
        heightManager.originalXPosCropBox = cropBoxRectTransform.localPosition.x;
    }

    private void Update()
    {
        if (dragStarted)
        {
            cropBoxRectTransform.localPosition = new Vector3((heightManager.originalXPosCropBox + differencex / 2), (heightManager.originalYPosCropBox + differencey / 2), 0);

            cropBoxRectTransform.sizeDelta = new Vector2(heightManager.width - differencex, heightManager.height + differencey);

            differencex = -(originalXPos - handleTopLeftRectTransform.anchoredPosition.x);
            differencey = -(originalYPos - handleTopLeftRectTransform.anchoredPosition.y);

            topHandleRectTransform.anchoredPosition = new Vector2(topHandleRectTransform.anchoredPosition.x, handleTopLeftRectTransform.anchoredPosition.y);
            leftHandleRectTransform.anchoredPosition = new Vector2(handleTopLeftRectTransform.anchoredPosition.x, leftHandleRectTransform.anchoredPosition.y);
            topRightHandleRectTransform.anchoredPosition = new Vector2(topRightHandleRectTransform.anchoredPosition.x, handleTopLeftRectTransform.anchoredPosition.y);
            bottomLeftHandleRectTransform.anchoredPosition = new Vector2(handleTopLeftRectTransform.anchoredPosition.x, bottomLeftHandleRectTransform.anchoredPosition.y);
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
