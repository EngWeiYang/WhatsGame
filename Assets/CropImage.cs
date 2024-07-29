using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Rect cropBox;
    private RectTransform handleTopRectTransform;
    private Canvas canvas;

    //Calculation
    private float originalYPos = 342;
    private float difference;
    private float height;

    private void Awake()
    {
        handleTopRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Handle pointer down event if needed
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Handle pointer up event if needed
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

        difference = originalYPos - handleTopRectTransform.anchoredPosition.y;
        //height = cropBox.height
    }

    //When handle_top is moved down, calculate difference (-122)
    //Calculate new height/width using difference (height - 122)
    //New x/y position = difference/2 (-122/2 = -61)

}
