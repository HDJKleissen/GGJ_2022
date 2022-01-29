using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public OwnerAnimator OwnerAnimator;

    [field: SerializeField] public float MovementSpeed { get; private set; }

    private Rigidbody2D body;

    private float horizontalInput;
    private float verticalInput;
    private float moveLimiter = 0.7f;

    [SerializeField] private List<Breakable> interactingObjects;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GameController.Instance.PlayerHasControl)
        {
            // Gives a value between -1 and 1
            horizontalInput = Input.GetAxisRaw("Horizontal"); // -1 is left
            verticalInput = Input.GetAxisRaw("Vertical"); // -1 is down


            //Rotate();
            OwnerAnimator.AnimatePlayer(new Vector2(horizontalInput, verticalInput));
            UpdateObjectInteraction();
        }
        else
        {
            OwnerAnimator.AnimatePlayer(Vector2.zero);
        }
    }

    void UpdateObjectInteraction()
    {
        List<Breakable> interactables = interactingObjects.FindAll(obj => obj.ItemBroken && !obj.ItemFixed);
        foreach(Breakable b in interactables)
        {
            b.Fix();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Breakable breakable = collision.gameObject.GetComponentInParent<Breakable>();
        if (breakable != null)
        {
            if (breakable.FixableObject != null && breakable.ItemBroken && !breakable.ItemFixed)
              {
                breakable.FixableObject.gameObject.SetActive(true);
                interactingObjects.Add(breakable);
            }
        }
        else
        {
            breakable = collision.gameObject.GetComponent<Breakable>();
            if (breakable != null)
            {
                if (breakable.FixableObject != null && breakable.ItemBroken && !breakable.ItemFixed)
                {
                    breakable.FixableObject.gameObject.SetActive(true);
                    interactingObjects.Add(breakable);
                }
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Breakable breakable = collision.gameObject.GetComponentInParent<Breakable>();
        if (breakable != null)
        {
            if (breakable.FixableObject != null)
                breakable.FixableObject.gameObject.SetActive(false);
            interactingObjects.Remove(breakable);
        }
        else
        {
            breakable = collision.gameObject.GetComponent<Breakable>();
            if (breakable != null)
            {
                if (breakable.FixableObject != null)
                    breakable.FixableObject.gameObject.SetActive(false);
                interactingObjects.Remove(breakable);
            }
        }
    }

    //spritedefault rotation should be looking right
    private void Rotate()
    {
        Vector2 moveDirection = gameObject.GetComponent<Rigidbody2D>().velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void Move()
    {
        if (horizontalInput != 0 && verticalInput != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontalInput *= moveLimiter;
            verticalInput *= moveLimiter;
        }

        body.velocity = new Vector2(horizontalInput * MovementSpeed, verticalInput * MovementSpeed);
    }
    

    void FixedUpdate()
    {
        if (GameController.Instance.PlayerHasControl)
        {
            Move();
        }
    }
}
