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
    public RawImage resultDisplayBeforeSending; // RawImage to display the cropped image
    public GameObject textBox;
    public GameObject imagescreen;
    public GameObject imageCropped;

    private void Start()
    {
        if (sendButton != null)
        {
            sendButton.onClick.AddListener(CropAndSend);
        }
        
    }

    private void CropAndSend()
    {
        if (photoDisplay != null && photoDisplay.texture is Texture2D originalTexture && originalTexture != null)
        {
            Texture2D croppedTexture = Crop(originalTexture);
            if (croppedTexture != null)
            {
                Send(croppedTexture);
            }
        }
        else
        {
            Debug.LogError("Photo Display or its Texture is not assigned or invalid");

        }
    }

    private Texture2D Crop(Texture2D originalTexture)
    {
        RectTransform photoTransform = photoDisplay.GetComponent<RectTransform>();
        if (photoTransform == null)
        {
            Debug.LogError("Photo Display does not have a RectTransform component");
            return null;
        }

        Vector2 photoSize = photoTransform.rect.size;
        Vector2 cropSize = cropArea.rect.size;
        Vector2 cropPosition = cropArea.anchoredPosition - photoTransform.anchoredPosition;

        // Calculate the aspect ratio of the photo display relative to the original texture
        float photoAspectRatioX = originalTexture.width / photoSize.x;
        float photoAspectRatioY = originalTexture.height / photoSize.y;

        // Adjust crop position to be in texture coordinates
        int x = Mathf.FloorToInt((cropPosition.x + (photoSize.x / 2) - (cropSize.x / 2)) * photoAspectRatioX);
        int y = Mathf.FloorToInt((cropPosition.y + (photoSize.y / 2) - (cropSize.y / 2)) * photoAspectRatioY);
        int width = Mathf.FloorToInt(cropSize.x * photoAspectRatioX);
        int height = Mathf.FloorToInt(cropSize.y * photoAspectRatioY);

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
            resultDisplayBeforeSending.texture = croppedTexture;
            if (textBox != null) textBox.SetActive(true);
            if (imagescreen != null) imagescreen.SetActive(true);
            if (imageCropped != null) imageCropped.SetActive(false);
            resultDisplay.gameObject.SetActive(true); // Ensure the RawImage is visible
        }
        
    }
}
