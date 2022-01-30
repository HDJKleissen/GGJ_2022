using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneController : MonoBehaviour
{
    public Animator animator;
    public UnityEvent OnFinishCutscene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   if(IsAnimationFinished())
        {
            OnFinishCutscene.Invoke();
        }
    }
    public bool IsAnimationFinished()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }
}
