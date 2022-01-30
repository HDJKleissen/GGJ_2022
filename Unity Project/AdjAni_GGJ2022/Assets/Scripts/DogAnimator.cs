using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnimator : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer SpriteRenderer;

    public void AnimateDog(bool moving, float verticalSpeed)
    {
        if (Time.timeScale != 0)
        {
            int verticalDir = (int)Mathf.Sign(verticalSpeed);
            if(verticalSpeed != 0)
            {
                SpriteRenderer.flipY = verticalSpeed < 0;
                animator.SetInteger("VerticalSpeed", verticalDir);
            }
            animator.SetBool("Moving", moving);
        }
    }
}
