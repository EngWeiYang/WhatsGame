using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropTopLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    private RectTransform cropBoxRectTransform;
    private RectTransform handleTopLeftRectTransform;
    private Canvas canvas;

    public HeightManager heightManager;

    private bool dragStarted = false;

    //Calculation
    private float originalXPos = -425;
    private float originalYPos = 330;
    private float differencex;
    private float differencey;

    private void Awake()
    {
        handleTopLeftRectTransform = GetComponent<RectTransform>();
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
        originalYPos = handleTopLeftRectTransform.localPosition.y;
        heightManager.originalTopLeftYPosCropBox = cropBoxRectTransform.localPosition.y;

        heightManager.width = cropBoxRectTransform.sizeDelta.x;
        originalXPos = handleTopLeftRectTransform.localPosition.x;
        heightManager.originalTopLeftXPosCropBox = cropBoxRectTransform.localPosition.x;
    }

    private void Update()
    {
        if (dragStarted)
        {
            cropBoxRectTransform.localPosition = new Vector3((heightManager.originalTopLeftXPosCropBox + differencex / 2), (heightManager.originalTopLeftYPosCropBox + differencey / 2), 0);

            cropBoxRectTransform.sizeDelta = new Vector2(heightManager.width - differencex, heightManager.height + differencey);

            differencex = -(originalXPos - handleTopLeftRectTransform.anchoredPosition.x);
            differencey = -(originalYPos - handleTopLeftRectTransform.anchoredPosition.y);
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
