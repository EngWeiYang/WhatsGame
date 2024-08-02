using UnityEngine;
using UnityEngine.UI;

public class SlideAnim : MonoBehaviour
{
    public GameObject movingObject; // Reference to the GameObject that will move
    public float duration = 2.0f; // Duration of the animation in seconds
    private Vector3 targetPosition; // The position to move to (center of the screen)
    private Vector3 startPosition; // The starting position of the object
    private float elapsedTime = 0f; // Time elapsed since the animation started
    private bool shouldSlide = false; // Flag to control sliding

    void Start()
    {
        // Ensure the main camera is not null
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera not found. Please ensure there is a camera tagged as 'MainCamera' in the scene.");
            return;
        }

        // Calculate the center position (center of the screen in world coordinates)
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        targetPosition.z = 0; // Ensure the targetPosition.z is zero

        // Find the Button component and set up the button click event listener
        Button button = GetComponent<Button>();
        if (button == null)
        {
            button = GetComponentInParent<Button>();
        }

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component not found on the GameObject or its parents");
        }
    }

    void Update()
    {
        if (shouldSlide)
        {
            Debug.Log("Hi");
            elapsedTime += Time.deltaTime; // Increment elapsed time

            // Calculate the normalized time (between 0 and 1)
            float t = Mathf.Clamp01(elapsedTime / duration);

            // Move the object using Lerp
            movingObject.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // Optional: Stop the object once it reaches the target position
            if (t >= 1.0f)
            {
                movingObject.transform.position = targetPosition; // Snap to target
                shouldSlide = false; // Stop sliding
            }
        }
    }
    

    public void OnButtonClick()
    {
        Debug.Log("Button is clicked");
        movingObject.SetActive(true);
        // Ensure the main camera is not null
        if (Camera.main == null)
        {
            Debug.LogError("Main Camera not found. Please ensure there is a camera tagged as 'MainCamera' in the scene.");
            return;
        }

        // Calculate the center position (center of the screen in world coordinates)
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        targetPosition.z = 0; // Ensure the targetPosition.z is zero

        // Set the start position to be 20 units to the right of the center
        Vector3 rightOffset = new Vector3(0, 0, 0); // 20 units to the right
        startPosition = targetPosition + rightOffset;

        // Set the object's position and animation
        movingObject.transform.position = startPosition;

        // Log the positions
        Debug.Log($"Right Position (Start): {startPosition}");
        Debug.Log($"Target Position (Center): {targetPosition}");

        // Reset elapsed time
        elapsedTime = 0f;

        // Set the flag to start sliding
        shouldSlide = true;
    }
}
