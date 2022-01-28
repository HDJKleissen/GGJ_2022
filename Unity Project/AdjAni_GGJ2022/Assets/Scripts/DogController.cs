using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DogController : MonoBehaviour
{
    public float MoveSpeed;
    public Transform DogSprite;
    public float LerpSpeed, SlowdownLerpSpeed, AccelerateLerpSpeed, AccelerateGatePercentage;
    public float MaxVelocity;

    public TextMeshProUGUI StateText, SpeedText, DesiredSpeedText;

    public Vector2 velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Breakable breakable = collision.gameObject.GetComponent<Breakable>();
        if (breakable != null)
        {
            breakable.Break();
            return;
        }

        ChaseObject chaseObject = collision.gameObject.GetComponent<ChaseObject>();
        if (chaseObject != null)
        {
            chaseObject.Kill();
            return;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 desiredVelocity = new Vector2(horizontalInput, verticalInput).normalized * MaxVelocity * Time.fixedDeltaTime;

        float lerpSpeed = LerpSpeed;

        DesiredSpeedText.SetText("Desired: " + desiredVelocity + ", speed: " + desiredVelocity.magnitude);
        SpeedText.SetText("Velocity: " + velocity + ", speed: " + velocity.magnitude);

        if (desiredVelocity == Vector2.zero)
        {
            StateText.SetText("Slowing");
            lerpSpeed *= SlowdownLerpSpeed;
        }
        else if(desiredVelocity.magnitude * AccelerateGatePercentage >= velocity.magnitude )
        {
            StateText.SetText("Accelerating");
            lerpSpeed *= AccelerateLerpSpeed;
        }
        else
        {
            StateText.SetText("Max speed?");
        }
        velocity = Vector2.Lerp(velocity, desiredVelocity, Time.fixedDeltaTime * lerpSpeed);

        RotateDogSprite(horizontalInput, verticalInput);
        transform.position += new Vector3(velocity.x, velocity.y, 0);
    }

    private void RotateDogSprite(float horizontalInput, float verticalInput)
    {
        float newZrot = DogSprite.rotation.eulerAngles.z;

        if (horizontalInput < 0)
        {
            if (verticalInput < 0)
            {
                newZrot = 145;

            }
            else if (verticalInput > 0)
            {
                newZrot = 45;
            }
            else
            {
                newZrot = 90;
            }
        }
        else if (horizontalInput > 0)
        {
            if (verticalInput < 0)
            {
                newZrot = 225;
            }
            else if (verticalInput > 0)
            {
                newZrot = 315;
            }
            else
            {
                newZrot = 270;
            }
        }
        else
        {
            if (verticalInput < 0)
            {
                newZrot = 180;
            }
            else if (verticalInput > 0)
            {
                newZrot = 0;
            }
            else
            {
                // Do nothing
            }
        }

        DogSprite.rotation = Quaternion.Euler(0, 0, newZrot);
    }
}
