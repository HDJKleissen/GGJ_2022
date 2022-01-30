using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    public void PlayUIConfirm()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_Confirm");
    }

    public void PlayUIBack()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_Back");
    }
}
