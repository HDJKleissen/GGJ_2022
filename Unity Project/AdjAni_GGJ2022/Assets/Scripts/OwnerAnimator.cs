using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnerAnimator : MonoBehaviour
{
    public Animator Animator;
    public SpriteRenderer SpriteRenderer;

    int verticalLookDirection;
    int horizontalLookDirection;
    bool moving;

    public void AnimatePlayer(Vector2 moveDirection)
    {
        Animator.SetBool("Moving", moveDirection != Vector2.zero);

        if (moveDirection != Vector2.zero)
        {
            if (moveDirection.y != 0)
            {
                verticalLookDirection = (int)Mathf.Sign(moveDirection.y);
            }
            
            horizontalLookDirection = (int)Mathf.Sign(moveDirection.x);
            SpriteRenderer.flipX = horizontalLookDirection == -1;
            Animator.SetInteger("VerticalLook", verticalLookDirection);
        }
        else
        {
            verticalLookDirection = -1;
        }
    }

    public void Sweep()
    {
        Animator.SetTrigger("Sweep");
    }
}
