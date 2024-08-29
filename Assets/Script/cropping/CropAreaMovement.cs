using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropAreaMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform handleParent;
    private RectTransform cropBoxRectTransform;
    private RectTransform handleParentRectTransform;
    private Canvas canvas;
    public HeightManager heightManager;
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
            handles[i].transform.parent = canvas.transform;
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

        cropBoxRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(localMousePosition.x, (-857.08f/2 + heightManager.width /2), (857.08f/2 - heightManager.width /2)), Mathf.Clamp(localMousePosition.y, (-685.5f/2 + heightManager.height/2), (685.5f /2 - heightManager.height / 2)));
        //cropBoxRectTransform.anchoredPosition = cropperBoxRectTransform.anchoredPosition;



    }
}
