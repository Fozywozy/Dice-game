using UnityEngine;

public class WallScript : MonoBehaviour
{
    private LevelManager LevelManager => GameObject.FindGameObjectWithTag("Level manager").GetComponent<LevelManager>();

    public SceneTile TileData;

    public void Bootup(SceneTile C_TileData)
    {
        TileData = C_TileData;
        transform.position = C_TileData.Position;
        transform.localScale = C_TileData.Scale;
        GetComponent<TileLoader>().Bootup();

        if (TileData.MeshType != TileMesh.Blank)
        {
            transform.GetChild(0).GetComponent<TileRenderer>().BootUp(LevelManager.LevelData.MeshAssignment[C_TileData.MeshType]);
        }
    }
}
