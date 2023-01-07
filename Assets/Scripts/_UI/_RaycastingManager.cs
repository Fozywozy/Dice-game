using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class _RaycastingManager : MonoBehaviour
{
    private Camera MainCamera => GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    //Hit Data
    [HideInInspector]
    public static RaycastHit? WorldHitData;

    //World hit data quicktypes
    public static Vector3? WorldHitPoint => WorldHitData?.point;
    public static Vector3? WorldHitNormal => WorldHitData?.normal;
    public static GameObject WorldHitGameobject => WorldHitData?.transform.gameObject;
    public static Transform WorldHitTransform => WorldHitData?.transform;
    public static string WorldHitName => WorldHitData?.transform.name;

    //Static outputs
    public static bool HitWorld = false;
    public static bool HoverWorld = false;

    //Editor Values
    [SerializeField]
    private string HitObjectName;
    [SerializeField]
    private string HoverObjectName;

    //Internal Values
    private Ray Ray => MainCamera.ScreenPointToRay(Input.mousePosition);
    private static UIRaycastingAsset UIRaycastAsset = new UIRaycastingAsset();


    private void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, (MainCamera.transform.position - Camera.main.transform.position).normalized, Color.blue, 2);
        UIRaycasting();
        WorldRaycasting();
    }


    private void UIRaycasting()
    {
        UIRaycastAsset.CheckObjects();
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


    public static void AddObjectToRaycastingAssets(Transform C_Transform)
    {
        UIRaycastAsset.AddNewAsset(C_Transform.GetComponent<RectTransform>());
    }
}

public class UIRaycastingAsset
{
    //The pixel position of the mouse
    private Vector2 UIHitLocation => Input.mousePosition;

    //A list of transforms that were hovered over last frame
    private List<RectTransform> HoverLastFrame = new List<RectTransform>();

    //A list of objects with the _RaycastAsset monobehavour attached to them
    private List<RectTransform> RaycastableObjects = new List<RectTransform>();


    public bool CursorIn(RectTransform C_Transform)
    {
        bool InXRange = Mathf.Abs(UIHitLocation.x - C_Transform.PixelPosition().x) < (C_Transform.PixelScale().x / 2);
        bool InYRange = Mathf.Abs(UIHitLocation.y - C_Transform.PixelPosition().y) < (C_Transform.PixelScale().y / 2);

        return InXRange && InYRange;
    }

    public void AddNewAsset(RectTransform C_Transform)
    {
        RaycastableObjects.Add(C_Transform);
    }


    public void CheckObjects()
    {
        List<RectTransform> HoverThisFrame = new List<RectTransform>();

        foreach (RectTransform T in RaycastableObjects)
        {
            if (T.gameObject.activeInHierarchy)
            {
                if (CursorIn(T))
                {
                    HoverThisFrame.Add(T);

                    //Check Hover entry
                    if (!HoverLastFrame.Contains(T))
                    {
                        T.GetComponent<_RaycastAsset>().OnHoverEntered();
                    }

                    //Check Hit
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        T.GetComponent<_RaycastAsset>().OnClicked();
                    }
                }
            }
        }

        //Check Hover exit
        foreach (RectTransform Hover in HoverLastFrame)
        {
            if (!HoverThisFrame.Contains(Hover))
            {
                Hover.GetComponent<_RaycastAsset>().OnHoverExited();
            }
        }

        HoverLastFrame = HoverThisFrame;
    }
}
