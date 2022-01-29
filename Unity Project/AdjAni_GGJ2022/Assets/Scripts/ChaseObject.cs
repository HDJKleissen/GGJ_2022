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

    float findPositionTimer = 0;

    bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        UpdateAliveness(true);
        GameController.Instance.RegisterChaseObject(this);
        AIDestination = new GameObject().transform;
        AIDestination.name = name + " Destination";
        AIDestinationSetter.target = AIDestination;
        AIDestination.position = FindNewRandomPosition();
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
                AIDestination.position = FindNewRandomPosition();
            }
        }
        chickenAnimator.AnimateChicken(AIPath.velocity.x > 0, AIPath.velocity.magnitude > 2f);
        previousPosition = transform.position;
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
            UpdateAliveness(false);
            // Inform gamecontroller we are dead
            GameController.Instance.KillChaseObject(this);
        }
    }

    Vector3 FindNewRandomPosition()
    {
        return new Vector3(Random.Range(LeftWall.position.x, RightWall.position.x), Random.Range(BottomWall.position.y, TopWall.position.y), 0);
    }
}
