using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRenderer : MonoBehaviour
{
    private bool MeshLoaded = false;
    private bool MaterialsLoaded = false;

    private Mesh Mesh;
    private Material[] Materials;

    public MeshRenderingAsset AssetAddresses;


    public void BootUp(MeshRenderingAsset C_AssetAddresses)
    {
        enabled = true;
        AssetAddresses = C_AssetAddresses;

        AddressManager.MeshAddressables.NewAssetAtPath(AssetAddresses.MeshReference);
        foreach (AddressableReference Reference in AssetAddresses.MaterialReferences)
        {
            AddressManager.MaterialAddressables.NewAssetAtPath(Reference);
        }
    }


    public void Update()
    {
        if (!MeshLoaded || !MaterialsLoaded)
        {
            if (!MeshLoaded)
            {
                Mesh AddressableMesh = AddressManager.MeshAddressables.ReturnAssetAtPath(AssetAddresses.MeshReference);
                if (AddressableMesh != null)
                {
                    GetComponent<MeshFilter>().mesh = AddressableMesh;
                    Mesh = AddressableMesh;
                    AddressManager.MeshAddressables.RemoveAssetAtPath(AssetAddresses.MeshReference);
                    MeshLoaded = true;
                }
            }

            if (!MaterialsLoaded)
            {
                bool Loaded = true;

                foreach (AddressableReference Reference in AssetAddresses.MaterialReferences)
                {
                    if (AddressManager.MaterialAddressables.ReturnAssetAtPath(Reference) == null)
                    {
                        Loaded = false;
                    }
                }

                if (Loaded)
                {
                    List<Material> NewMaterials = new List<Material>();

                    foreach (AddressableReference Reference in AssetAddresses.MaterialReferences)
                    {
                        NewMaterials.Add(AddressManager.MaterialAddressables.ReturnAssetAtPath(Reference));
                    }
                    Materials = NewMaterials.ToArray();
                    GetComponent<MeshRenderer>().materials = NewMaterials.ToArray();
                    MaterialsLoaded = true;
                }
            }
        }
        else
        {
            enabled = false;
        }
    }


    public void UpdateGraphics()
    {
        GetComponent<MeshFilter>().mesh = Mesh;
        GetComponent<MeshRenderer>().materials = Materials;
    }
}
