using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneAudio : MonoBehaviour
{
    public void PlaySurpriseBark()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/CutScene_Bark");
    }

    public void PlayPounceBark()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Bark");
    }

    public void PlayBok()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/CutScene_Bok");
    }

    public void PlaySquawk()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Chicken_Die");
    }

    public void PlayWTFDog()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/WTF_Dog");
    }

    public void PlayBagDrop()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Drop_Bags");
    }

    public void PlaySnore()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Dog_Snore_Double");
    }

    public void PlaySingleBok()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Single_Bok");
    }
}

