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
    Rigidbody2D rbody2D;
    // Start is called before the first frame update
    void Start()
    {
        rbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 desiredVelocity = new Vector2(horizontalInput, verticalInput).normalized * MaxVelocity * Time.fixedDeltaTime;

        float lerpSpeed = LerpSpeed;
        Debug.Log("velmag: " + velocity.magnitude);
        Debug.Log("velmag with gate: " + velocity.magnitude * AccelerateGatePercentage);
        Debug.Log("desired: " + desiredVelocity.magnitude);
        DesiredSpeedText.SetText("Desired: " + desiredVelocity + ", speed: " + desiredVelocity.magnitude);
        SpeedText.SetText("Velocity: " + velocity + ", speed: " + velocity.magnitude);
        if (desiredVelocity == Vector2.zero)
        {
            Debug.Log("slowing");
            StateText.SetText("Slowing");
            lerpSpeed *= SlowdownLerpSpeed;
        }
        else if(desiredVelocity.magnitude * AccelerateGatePercentage >= velocity.magnitude )
        {
            StateText.SetText("Accelerating");
            Debug.Log("accelerating");
            lerpSpeed *= AccelerateLerpSpeed;
        }
        else
        {
            StateText.SetText("Max speed?");
            Debug.Log("nuffin");
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
