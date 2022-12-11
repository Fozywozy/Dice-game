using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AddressManager
{
    public static AddressableListReference<Mesh> MeshAddressables = new AddressableListReference<Mesh>();
    public static AddressableListReference<Material> MaterialAddressables = new AddressableListReference<Material>();

}

public class AddressableListReference<T> where T : class
{
    public List<GetAddressable<T>> AddressableAssets = new List<GetAddressable<T>>();

    /// <summary>
    /// Removes one request from the asset requested
    /// </summary>
    public void RemoveAssetAtPath(AddressableReference C_Path)
    {
        GetAddressable<T> Address = GetAssetAtPath(C_Path);

        if (C_Path == Address.Path)
        {
            Address.RequestCount--;
            if (Address.RequestCount <= 0)
            {
                Debug.Log("Mesh no longer needed");
                _ = AddressableAssets.Remove(Address);
                return;
                //Nothing needing this anymore
            }
        }
    }


    /// <summary>
    /// Returns the Mesh at the given path, returns null if it isn't loaded yet, does not modify anything.
    /// </summary>
    public T ReturnAssetAtPath(AddressableReference C_Path)
    {
        GetAddressable<T> Address = GetAssetAtPath(C_Path);

        if (C_Path == Address.Path)
        {
            if (Address.Output != null)
            {
                return Address.Output;
            }
            else
            {
                return null;
            }
        }

        AddressableAssets.Add(new GetAddressable<T>(C_Path));
        return null;
    }


    /// <summary>
    /// Starts loading the Mesh at the given path. If it is already loading, it adds one to the requested count
    /// </summary>
    public void NewAssetAtPath(AddressableReference C_Path)
    {
        if (GetAssetAtPath(C_Path) != null)
        {
            GetAssetAtPath(C_Path).RequestCount++;
        }
        else
        {
            //GetAddressable Does not exist
            AddressableAssets.Add(new GetAddressable<T>(C_Path));
        }
    }


    /// <summary>
    /// Returns null if theat path does not have a GetAddressable
    /// </summary>
    private GetAddressable<T> GetAssetAtPath(AddressableReference C_Path)
    {
        foreach (GetAddressable<T> Address in AddressableAssets)
        {
            if (C_Path == Address.Path)
            {
                return Address;
            }
        }
        return null;
    }
}


public class GetAddressable<T> where T : class
{
    public int RequestCount = 1;
    public AddressableReference Path;
    public T Output = null;

    public GetAddressable(AddressableReference C_Path)
    {
        Path = C_Path;
        AsyncOperationHandle<T> Handler = Addressables.LoadAssetAsync<T>(C_Path.String);
        Handler.Completed += Handler_Completed;
    }

    private void Handler_Completed(AsyncOperationHandle<T> obj)
    {
        Output = obj.Result;
    }
}


public class AddressableReference
{
    public string AddressableTag;  //The tag the addressable is, E.g. Weapon, Walls
    public string AddressableName; //The addressables name

    public string String => (AddressableTag ?? "") + ":" + (AddressableName ?? "");

    public AddressableReference(string C_Tag = null, string C_Name = null)
    {
        AddressableTag = C_Tag;
        AddressableName = C_Name;
    }
}


public class MeshRenderingAsset
{
    public AddressableReference MeshReference;
    public List<AddressableReference> MaterialReferences = new List<AddressableReference>();

    public MeshRenderingAsset(List<string> C_AssetAddresses)
    {
        MeshReference = new AddressableReference(C_AssetAddresses[0], C_AssetAddresses[1]);
        for (int AssetIndex = 2; AssetIndex < C_AssetAddresses.Count; AssetIndex += 2)
        {
            MaterialReferences.Add(new AddressableReference(C_AssetAddresses[AssetIndex], C_AssetAddresses[AssetIndex + 1]));
        }
    }

    public MeshRenderingAsset()
    {

    }
}
