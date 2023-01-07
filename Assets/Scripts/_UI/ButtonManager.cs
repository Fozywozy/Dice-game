using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public float OrientationMultiplier => Orientation ? 1 : -1;


    private void Update()
    {
        if (Scrolling)
        {
            transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, Time.deltaTime * 7.5f);

            if (transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition.y < -15)
            {
                transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 15);
            }
        }
    }


    public void SetScrolling(bool C_Value)
    {
        Scrolling = C_Value;
    }
}
