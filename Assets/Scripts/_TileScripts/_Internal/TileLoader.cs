using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLoader : MonoBehaviour
{
    [SerializeField]
    private Material Transparent;

    public bool LoadingOut = false;
    public Timer LoadInTimer;
    public Timer LoadOutTimer;

    public bool StartedLoadIn = false;
    public bool StartedLoadOut = false;


    private List<Vector3> Positions = new List<Vector3>();
    private List<Material[]> MeshRenderers = new List<Material[]>();
    private List<Color?> SpriteRenderers = new List<Color?>();


    public void Bootup()
    {
        LoadIn();
    }


    public void Update()
    {
        if (LoadingOut)
        {
            //Load out
            if (LoadOutTimer.Active)
            {
                //Timer started
                if (!StartedLoadOut)
                {
                    //Initialize original variables
                    StartedLoadOut = true;
                    StartLoad();
                }

                if (LoadOutTimer)
                {
                    //Finished loading
                    Destroy(gameObject);
                }
                else
                {
                    //Still Loading
                    for (int I = 0; I != transform.childCount; I++)
                    {
                        transform.GetChild(I).position = Positions[I] + (Vector3.down * LoadOutTimer);
                    }

                    SetMaterialOpacity(1 - LoadOutTimer);
                }
            }
        }
        else
        {
            //Load in
            if (LoadInTimer.Active)
            {
                //Timer started
                if (!StartedLoadIn)
                {
                    //Initialize original variables
                    StartedLoadIn = true;
                    StartLoad();
                }

                if (LoadInTimer)
                {
                    //Finished loading
                    for (int I = 0; I != transform.childCount; I++)
                    {
                        transform.GetChild(I).position = Positions[I];
                    }

                    ResetMaterial();
                    enabled = false;
                    Positions = new List<Vector3>();
                    MeshRenderers = new List<Material[]>();
                    SpriteRenderers = new List<Color?>();
                }
                else
                {
                    //Still Loading
                    for (int I = 0; I != transform.childCount; I++)
                    {
                        transform.GetChild(I).position = Positions[I] + (Vector3.up * (1 - LoadInTimer));
                    }

                    SetMaterialOpacity(LoadInTimer);
                }
            }
        }
    }


    public void LoadIn()
    {
        enabled = true;
        LoadInTimer = new Timer(0.5f, 1f + transform.position.magnitude * 0.1f);
    }


    public void StartLoad()
    {
        foreach (Transform T in transform)
        {
            Positions.Add(T.position);

            if (T.TryGetComponent(out MeshRenderer MeshRednerer))
            {
                MeshRenderers.Add(MeshRednerer.materials);
            }
            else
            {
                MeshRenderers.Add(null);
            }

            if (T.TryGetComponent(out SpriteRenderer SpriteRednerer))
            {
                SpriteRenderers.Add(SpriteRednerer.color);
            }
            else
            {
                SpriteRenderers.Add(null);
            }
        }
    }


    public void LoadOut()
    {
        enabled = true;
        LoadingOut = true;
        LoadOutTimer = new Timer(0.5f, 0.5f + transform.position.magnitude * 0.1f);
    }


    public void SetMaterialOpacity(float C_OpacityMultiplier)
    {
        for (int TransformIndex = 0; TransformIndex != transform.childCount; TransformIndex++)
        {
            if (transform.GetChild(TransformIndex).TryGetComponent(out MeshRenderer MeshOut))
            {
                Material[] OriginalRenderer = MeshRenderers[TransformIndex];

                List<Material> NewMaterials = new List<Material>();
                for (int Index = 0; Index != OriginalRenderer.Length; Index++)
                {
                    Material Mat = new Material(Transparent);

                    Mat.SetColor("_EmissionColor", OriginalRenderer[Index].GetColor("_EmissionColor"));
                    Mat.SetFloat("_Metallic", OriginalRenderer[Index].GetFloat("_Metallic"));
                    Mat.SetFloat("_Smoothness", OriginalRenderer[Index].GetFloat("_Smoothness"));

                    Color NewColor = OriginalRenderer[Index].color;
                    NewColor.a *= C_OpacityMultiplier;
                    Mat.color = NewColor;

                    NewMaterials.Add(Mat);
                }
                MeshOut.materials = NewMaterials.ToArray();
                MeshOut.enabled = true;
            }

            if (transform.GetChild(TransformIndex).TryGetComponent(out SpriteRenderer SpriteOut))
            {
                SpriteOut.enabled = true;
                Color OriginalRenderer = SpriteRenderers[TransformIndex].Value;

                Color NewColor = OriginalRenderer;
                NewColor.a *= C_OpacityMultiplier;
                SpriteOut.color = NewColor;
            }
        }
    }


    public void ResetMaterial()
    {
        for (int TransformIndex = 0; TransformIndex != transform.childCount; TransformIndex++)
        {
            if (transform.GetChild(TransformIndex).TryGetComponent(out TileRenderer MeshOut))
            {
                MeshOut.UpdateGraphics();
            }

            if (transform.GetChild(TransformIndex).TryGetComponent(out SpriteRenderer SpriteOut))
            {
                Color OriginalRenderer = SpriteRenderers[TransformIndex].Value;

                SpriteOut.color = OriginalRenderer;
            }
        }
    }
}
