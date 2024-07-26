using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ImageSaver
{
    public static Texture2D croppedTexture;

    public static void SaveImage(Texture2D texture)
    {
        croppedTexture = texture;
    }
}
