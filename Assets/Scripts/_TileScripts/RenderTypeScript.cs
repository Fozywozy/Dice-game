using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTypeScript : MonoBehaviour
{
    private LevelManager LevelManager => GameObject.FindGameObjectWithTag("Level manager").GetComponent<LevelManager>();

    public RenderType TileData;

    public void Bootup(SceneTile C_TileData)
    {
        TileData = (RenderType)C_TileData;
        transform.position = C_TileData.Position;
        transform.localScale = C_TileData.Scale;
        GetComponent<TileLoader>().Bootup();
        if (TileData.MeshType != TileMesh.Blank)
        {
            transform.GetChild(0).GetComponent<TileRenderer>().BootUp(LevelManager.LevelData.MeshAssignment[C_TileData.MeshType]);
        }
    }

    public void Update()
    {
        
    }

    public void CheckTrigger()
    {

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
