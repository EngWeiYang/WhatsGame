using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropTop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    private RectTransform cropBoxRectTransform;
    private RectTransform handleTopRectTransform;
    private Canvas canvas;

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalYPos = 342;
    private float difference;

    private void Awake()
    {
        handleTopRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        cropBoxRectTransform = cropBox.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragStarted = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragStarted = false;

        heightManager.height = cropBoxRectTransform.sizeDelta.y;
        originalYPos = handleTopRectTransform.localPosition.y;
        heightManager.originalYPosCropBox = cropBoxRectTransform.localPosition.y;
    }

    private void Update()
    {
        if (dragStarted)
        {
            cropBoxRectTransform.localPosition = new Vector3(cropBoxRectTransform.anchoredPosition.x, heightManager.originalYPosCropBox + difference / 2, 0);
            cropBoxRectTransform.sizeDelta = new Vector2(cropBoxRectTransform.sizeDelta.x, heightManager.height + difference);
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

        handleTopRectTransform.anchoredPosition = new Vector2(handleTopRectTransform.anchoredPosition.x, localMousePosition.y);

        difference = -(originalYPos - handleTopRectTransform.anchoredPosition.y);
    }
}
