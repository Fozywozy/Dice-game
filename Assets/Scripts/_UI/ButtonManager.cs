using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [HideInInspector]
    public float Scale;
    [HideInInspector]
    public float LengthSize;
    [HideInInspector]
    public bool Orientation;
    [HideInInspector]
    public bool ShowTextOptions;
    [HideInInspector]
    public string Text;

    private bool Scrolling = false;

    public void SetScrolling(bool C_Value)
    {
        Scrolling = C_Value;
    }

    private void Update()
    {
        if (Scrolling)
        {
            transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, Time.deltaTime * 2.5f);
            if (transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition.y < -5)
            {
                transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 5);
            }
        }
    }
}
