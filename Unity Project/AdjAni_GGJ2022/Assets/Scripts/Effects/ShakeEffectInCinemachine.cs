using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;
using DG.Tweening;

public class ShakeEffectInCinemachine : MonoBehaviour
{
    // Cinemachine Shake
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    [field: Space, Header("ShakeRot Effect")]
    [field: SerializeField, Range(0, 1)] public float ShakeDuration { get; private set; } = 0.3f;          // Time the Camera Shake effect will last
    [field: SerializeField, Range(0, 10)] public float ShakeAmplitude { get; private set; } = 1.2f;         // Cinemachine Noise Profile Parameter
    [field: SerializeField, Range(0, 10)] public float ShakeFrequency { get; private set; } = 2.0f;         // Cinemachine Noise Profile Parameter

    [field: Space, Header("ShakeZoom Effect")]
    [field: SerializeField, Range(0, 1)] public float ZoomDuration { get; private set; } = 0.2f;
    [field: SerializeField, Range(0, 5)] public float ZoomScale { get; private set; } = 0.3f;

    private float DefaultOrthographicSize;
    private float ShakeElapsedTime = 0f;


    // Use this for initialization
    void Start()
    {
        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null)
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

        DefaultOrthographicSize = VirtualCamera.m_Lens.OrthographicSize;
    }

    public void Shake()
    {
        ShakeElapsedTime = ShakeDuration;
        var ZoomSequence = DOTween.Sequence();
        
        ZoomSequence.Append(DOTween.To(() => VirtualCamera.m_Lens.OrthographicSize, 
                x => VirtualCamera.m_Lens.OrthographicSize = x, 
                DefaultOrthographicSize + ZoomScale, ZoomDuration/2).SetEase(Ease.InOutBounce));

        ZoomSequence.Append(DOTween.To(() => VirtualCamera.m_Lens.OrthographicSize,
                x => VirtualCamera.m_Lens.OrthographicSize = x,
                DefaultOrthographicSize, ZoomDuration / 2).SetEase(Ease.InOutBounce));
    }

    private void Test()
    {
        // TODO: Replace with your trigger
        if (Input.GetKeyDown(KeyCode.S))
        {
            Shake();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Test();

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;

                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }
}