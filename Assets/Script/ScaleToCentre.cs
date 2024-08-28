using System.Collections;
using UnityEngine;

public class ScaleToCenter : MonoBehaviour
{
    public Vector3 targetScale = Vector3.one;  // Target scale
    public float duration = 0.3f;  // Duration in seconds (faster scaling)
    public Vector2 screenCenter; // Manually set position in UI space

    private RectTransform rectTransform;
    private Vector3 initialScale;
    private Vector3 initialPosition;
    private bool isScaling = false;

    void Start()
    {
        // Get the RectTransform component
        rectTransform = GetComponent<RectTransform>();

        // Store the initial scale and position
        initialScale = rectTransform.localScale;
        initialPosition = rectTransform.anchoredPosition;
        screenCenter = new Vector3(0, -617, 0); 

        // Debug log to confirm positions
        Debug.Log($"Initial Position: {initialPosition}");
        Debug.Log($"Target Position: {screenCenter}");
    }

    public void StartScaling()
    {
        if (!isScaling)
        {
            isScaling = true;
            Debug.Log("Starting Scaling");
            StartCoroutine(ScaleToTargetCoroutine());
        }
    }

    private IEnumerator ScaleToTargetCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new scale and position
            rectTransform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            rectTransform.anchoredPosition = Vector3.Lerp(initialPosition, screenCenter, elapsedTime / duration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the final scale and position are set
        rectTransform.localScale = targetScale;
        rectTransform.anchoredPosition = screenCenter;
        isScaling = false;

        Debug.Log("Scaling Complete");
    }
}