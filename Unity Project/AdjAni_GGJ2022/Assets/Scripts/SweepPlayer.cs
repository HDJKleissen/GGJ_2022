using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepPlayer : MonoBehaviour
{
    public void PlaySweepSound()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Sweep", gameObject);
    }
}
