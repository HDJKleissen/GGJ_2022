using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimator : MonoBehaviour
{
    public Animator animator;

    public void AnimateChicken(bool moving)
    {
        animator.SetBool("Moving", moving);
    }
}