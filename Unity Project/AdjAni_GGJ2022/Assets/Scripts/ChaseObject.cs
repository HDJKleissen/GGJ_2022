using Pathfinding;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseObject : MonoBehaviour
{
    public GameObject AliveSprite;
    public GameObject DeadSprite;
    public AIDestinationSetter AIDestinationSetter;
    public AIPath AIPath;
    public Transform Dog;
    public ChickenAnimator chickenAnimator;
    public Rigidbody2D rigbod2D;
    public Transform LeftWall, RightWall, TopWall, BottomWall;
    public float TimeUntilNewPosition;
    Transform AIDestination;
    Vector3 previousPosition;
    FMOD.Studio.EventInstance ChickenSounds;

    float findPositionTimer = 0;

    bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        ChickenSounds = FMODUnity.RuntimeManager.CreateInstance("event:/Chicken_Pok");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(ChickenSounds, transform);
        ChickenSounds.start();
        ChickenSounds.release();
        UpdateAliveness(true);
        GameController.Instance.RegisterChaseObject(this);
        AIDestination = new GameObject().transform;
        AIDestination.name = name + " Destination";
        AIDestinationSetter.target = AIDestination;
        FindNewRandomDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            
            if (findPositionTimer < TimeUntilNewPosition && !AIPath.reachedEndOfPath)
            {
                findPositionTimer += Time.deltaTime;
            }
            else
            {
                findPositionTimer = 0;
                FindNewRandomDestination();
            }
        }
        chickenAnimator.AnimateChicken(AIPath.velocity.x > 0, AIPath.velocity.magnitude > 2f);
        previousPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (alive)
        {
            if (GameController.Instance.GameState == GameState.Dog)
            {
                if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Chicken")
                {
                    FindNewRandomDestination();
                    findPositionTimer = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Door" && GameController.Instance.GameState == GameState.Owner)
        {
            GameController.Instance.CleanChaseObject(this);
        }
    }

    void UpdateAliveness(bool isAlive)
    {
        alive = isAlive;
        AIPath.canMove = alive;
        DeadSprite.SetActive(!alive);
        AliveSprite.SetActive(alive);
    }

    public void Kill()
    {
        //HandleKill();
        if (alive)
        {
            ChickenSounds.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Chicken_Die", gameObject);
            UpdateAliveness(false);
            // Inform gamecontroller we are dead
            GameController.Instance.KillChaseObject(this);
        }
    }

    void FindNewRandomDestination()
    {
        AIDestination.position = new Vector3(Random.Range(LeftWall.position.x, RightWall.position.x), Random.Range(BottomWall.position.y, TopWall.position.y), 0);
    }
}
