using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShakeEffectInCinemachine))]
public class ShakeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ShakeEffectInCinemachine shakeEffect = (ShakeEffectInCinemachine)target;
        EditorGUILayout.Space();
        if(GUILayout.Button("Shake Dat Screen!"))
        {
            shakeEffect.Shake();
        }

        DrawDefaultInspector();
    }
}
