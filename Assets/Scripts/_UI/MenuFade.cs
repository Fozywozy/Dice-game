using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFade : MonoBehaviour
{
    [HideInInspector]
    public bool ActiveOnPlay;

    [HideInInspector]
    public bool Visible;

    private Timer FadeTimer;
    private bool? FadeIn;

    private void Awake()
    {
        Visible = ActiveOnPlay;
    }

    private void Update()
    {
        if (FadeIn != null)
        {
            float Percent = FadeIn.Value ? FadeTimer : 1 - FadeTimer;

            if (FadeTimer)
            {
                enabled = false;
            }

            SetFade(transform, Percent);
        }
    }


    /// <summary>
    /// True for fade in, false for fade out
    /// </summary>
    public void Fade(bool C_FadeInOrOut)
    {
        Visible = C_FadeInOrOut;
        enabled = true;
        FadeIn = C_FadeInOrOut;
        FadeTimer = new Timer(0.25f, 0.25f);
    }


    public void SetFade(Transform C_Transform, float C_Percent)
    {
        foreach (Transform T in C_Transform)
        {
            int OrientationMultiplier = (T.GetComponent<RectTransform>().anchoredPosition.x > 0) ? 1 : -1;

            if (T.TryGetComponent(out ButtonManager _))
            {
                T.GetChild(0).GetComponent<RectTransform>().anchoredPosition = 50 * (1 - C_Percent) * OrientationMultiplier * Vector2.right;
                T.GetComponent<_RaycastAsset>().SetOpacity(C_Percent);
                continue;
            }

            if (T.TryGetComponent(out ButtonScroll _))
            {
                T.GetChild(0).GetComponent<RectTransform>().anchoredPosition = 50 * (1 - C_Percent) * OrientationMultiplier * Vector2.right + T.GetChild(0).GetComponent<RectTransform>().anchoredPosition * Vector2.up;
                T.GetChild(1).GetComponent<RectTransform>().anchoredPosition = 50 * (1 - C_Percent) * OrientationMultiplier * Vector2.right + T.GetChild(0).GetComponent<RectTransform>().anchoredPosition * Vector2.up;
                T.GetComponent<_RaycastAsset>().SetOpacity(C_Percent);
                continue;
            }

            if (T.TryGetComponent(out MenuFade Fade))
            {
                if (Fade.Visible)
                {
                    Fade.SetFade(T, C_Percent);
                }
                continue;
            }

            if (T.TryGetComponent(out Image Image))
            {
                Image.color = new Color(1, 1, 1, C_Percent);
            }

            SetFade(T, C_Percent);
        }
    }
}
