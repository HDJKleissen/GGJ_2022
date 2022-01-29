using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAnimator : MonoBehaviour
{
    public Animator animator;

    public void AnimateDog(bool moving)
    {
        animator.SetBool("Moving", moving);
    }
}