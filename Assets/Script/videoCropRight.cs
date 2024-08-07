using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class videoCropRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform videoCropper;
    public Transform videoCropperImage;
    public Transform VideoCropperImageMask;
    public Transform videoPhoto;
    public TMP_Text videoDuration;
    public TMP_Text videoDurationStorage;
    private RectTransform videoCropperRectTransform;
    private RectTransform videoCropperImageRectTransform;
    private RectTransform videoCropperImageMaskRectTransform;
    private RectTransform videoCropperRightRectTransform;
    private RectTransform videoPhotoRectTransform;
    private Canvas canvas;
    private bool isDragging = false;

    private float originalXValue = 290;
    private float minXValue = -225;
    private float difference;
    private float croppedTime;
    private float croppedVideoStorage;
    private float VideoTiming = 50f;
    private float videoStorage = 503;

    public HeightManager heightManager;

    private void Awake()
    {
        videoDuration.text = "0:" + VideoTiming.ToString("#");
        videoDurationStorage.text = videoStorage.ToString() + "kB";
        videoCropperRightRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        videoCropperRectTransform = videoCropper.GetComponent<RectTransform>();
        videoCropperImageRectTransform = videoCropperImage.GetComponent<RectTransform>();
        videoCropperImageMaskRectTransform = VideoCropperImageMask.GetComponent<RectTransform>();
        videoPhotoRectTransform = videoPhoto.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        originalXValue = videoCropperRightRectTransform.localPosition.x;
        videoPhotoRectTransform.anchorMin = new Vector2(0, 0.5f);
        videoPhotoRectTransform.anchoredPosition = new Vector2(290, videoPhotoRectTransform.anchoredPosition.y);
        videoPhotoRectTransform.anchorMax = new Vector2(0, 0.5f);
        heightManager.videoCropperWidth = videoCropperImageRectTransform.sizeDelta.x;
        heightManager.originalXPosVideoCropper = videoCropperImageRectTransform.localPosition.x;
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
            videoCropperImageRectTransform.localPosition = new Vector3(heightManager.originalXPosVideoCropper + difference / 2, videoCropperRectTransform.anchoredPosition.y, 0);

            //Scale
            videoCropperRectTransform.sizeDelta = new Vector2(heightManager.videoCropperWidth + difference, videoCropperRectTransform.sizeDelta.y);
            videoCropperImageRectTransform.sizeDelta = new Vector2(heightManager.videoCropperWidth + difference, videoCropperRectTransform.sizeDelta.y);
            croppedTime = (videoCropperImageRectTransform.sizeDelta.x / videoCropperImageMaskRectTransform.sizeDelta.x) * VideoTiming;
            croppedVideoStorage = (videoCropperImageRectTransform.sizeDelta.x / videoCropperImageMaskRectTransform.sizeDelta.x) * videoStorage;

            difference = -(originalXValue - videoCropperRightRectTransform.anchoredPosition.x);

            videoDuration.text = "0:" + croppedTime.ToString("#");
            videoDurationStorage.text = croppedVideoStorage.ToString("#") + "kB";
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
