using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MenuFade))]
public class MenuFadeEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MenuFade Script = (MenuFade)target;

        if (Script.ActiveOnPlay != EditorGUILayout.Toggle("Active on play", Script.ActiveOnPlay))
        {
            Script.ActiveOnPlay = !Script.ActiveOnPlay;
            Script.Visible = Script.ActiveOnPlay;
            Script.SetFade(Script.transform, Script.ActiveOnPlay ? 1 : 0);
        }
    }
}
