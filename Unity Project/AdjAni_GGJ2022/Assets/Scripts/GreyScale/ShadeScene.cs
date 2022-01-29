using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class ShadeScene : MonoBehaviour
{
    bool dogScene = true;
    [SerializeField] private List<SpriteRenderer> objects;
    // Start is called before the first frame update
    void Start()
    {
        objects = GetAllObjectsOnlyInScene();
        SetGreyScale(objects, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<SpriteRenderer> GetAllObjectsOnlyInScene()
    {
        List<SpriteRenderer> SRInScene = new List<SpriteRenderer>();

        foreach (SpriteRenderer SR in Resources.FindObjectsOfTypeAll(typeof(SpriteRenderer)) as SpriteRenderer[])
        {
            if (!EditorUtility.IsPersistent(SR.gameObject.transform.root.gameObject) && !(SR.gameObject.hideFlags == HideFlags.NotEditable || SR.gameObject.hideFlags == HideFlags.HideAndDontSave))
                SRInScene.Add(SR);
        }

        return SRInScene;
    }

    void SetGreyScale(List<SpriteRenderer> renderers, bool on)
    {
        //recolor everything except for components that are inversely colored
        foreach(SpriteRenderer r in renderers)
        {
            if(!r.gameObject.TryGetComponent(out InverseGreyScale igs))
            {
                r.material.SetInt("_IsGreyScale", Convert.ToInt32(on));
            }
        }
    }
}
