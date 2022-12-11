using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperNodeScript : MonoBehaviour
{
    private LevelManager LevelManager => GameObject.FindGameObjectWithTag("Level manager").GetComponent<LevelManager>();

    public SuperNode TileData;

    public void Bootup(SceneTile C_TileData)
    {
        TileData = (SuperNode)C_TileData;
        transform.position = C_TileData.Position;
        transform.localScale = C_TileData.Scale;
        GetComponent<TileLoader>().Bootup();
        if (TileData.MeshType != TileMesh.Blank)
        {
            transform.GetChild(0).GetComponent<TileRenderer>().BootUp(LevelManager.LevelData.MeshAssignment[C_TileData.MeshType]);
        }
    }

    public void PlayerOn()
    {

    }

    public void PlayerOff()
    {

    }

    public void PlayerIn()
    {

    }

    public void PlayerOut()
    {

    }
}
