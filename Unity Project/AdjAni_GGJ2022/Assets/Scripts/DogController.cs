using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Collections;
using UnityEngine.UI;

public class DogController : MonoBehaviour
{
    public DogAnimator DogAnimator;
    public float MoveSpeed;
    public Transform DogSprite;
    public float LerpSpeed, SlowdownLerpSpeed, AccelerateLerpSpeed, AccelerateGatePercentage;
    public float MaxVelocity;
    public float PounceWindupTime, PounceJumpTime, PounceSpeed;
    [ReadOnly]
    public int MaxPounceCharges, PounceCharges;
    public float pounceRechargeTimer;
    public float PounceRechargeTime;
    public Transform PounceCirclesParent;
    public Image[] circles;

    public Vector2 velocity = Vector2.zero;
    [ReadOnly]
    public float speed, desiredSpeed;
    Vector2 lastNonZeroInput;
    Vector2 desiredVelocity;
    bool pouncing = false;

    public TextMeshPro statusText;

    // Start is called before the first frame update
    void Start()
    {
        circles= PounceCirclesParent.GetComponentsInChildren<Image>();
        MaxPounceCharges = circles.Length;
        PounceCharges = MaxPounceCharges;
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Bark", gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Breakable breakable = collision.gameObject.GetComponentInParent<Breakable>();
        if (breakable != null)
        {
            breakable.Break();
            return;
        }
        else
        {
            breakable = collision.gameObject.GetComponent<Breakable>();
            if (breakable != null)
            {
                breakable.Break();
                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ChaseObject chaseObject = collider.gameObject.GetComponent<ChaseObject>();
        if (chaseObject != null)
        {
            chaseObject.Kill();
            return;
        }
    }
    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        if(horizontalInput != 0 || verticalInput != 0)
        {
            lastNonZeroInput = new Vector2(horizontalInput, verticalInput).normalized;
        }
        DogAnimator.AnimateDog(new Vector2(horizontalInput, verticalInput) != Vector2.zero || pouncing);

        if(PounceCharges < MaxPounceCharges)
        {
            if(pounceRechargeTimer >= PounceRechargeTime)
            {
                pounceRechargeTimer = 0;
                PounceCharges++;
            }
            else
            {
                pounceRechargeTimer += Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && PounceCharges > 0)
        { 
            PounceCharges--;
            StartCoroutine(PounceAttack());
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Bark", gameObject);
        }
        UpdateChargeCircles();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        speed = velocity.magnitude;
        desiredSpeed = desiredVelocity.magnitude;
        if (GameController.Instance.PlayerHasControl)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            RotateDogSprite(horizontalInput, verticalInput);
            if (!pouncing)
            {
                desiredVelocity = new Vector2(horizontalInput, verticalInput).normalized * MaxVelocity * Time.fixedDeltaTime;
            }
            float lerpSpeed = LerpSpeed;

            if (desiredVelocity == Vector2.zero && !pouncing)
            {
                statusText.SetText("no move");

                lerpSpeed *= SlowdownLerpSpeed;
            }
            else if (velocity.magnitude > desiredVelocity.magnitude && !pouncing)
            {
                statusText.SetText("too fast");
                lerpSpeed *= SlowdownLerpSpeed * 4;
            }
            else if (desiredVelocity.magnitude * AccelerateGatePercentage > velocity.magnitude && !pouncing)
            {
                statusText.SetText("accell");

                lerpSpeed *= AccelerateLerpSpeed;
            }
            else
            {
                if (!pouncing)
                {
                    statusText.SetText("schmoovin");
                }
            }
            if (!pouncing)
            {
                velocity = Vector2.Lerp(velocity, desiredVelocity, Time.fixedDeltaTime * lerpSpeed);
            }

            transform.position += new Vector3(velocity.x, velocity.y, 0);
        }
    }

    IEnumerator PounceAttack()
    {
        statusText.SetText("Starting pounce");
        pouncing = true;

        velocity = velocity.normalized * Time.fixedDeltaTime;
        desiredVelocity = Vector2.zero;

        yield return new WaitForSeconds(PounceWindupTime);

        statusText.SetText("Pouncing");
        Vector2 pounceDirection = lastNonZeroInput.normalized;
        RotateDogSprite(pounceDirection.x, pounceDirection.y);
        velocity = pounceDirection * PounceSpeed * Time.fixedDeltaTime;
        desiredVelocity = pounceDirection * PounceSpeed * Time.fixedDeltaTime;
        GameController.Instance.ShakeCamera();

        yield return new WaitForSeconds(PounceJumpTime);

        velocity = pounceDirection * MaxVelocity* Time.fixedDeltaTime;
        desiredVelocity = Vector2.zero;
        statusText.SetText("Done pouncing");
        pouncing = false;
    }

    void UpdateChargeCircles()
    {
        for (int i = 0; i < circles.Length; i++)
        {
            if (i < PounceCharges)
            {
                circles[i].fillAmount = 1;
            }
            else if (i == PounceCharges)
            {
                circles[i].fillAmount = pounceRechargeTimer / PounceRechargeTime;
            }
            else
            {
                circles[i].fillAmount = 0;
            }
        }
    }

    private void RotateDogSprite(float horizontalInput, float verticalInput)
    {
        float newZrot = DogSprite.rotation.eulerAngles.z;

        // Terribad no good way of doing this but i'm lazy xd
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
