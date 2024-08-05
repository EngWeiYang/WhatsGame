using UnityEngine;
using UnityEngine.UI;

public class SlideAnim : MonoBehaviour
{
    private Animator animator;
    public GameObject objectToDeactivate;
    public GameObject objectToDeactivate2;

    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();
    }

    // Function to set the trigger
    public void TriggerSlideAnimation()
    {
        // Set the trigger parameter to start the animation
        animator.SetTrigger("Button pressed");
    }

    public void Deactivate()
    {
        objectToDeactivate.SetActive(false);
        objectToDeactivate2.SetActive(false);
    }
}

