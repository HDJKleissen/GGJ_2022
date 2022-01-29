using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DG.Tweening;

public class ShakeEffect : MonoBehaviour
{
    [SerializeField] private Transform target;

    [field: Space, Header("Rotation Effect")]
    [field: SerializeField, Range(0f, 360f)] public float ShakeStrength { get; private set; }
    [field: SerializeField, Range(0f, 2f)] public float ShakeDuration { get; private set; }
    [field: SerializeField, Range(0f, 20f)] public int  ShakeVibrato { get; private set; }
    [field: SerializeField, Range(0, 180)] public int  ShakeRandomness { get; private set; }

    [field: Space, Header("Scale Effect")]
    [field: SerializeField, Range(0f, 360f)] public float ScaleStrength { get; private set; }
    [field: SerializeField, Range(0f, 2f)] public float ScaleDuration { get; private set; }
    [field: SerializeField, Range(0f, 20f)] public int ScaleVibrato { get; private set; }
    [field: SerializeField, Range(0, 180)] public int ScaleRandomness { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Test();
    }

    void Test()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShakeTarget();
        }
    }

    void ShakeTarget()
    {
        Debug.Log("Shaking..");
        target.DOShakeRotation(ShakeDuration, new Vector3(0, 0, ShakeStrength), ShakeVibrato, ShakeRandomness);
        target.DOShakeScale(ScaleDuration, new Vector3(0, 0, ScaleStrength), ScaleVibrato, ScaleRandomness);

    }
}
