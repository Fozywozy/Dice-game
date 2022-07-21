using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class AddressableListReference<T> where T : class
{
    public List<GetAddressable<T>> AddressableAssets = new List<GetAddressable<T>>();

    /// <summary>
    /// Removes one request from the asset requested
    /// </summary>
    public void RemoveAssetAtPath(AddressableReference C_Path)
    {
        GetAddressable<T> Address = GetAtPath(C_Path);

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
    public T ReturnMeshAtPath(AddressableReference C_Path) //Checks if it got the mesh, if it has, it returns it
    {
        GetAddressable<T> Address = GetAtPath(C_Path);

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
    public T NewMeshAtPath(AddressableReference C_Path)
    {
        GetAddressable<T> Address = GetAtPath(C_Path);

        if (C_Path == Address.Path)
        {
            if (Address.Output != null)
            {
                return Address.Output;
            }
            else
            {
                Address.RequestCount++;
                return null;
            }
        }

        AddressableAssets.Add(new GetAddressable<T>(C_Path));
        return null;
    }


    private GetAddressable<T> GetAtPath(AddressableReference C_Path)
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
