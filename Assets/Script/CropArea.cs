using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CropArea : MonoBehaviour
{
    public RectTransform cropArea;
    public RectTransform[] resizeHandles; // Assign handles here

    private void Start()
    {
        foreach (var handle in resizeHandles)
        {
            handle.gameObject.AddComponent<ResizeHandlers>().cropArea = cropArea;
        }
    }
}
