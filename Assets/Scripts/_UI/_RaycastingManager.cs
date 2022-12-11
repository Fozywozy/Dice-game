using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class _RaycastingManager : MonoBehaviour
{
    private Camera MainCamera => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    private EventSystem EventSystem => transform.GetChild(0).GetComponent<EventSystem>();

    //Hit Data
    [HideInInspector]
    public static RaycastHit? WorldHitData;
    [HideInInspector]
    public static RaycastResult? UIHitData;

    //World hit data quicktypes
    public static Vector3? WorldHitPoint => WorldHitData?.point;
    public static Vector3? WorldHitNormal => WorldHitData?.normal;
    public static GameObject WorldHitGameobject => WorldHitData?.transform.gameObject;
    public static Transform WorldHitTransform => WorldHitData?.transform;
    public static string WorldHitName => WorldHitData?.transform.name;

    //UI hit data quicktypes
    public static Vector3? UIHitPoint => UIHitData?.worldPosition;
    public static Vector3? UIHitNormal => UIHitData?.worldNormal;
    public static GameObject UIHitGameobject => UIHitData?.gameObject;
    public static Transform UIHitTransform => UIHitData?.gameObject.transform;
    public static string UIHitName => UIHitData?.gameObject.name;

    //Static outputs
    public static bool HitWorld = false;
    public static bool HoverWorld = false;
    public static bool HitUI = false;
    public static bool HoverUI = false;

    //Internal values
    private GameObject HoverLastFrame = null;

    private Ray Ray => MainCamera.ScreenPointToRay(Input.mousePosition);


    private PointerEventData PEData()
    {
        PointerEventData Output = new PointerEventData(EventSystem);
        Output.position = Input.mousePosition;
        return Output;
    }


    private void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, (MainCamera.transform.position - Camera.main.transform.position).normalized, Color.blue, 2);
        UIRaycasting();
        WorldRaycasting();
    }


    private void UIRaycasting()
    {
        List<RaycastResult> Results = new List<RaycastResult>();
        EventSystem.RaycastAll(PEData(), Results);

        if (Results.Count == 0)
        {
            UIHitData = null;
            HoverUI = false;
            HoverUICheck(null);
        }
        else
        {
            if (Results[0].gameObject == null)
            {
                UIHitData = null;
                HoverUI = false;
                HoverUICheck(null);
            }
            else
            {
                //Hit ui
                UIHitData = Results[0];
                HoverUI = true;
                HoverUICheck(UIHitData.Value.gameObject);
            }
        }

        if (UIHitData != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (UIHitData.Value.gameObject != null)
                {
                    HitUICheck(UIHitData.Value.gameObject);
                }
            }
            else
            {
                if (UIHitData.Value.gameObject != null)
                {
                    HoverUICheck(UIHitData.Value.gameObject);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            UIHitData = null;
        }
    }


    private void WorldRaycasting()
    {

        if (Physics.Raycast(Ray, out RaycastHit Hit, 300f, LayerMask.GetMask("Default")))
        {
            //Hit world
            WorldHitData = Hit;
            //Debug.Log("World " + Hit.transform.name);
            HoverWorld = true;
        }
        else
        {
            HoverWorld = false;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) { WorldHitData = null; }
    }


    public static Vector3 WorldHitToCanvasPosition(RectTransform C_Canvas)
    {
        Vector3 Output = C_Canvas.InverseTransformPoint(WorldHitPoint.Value);
        return Output;
    }


    private void HoverUICheck(GameObject C_Hover)
    {
        if (HoverLastFrame != C_Hover)
        {
            if (C_Hover != null)
            {
                if (C_Hover.TryGetComponent(out _RaycastAsset NewAsset))
                {
                    NewAsset.OnHovered(true);
                }
            }

            if (HoverLastFrame != null)
            {
                if (HoverLastFrame.TryGetComponent(out _RaycastAsset OldAsset))
                {
                    OldAsset.OnHovered(false);
                }
            }
            HoverLastFrame = C_Hover;
        }
    }


    private void HitUICheck(GameObject C_Hit)
    {
        if (C_Hit.TryGetComponent(out _RaycastAsset NewAsset))
        {
            NewAsset.OnClicked();
        }
    }
}
