using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cropper : MonoBehaviour
{
    public RawImage photoDisplay; // Original photo display
    public RectTransform cropArea; // Crop area
    public Button sendButton; // Button to trigger the crop and send
    public RawImage resultDisplay; // RawImage to display the cropped image

    private void Start()
    {
        sendButton.onClick.AddListener(CropAndSend);
    }

    private void CropAndSend()
    {
        if (photoDisplay.texture is Texture2D originalTexture && originalTexture != null)
        {
            Texture2D croppedTexture = Crop(originalTexture);
            if (croppedTexture != null)
            {
                Send(croppedTexture);
            }
        }
    }

    private Texture2D Crop(Texture2D originalTexture)
    {
        RectTransform photoTransform = photoDisplay.GetComponent<RectTransform>();
        Vector2 photoSize = photoTransform.rect.size;
        Vector2 cropSize = cropArea.rect.size;
        Vector2 cropPosition = cropArea.anchoredPosition - photoTransform.anchoredPosition;

        // Calculate scaling factors if necessary
        Vector2 photoScale = photoTransform.localScale;
        Vector2 cropAreaScale = cropArea.localScale;

        // Convert crop area position and size to texture coordinates
        int x = Mathf.FloorToInt((cropPosition.x / photoSize.x) * originalTexture.width);
        int y = Mathf.FloorToInt((cropPosition.y / photoSize.y) * originalTexture.height);
        int width = Mathf.FloorToInt((cropSize.x / photoSize.x) * originalTexture.width);
        int height = Mathf.FloorToInt((cropSize.y / photoSize.y) * originalTexture.height);

        // Apply scaling
        x = Mathf.FloorToInt(x / photoScale.x);
        y = Mathf.FloorToInt(y / photoScale.y);
        width = Mathf.FloorToInt(width / photoScale.x);
        height = Mathf.FloorToInt(height / photoScale.y);

        // Ensure cropping area is within texture bounds
        x = Mathf.Clamp(x, 0, originalTexture.width - 1);
        y = Mathf.Clamp(y, 0, originalTexture.height - 1);
        width = Mathf.Clamp(width, 1, originalTexture.width - x);
        height = Mathf.Clamp(height, 1, originalTexture.height - y);

        Color[] pixels = originalTexture.GetPixels(x, y, width, height);
        Texture2D croppedTexture = new Texture2D(width, height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        return croppedTexture;
    }

    private void Send(Texture2D croppedTexture)
    {
        if (resultDisplay != null)
        {
            // Set the cropped texture to the result display
            resultDisplay.texture = croppedTexture;
            resultDisplay.gameObject.SetActive(true); // Ensure the RawImage is visible
        }
    }
}
