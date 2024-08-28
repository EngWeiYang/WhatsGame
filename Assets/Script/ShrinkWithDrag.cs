using UnityEngine;

public class ShrinkWithDrag : MonoBehaviour
{
    // Public properties to define the scale limits
    public float minHeight = 0.001f; // Adjust this to a smaller value to shrink more
    public float maxHeight = 1f;   // Maximum height scale (original height)
    public float lockThresholdY = 20000f; // Increase this to make the object shrink more for the same drag distance

    public RectTransform objectToIgnore; // The specific object that should not shrink

    private Vector3 initialScale; // To store the initial scale of the object
    private RectTransform rectTransform; // Reference to RectTransform
    private Vector2 initialpos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>(); // Get the RectTransform component
        initialScale = rectTransform.localScale; // Store the initial scale of the object
        initialpos = transform.position;

        // Set the pivot to (0.5, 0) to make the object shrink towards the top
        rectTransform.pivot = new Vector2(0.5f, 0);
    }

    // Call this method while dragging
    public void AdjustHeight(float dragDistance)
    {
        // Check if the current RectTransform is the one to ignore
        if (rectTransform != objectToIgnore)
        {
            // Calculate the scale factor based on the drag distance
            float scaleFactor = Mathf.Lerp(maxHeight, minHeight, dragDistance / lockThresholdY);
            
            // Apply the scale factor only to the Y axis to make the object shorter
            rectTransform.localScale = new Vector3(initialScale.x, initialScale.y * scaleFactor, initialScale.z);
        }
    }

    // Reset the object to its original scale
    public void ResetHeight()
    {
        rectTransform.localScale = initialScale;
        rectTransform.position = initialpos;
    }
}
