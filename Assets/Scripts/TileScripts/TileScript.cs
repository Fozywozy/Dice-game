using UnityEngine;

public class TileScript : MonoBehaviour
{
    public SceneTile TileData;

    public void Bootup(SceneTile C_TileData)
    {
        TileData = C_TileData;
        transform.position = C_TileData.Position;
        transform.localScale = C_TileData.Scale;
    }

    void Update()
    {

    }
}
