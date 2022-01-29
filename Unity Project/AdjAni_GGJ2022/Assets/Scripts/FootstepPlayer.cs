using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{

    public void PlayDogFootstep()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Dog_Step", gameObject);
    }

    public void PlayOwnerFootstep()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Owner_Step", gameObject);
    }
}
