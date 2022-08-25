using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    public Collectable TileData;

    public void Bootup(Collectable C_TileData)
    {
        TileData = C_TileData;
        transform.position = C_TileData.Position;
    }

    void Update()
    {
        
    }
}
