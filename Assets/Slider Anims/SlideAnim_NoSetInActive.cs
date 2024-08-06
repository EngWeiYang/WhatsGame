using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideAnim_NoSetInActive : MonoBehaviour
{
    private Animator animator;

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
}
