using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropBottomLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    private RectTransform cropBoxRectTransform;
    private RectTransform handleBottomLeftRectTransform;
    private Canvas canvas;

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = -425;
    private float originalYPos = -330;
    private float differencex;
    private float differencey;

    private void Awake()
    {
        handleBottomLeftRectTransform = GetComponent<RectTransform>();
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
        originalYPos = handleBottomLeftRectTransform.localPosition.y;
        heightManager.originalBottomLeftYPosCropBox = cropBoxRectTransform.localPosition.y;

        heightManager.width = cropBoxRectTransform.sizeDelta.x;
        originalXPos = handleBottomLeftRectTransform.localPosition.x;
        heightManager.originalBottomLeftXPosCropBox = cropBoxRectTransform.localPosition.x;
    }

    private void Update()
    {
        if (dragStarted)
        {
            cropBoxRectTransform.localPosition = new Vector3((heightManager.originalBottomLeftXPosCropBox + differencex / 2), (heightManager.originalBottomLeftYPosCropBox + differencey / 2), 0);

            cropBoxRectTransform.sizeDelta = new Vector2(heightManager.width - differencex, heightManager.height - differencey);

            differencex = -(originalXPos - handleBottomLeftRectTransform.anchoredPosition.x);
            differencey = -(originalYPos - handleBottomLeftRectTransform.anchoredPosition.y);
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

        handleBottomLeftRectTransform.anchoredPosition = new Vector2(localMousePosition.x, localMousePosition.y);
    }
}
