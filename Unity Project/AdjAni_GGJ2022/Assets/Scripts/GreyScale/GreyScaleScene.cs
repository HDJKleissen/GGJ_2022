using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class GreyScaleScene : MonoBehaviour
{
    public bool IsGreyScaled { get; set; } = false;
    [SerializeField] private List<Renderer> objects;
    // Start is called before the first frame update
    void Start()
    {
        objects = GetAllObjectsOnlyInScene();
    }

    //should be called late
    public void Shade()
    {
        if (GameController.Instance.GameState == GameState.Dog)
        {
            SetGreyScale(objects, true);
            IsGreyScaled = true;
        }
        else
        {
            SetGreyScale(objects, false);
        }
    }
	
    List<Renderer> GetAllObjectsOnlyInScene()
    {
        List<Renderer> SRInScene = new List<Renderer>();

        foreach (Renderer SR in Resources.FindObjectsOfTypeAll(typeof(Renderer)) as Renderer[])
        {
            if (!EditorUtility.IsPersistent(SR.gameObject.transform.root.gameObject) && !(SR.gameObject.hideFlags == HideFlags.NotEditable || SR.gameObject.hideFlags == HideFlags.HideAndDontSave))
                SRInScene.Add(SR);
        }

        return SRInScene;
    }

    void SetGreyScale(List<Renderer> renderers, bool on)
    {
        //recolor everything except for components that have the inverse component
        foreach(Renderer r in renderers)
        {
            bool inverse = r.gameObject.TryGetComponent(out InverseGreyScale igs);
            bool dontShade = r.gameObject.TryGetComponent(out DontGreyScale dgs);
            if (!dontShade)
            {
                r.material.SetInt("_IsGreyScale", Convert.ToInt32(on ^ inverse));
            }
        }
    }
}