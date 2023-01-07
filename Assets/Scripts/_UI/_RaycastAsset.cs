using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class _RaycastAsset : MonoBehaviour
{
    private bool Active = false;

    [Header("On Click")]
    [SerializeField]
    private bool ClickWhenActive;
    [SerializeField]
    private UnityEvent OnClick;

    [Header("On Hover Enter")]
    [SerializeField]
    private bool HoverEnterWhenActive;
    [SerializeField]
    private UnityEvent OnHoverEnter;

    [Header("On Hover Exit")]
    [SerializeField]
    private bool HoverExitWhenActive;
    [SerializeField]
    private UnityEvent OnHoverExit;

    private void Awake()
    {
        _RaycastingManager.AddObjectToRaycastingAssets(transform);

        CheckActive();
    }


    public void OnClicked()
    {
        if (Active || (!ClickWhenActive))
        {
            OnClick.Invoke();
        }
    }


    public void OnHoverEntered()
    {
        if (Active || (!HoverEnterWhenActive))
        {
            OnHoverEnter.Invoke();
        }
    }


    public void OnHoverExited()
    {
        if (Active || (!HoverExitWhenActive))
        {
            OnHoverExit.Invoke();
        }
    }


    public void SetOpacity(float C_Opacity)
    {
        GetComponent<CanvasGroup>().alpha = C_Opacity;

        CheckActive();
    }


    public void CheckActive()
    {
        Active = GetEffectiveOpacity() switch
        {
            0 => false,
            _ => true
        };
    }


    private float GetEffectiveOpacity()
    {
        float Opacity = GetComponent<CanvasGroup>().alpha;

        Transform Object = transform.parent;

        while (true)
        {
            if (Object.TryGetComponent(out CanvasGroup CanvasGroup))
            {
                Opacity *= CanvasGroup.alpha;
            }

            if (Object.parent == null)
            {
                return Opacity;
            }
            else
            {
                Object = Object.parent;
            }
        }
    }
}
