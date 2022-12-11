using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class _RaycastAsset : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnClick;
    [SerializeField]
    private UnityEvent<bool> OnHover;

    public void OnClicked()
    {
        OnClick.Invoke();
    }

    public void OnHovered(bool C_EnterExit)
    {
        OnHover.Invoke(C_EnterExit);
    }
}
