using UnityEngine;

public class CollectableScript : MonoBehaviour
{
    private LevelManager LevelManager => GameObject.FindGameObjectWithTag("Level manager").GetComponent<LevelManager>();

    public Collectable TileData;

    public void Bootup(SceneTile C_TileData)
    {
        TileData = (Collectable)C_TileData;
        transform.position = C_TileData.Position;
        GetComponent<TileLoader>().Bootup();

        if (TileData.MeshType != TileMesh.Blank)
        {
            transform.GetChild(0).GetComponent<TileRenderer>().BootUp(LevelManager.LevelData.MeshAssignment[C_TileData.MeshType]);
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, Time.fixedUnscaledTime, 0) * 180);
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
