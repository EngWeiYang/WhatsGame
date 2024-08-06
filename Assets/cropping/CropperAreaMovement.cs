using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropperAreaMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform handleParent;
    private RectTransform cropBoxRectTransform;
    private RectTransform handleParentRectTransform;
    private Canvas canvas;
    public Transform mid;
    public HeightCropManager heightCropManager;
    public GameObject[] handles;
    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        cropBoxRectTransform = cropBox.GetComponent<RectTransform>();
        handleParentRectTransform = handleParent.GetComponent<RectTransform>();

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        for (int i = 0; i < handles.Length; i++)
        {
            handles[i].transform.parent = gameObject.transform;
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        for (int i = 0; i < handles.Length; i++)
        {
            handles[i].transform.parent = mid.transform;
        }
    }
    private void Update()
    {
        handleParentRectTransform.anchoredPosition = cropBoxRectTransform.anchoredPosition;
        handleParentRectTransform.sizeDelta = cropBoxRectTransform.sizeDelta;

    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localMousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localMousePosition);

        cropBoxRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(localMousePosition.x, (-675.75f / 2 + heightCropManager.width / 2), (675.75f / 2 - heightCropManager.width / 2)), Mathf.Clamp(localMousePosition.y, (-503.28f / 2 + heightCropManager.height / 2), (503.28f / 2 - heightCropManager.height / 2)));



    }
}
