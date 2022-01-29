using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimator : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer sprenderer;

    public void AnimateChicken(bool facingRight, bool moving)
    {
        sprenderer.flipX = facingRight;
        animator.SetBool("Moving", moving);
    }
}
