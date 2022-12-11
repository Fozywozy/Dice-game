using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(ButtonManager))]
public class ButtonEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ButtonManager Script = (ButtonManager)target;
        Transform Transform = Script.transform;

        Script.ShowTextOptions = EditorGUILayout.Toggle("Active", Script.ShowTextOptions);
        if (Script.ShowTextOptions)
        {
            Script.Scale = EditorGUILayout.FloatField("Scale", Script.Scale);
            Script.LengthSize = EditorGUILayout.FloatField("Length", Script.LengthSize);
            Script.Text = EditorGUILayout.TextField("Text", Script.Text);
            Script.Orientation = EditorGUILayout.Toggle("Orientation", Script.Orientation);

            Script.LengthSize = 2 * ((int)(Script.LengthSize / 2));

            for (int ChildIndex = 0; ChildIndex != 2; ChildIndex++)
            {
                float Height = ChildIndex == 0 ? 50 : 46;
                float Orientation = Script.Orientation ? 1 : -1;

                //Head
                (Transform.GetChild(0).GetChild(ChildIndex).GetChild(0) as RectTransform).sizeDelta = new Vector2(24 * Script.Scale, Height * Script.Scale);
                (Transform.GetChild(0).GetChild(ChildIndex).GetChild(0) as RectTransform).anchoredPosition = new Vector2((Script.LengthSize / 2 + 24 * Script.Scale) * -Orientation, 0);
                (Transform.GetChild(0).GetChild(ChildIndex).GetChild(0) as RectTransform).localScale = new Vector3(Orientation, 1, 1);

                //Body
                (Transform.GetChild(0).GetChild(ChildIndex).GetChild(1) as RectTransform).sizeDelta = new Vector2(24 * Script.Scale + Script.LengthSize, Height * Script.Scale);
                (Transform.GetChild(0).GetChild(ChildIndex).GetChild(1) as RectTransform).anchoredPosition = new Vector2(0, 0);

                //Tail
                (Transform.GetChild(0).GetChild(ChildIndex).GetChild(2) as RectTransform).sizeDelta = new Vector2(24 * Script.Scale, Height * Script.Scale);
                (Transform.GetChild(0).GetChild(ChildIndex).GetChild(2) as RectTransform).anchoredPosition = new Vector2((Script.LengthSize / 2 + 24 * Script.Scale) * Orientation, 0);
                (Transform.GetChild(0).GetChild(ChildIndex).GetChild(2) as RectTransform).localScale = new Vector3(Orientation, 1, 1);
            }

            (Transform.GetChild(0).GetChild(2) as RectTransform).sizeDelta = new Vector2((72 - (Script.Orientation ? 30 : 10)) * Script.Scale + Script.LengthSize, 0);
            Transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().fontSize = 24 * Script.Scale;
            Transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = Script.Text;

            (Transform.GetChild(0) as RectTransform).sizeDelta = new Vector2(72 * Script.Scale + Script.LengthSize, 46 * Script.Scale);
            (Transform as RectTransform).sizeDelta = new Vector2(72 * Script.Scale + Script.LengthSize, 46 * Script.Scale);
        }
    }
}
