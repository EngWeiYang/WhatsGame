using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class videoCropRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform videoCropper;


    private RectTransform videoCropperRectTransform;
    private RectTransform videoCropperRightRectTransform;
    private Canvas canvas;
    private bool isDragging = false;

    private float originalXValue = 290;
    private float minXValue = -225;
    private float difference;

    public HeightManager heightManager;

    private void Awake()
    {
        videoCropperRightRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        videoCropperRectTransform = videoCropper.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;

        originalXValue = videoCropperRightRectTransform.localPosition.x;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        heightManager.videoCropperWidth = videoCropperRectTransform.sizeDelta.x;
        heightManager.originalXPosVideoCropper = videoCropperRectTransform.localPosition.x;
    }

    private void Update()
    {
        if (isDragging)
        {
            //Position
            videoCropperRectTransform.localPosition = new Vector3(heightManager.originalXPosVideoCropper + difference / 2, videoCropperRectTransform.anchoredPosition.y, 0);

            //Scale
            videoCropperRectTransform.sizeDelta = new Vector2(heightManager.videoCropperWidth + difference, videoCropperRectTransform.sizeDelta.y);


            difference = -(originalXValue - videoCropperRightRectTransform.anchoredPosition.x);


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

        videoCropperRightRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(localMousePosition.x, minXValue, 290), videoCropperRightRectTransform.anchoredPosition.y);


    }
}
