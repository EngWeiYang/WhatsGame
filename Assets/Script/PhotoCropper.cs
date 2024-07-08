using UnityEngine;
using UnityEngine.UI;

public class PhotoCropper : MonoBehaviour
{
    public RectTransform cropArea; // The UI element defining the crop area
    public RawImage originalImage; // The image to be cropped
    public RawImage croppedImage;  // The image to display the cropped result

    private Texture2D originalTexture;

    void Start()
    {
        originalTexture = originalImage.texture as Texture2D;
        if (originalTexture == null || !originalTexture.isReadable)
        {
            Debug.LogError("The texture is not readable. Please enable 'Read/Write Enabled' in the texture import settings.");
        }
    }

    public void Crop()
    {
        if (originalTexture == null || !originalTexture.isReadable)
        {
            Debug.LogError("The texture is not readable. Please enable 'Read/Write Enabled' in the texture import settings.");
            return;
        }

        // Convert crop area from RectTransform to Texture2D coordinates
        Rect rect = GetCropRect();

        // Ensure the crop area is within texture bounds
        rect = ClampRectToTexture(rect, originalTexture.width, originalTexture.height);

        Texture2D croppedTexture = new Texture2D((int)rect.width, (int)rect.height);

        // Get pixels from the original texture within the crop area
        Color[] pixels = originalTexture.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        // Display the cropped texture
        croppedImage.texture = croppedTexture;
        croppedImage.rectTransform.sizeDelta = new Vector2(rect.width, rect.height);
    }

    private Rect GetCropRect()
    {
        Vector2 size = cropArea.rect.size;
        Vector2 position = cropArea.anchoredPosition;

        float rectX = position.x - size.x / 2f;
        float rectY = position.y - size.y / 2f;

        // Adjust for the position and pivot of the image
        float imageX = originalImage.rectTransform.anchoredPosition.x - originalImage.rectTransform.rect.width / 2f;
        float imageY = originalImage.rectTransform.anchoredPosition.y - originalImage.rectTransform.rect.height / 2f;

        float x = rectX - imageX;
        float y = rectY - imageY;

        return new Rect(x, y, size.x, size.y);
    }

    private Rect ClampRectToTexture(Rect rect, int textureWidth, int textureHeight)
    {
        float x = Mathf.Clamp(rect.x, 0, textureWidth);
        float y = Mathf.Clamp(rect.y, 0, textureHeight);
        float width = Mathf.Clamp(rect.width, 0, textureWidth - x);
        float height = Mathf.Clamp(rect.height, 0, textureHeight - y);

        return new Rect(x, y, width, height);
    }
}
