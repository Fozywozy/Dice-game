using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GameObject PlayerObject => GameObject.FindGameObjectWithTag("Player");

    [SerializeField]
    private GameObject TilePrefab;
    [SerializeField]
    private GameObject CollectablePrefab;
    [SerializeField]
    private GameObject NodePrefab;

    public LevelSave LevelData;

    public Vector3 LastPosition;

    public void Start()
    {
        LevelData = new LevelSave();
        LoadLevel(new IONode(0, 0, 0, 0, 0, 0, 0));
    }


    public void Update()
    {

    }


    public void LoadLevel(IONode C_Node)
    {
        LevelData = LevelCatalogue.GetLevelAtIndex(C_Node.LevelTo);
        PlayerObject.transform.position = C_Node.PositionTo;
        LastPosition = C_Node.PositionTo;

        foreach (Transform T in transform)
        {
            Destroy(T.gameObject);
        }

        foreach (SceneTile TileData in LevelData.Scenetiles)
        {
            GameObject NewTile = Instantiate(TilePrefab, transform);
            NewTile.GetComponent<TileScript>().Bootup(TileData);
        }

        foreach (Collectable TileData in LevelData.Collectables)
        {
            GameObject NewTile = Instantiate(CollectablePrefab, transform);
            NewTile.GetComponent<CollectableScript>().Bootup(TileData);
        }

        foreach (IONode TileData in LevelData.Nodes)
        {
            GameObject NewTile = Instantiate(NodePrefab, transform);
            NewTile.GetComponent<ExitNodeScript>().Bootup(TileData);
        }

        LevelData.GenerateAtPositionList();
    }


}
