using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenCleanIndicator : MonoBehaviour
{
    [SerializeField] Transform goal;
    [SerializeField] float rotSpeed = 15.0f;
    private float rotToSetSpritedefaultRight = 90;
    // Start is called before the first frame update
    void OnEnable()
    {
        Vector3 vectorToTarget = goal.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + rotToSetSpritedefaultRight;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vectorToTarget = goal.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + rotToSetSpritedefaultRight;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotSpeed);
    }
}
