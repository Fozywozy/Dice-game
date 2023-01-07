using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScroll : MonoBehaviour
{
    public ScrollDirection Scroll;
    public int DistancePerScroll;

    private float ScrolledHowFar = 0;

    private Vector2 ScrollVector => (Scroll == ScrollDirection.Horizontal) ? Vector2.right : Vector2.up;
    private float ScrolledOver => (Scroll == ScrollDirection.Horizontal)
        ? TopRightCornerScreen().x - (MaxPointButton().x + 2 * FirstButtonSize().x + LastButtonSize().x)
        : MinPointButton().y - (LastButtonSize().y / 2) - BottomLeftCornerScreen().y;


    private Vector2 FirstButtonSize()
    {
        return transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().PixelScale();
    }


    private Vector2 LastButtonSize()
    {
        return transform.GetChild(0).GetChild(transform.GetChild(0).childCount - 1).GetComponent<RectTransform>().PixelScale();
    }


    private Vector2 TopRightCornerScreen()
    {
        return GetComponent<RectTransform>().PixelPosition() + (GetComponent<RectTransform>().PixelScale() / 2);
    }


    private Vector2 BottomLeftCornerScreen()
    {
        return GetComponent<RectTransform>().PixelPosition() - (GetComponent<RectTransform>().PixelScale() / 2);
    }


    private Vector2 MaxPointButton()
    {
        Vector2 Output = Vector2.negativeInfinity;

        foreach (Transform T in transform.GetChild(0))
        {
            RectTransform RT = T.GetComponent<RectTransform>();

            Vector2 RTMaximums = new Vector2(
                RT.PixelPosition().x + RT.PixelScale().x / 2f,
                RT.PixelPosition().y + RT.PixelScale().y / 2f);

            Output.x = Mathf.Max(new float[] { RTMaximums.x, Output.x });
            Output.y = Mathf.Max(new float[] { RTMaximums.y, Output.y });
        }

        return Output;
    }


    private Vector2 MinPointButton()
    {
        Vector2 Output = Vector2.positiveInfinity;

        foreach (Transform T in transform.GetChild(0))
        {
            RectTransform RT = T.GetComponent<RectTransform>();

            Vector2 RTMaximums = new Vector2(
                RT.PixelPosition().x - RT.PixelScale().x / 2f,
                RT.PixelPosition().y - RT.PixelScale().y / 2f);

            Output.x = Mathf.Min(new float[] { RTMaximums.x, Output.x });
            Output.y = Mathf.Min(new float[] { RTMaximums.y, Output.y });
        }

        return Output;
    }


    private void Awake()
    {
        ChangeScroll(0);
    }


    private void Update()
    {
        int ScrollScale = (Input.mouseScrollDelta.y > 0) ? 1 : (Input.mouseScrollDelta.y < 0) ? -1 : 0;

        if (ScrollScale != 0)
        {
            ChangeScroll(ScrollScale);
        }
    }


    private void ChangeScroll(float C_ScrollChange)
    {
        ScrolledHowFar -= DistancePerScroll * C_ScrollChange;

        if (ScrolledHowFar < 0) { ScrolledHowFar = 0; }
        if (ScrolledOver > 0) { ScrolledHowFar -= ScrolledOver; }

        transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition =
            ScrollVector * (ScrolledHowFar - ((Scroll == ScrollDirection.Horizontal) ? FirstButtonSize().x / 2 : FirstButtonSize().y / 2));

        SetScroll();
    }


    public void SetScroll()
    {
        for (int ChildIndex = 0; ChildIndex < transform.GetChild(0).childCount; ChildIndex++)
        {
            RectTransform RT = transform.GetChild(0).GetChild(ChildIndex).GetComponent<RectTransform>();

            float MinPosition = (Scroll == ScrollDirection.Horizontal)
                ? BottomLeftCornerScreen().x
                : BottomLeftCornerScreen().y;
                
            float MaxPosition = (Scroll == ScrollDirection.Horizontal)
                ? TopRightCornerScreen().x
                : TopRightCornerScreen().y;

            float ButtonSize = (Scroll == ScrollDirection.Horizontal)
                ? RT.PixelScale().x
                : RT.PixelScale().y;

            float ButtonPosition = (Scroll == ScrollDirection.Horizontal)
                ? RT.PixelPosition().x
                : RT.PixelPosition().y;

            float Opacity = 1;

            if (ButtonPosition < MinPosition + ButtonSize)
            {
                Opacity = (ButtonPosition - MinPosition) / ButtonSize;
            }

            if (ButtonPosition > MaxPosition - ButtonSize)
            {
                Opacity = (MaxPosition - ButtonPosition) / ButtonSize;
            }

            if (Opacity < 0) { Opacity = 0; }

            if (RT.TryGetComponent(out _RaycastAsset ButtonManager))
            {
                ButtonManager.SetOpacity(Opacity);
            }
        }
    }
}


public enum ScrollDirection
{
    Horizontal,
    Vertical,
}
