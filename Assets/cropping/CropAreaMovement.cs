using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropAreaMovement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Transform cropBox;
    public Transform cropperBox;
    private RectTransform cropBoxRectTransform;
    private RectTransform cropperBoxRectTransform;
    private Canvas canvas;
    public HeightManager heightManager;
    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        cropBoxRectTransform = cropBox.GetComponent<RectTransform>();
        cropperBoxRectTransform = cropperBox.GetComponent<RectTransform>();
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
       

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
    private void Update()
    {
        //Step1


        //Step 2
        //float dividedHeightOfCropBox = heightManager.height / 2;
        //float dividedWidthOfCropBox = heightManager.width / 2;

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
