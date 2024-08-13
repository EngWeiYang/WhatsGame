using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideAnimLevel12 : MonoBehaviour
{
    private Animator animator;
    public GameObject objectToDeactivate;
    public GameObject objectToActivate;

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
    }

    public void Activate()
    {
        objectToActivate.SetActive(true);
    }
}
