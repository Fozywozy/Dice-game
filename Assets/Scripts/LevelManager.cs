using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private GameObject Splashscreen => GameObject.FindGameObjectWithTag("Splash");
    private GameObject PlayerObject => GameObject.FindGameObjectWithTag("Player");
    private ExitBeam ExitBeam => GameObject.FindGameObjectWithTag("Exit beam").GetComponent<ExitBeam>();
    private FogManager FogManager => GameObject.FindGameObjectWithTag("Fog Manager").GetComponent<FogManager>();
    private GameObject Canvas => GameObject.FindGameObjectWithTag("Canvas");

    [SerializeField]
    private GameObject TilePrefab;
    [SerializeField]
    private GameObject CollectablePrefab;
    [SerializeField]
    private GameObject NodePrefab;
    [SerializeField]
    private GameObject PistonPrefab;
    [SerializeField]
    private GameObject TimerPrefab;
    [SerializeField]
    private GameObject RenderingFeaturePrefab;

    public GameMode Mode = GameMode.Menu;

    public LevelSave LevelData;
    public Vector3 LastPosition = Vector3.zero;

    public bool BootUp = false;
    public bool Splash = true;

    public void Start()
    {
        if (!BootUp)
        {
            //DontDestroyOnLoad(this);
            TimerManager.NewTimer("SplashFadeIn", 0.3f, 7);
            TimerManager.NewTimer("TextFadeIn", 0.3f, 7.2f);
            TimerManager.NewTimer("SplashFadeOut", 0.3f, 8.7f);
            TimerManager.NewTimer("BlackroundFadeOut", 0.3f, 9.7f);
        }
    }


    public void Update()
    {
        if (Splash)
        {
            Splashscreen.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 1, TimerManager.GetTimer("TextFadeIn"));
            Splashscreen.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, TimerManager.GetTimer("SplashFadeIn"));
            if (TimerManager.GetTimer("SplashFadeOut").Active)
            {
                Splashscreen.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 1, 1 - TimerManager.GetTimer("TextFadeIn"));
                Splashscreen.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1 - TimerManager.GetTimer("SplashFadeOut"));
            }

            Splashscreen.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 1 - TimerManager.GetTimer("BlackroundFadeOut"));

            if (TimerManager.GetTimer("BlackroundFadeOut"))
            {
                //Splash Completed
                Splashscreen.SetActive(false);
                Splash = false;
                TimerManager.RemoveTimer("SplashFadeIn");
                TimerManager.RemoveTimer("TextFadeIn");
                TimerManager.RemoveTimer("SplashFadeOut");
                TimerManager.RemoveTimer("BlackroundFadeOut");
            }
        }
    }


    /// <summary>
    /// For switching from level to level
    /// </summary>
    public void LoadLevel(int C_Level, Vector3Int C_Position)
    {
        ExitBeam.ExitAt(PlayerObject.transform.position);
        LevelData = LevelCatalogue.GetLevelAtIndex(C_Level);
        LevelData.GenerateAtPositionList();
        LastPosition = C_Position;
        PlayerObject.GetComponent<Player>().BackToCheckpoint();

        foreach (Transform T in transform)
        {
            T.GetComponent<TileLoader>().LoadOut();
        }

        foreach (SceneTile TileData in LevelCatalogue.GetLevelAtIndex(C_Level).Scenetiles)
        {
            if (TileData.Breakable)
            {
                Vector3 StartPosition = TileData.Position - new Vector3(TileData.Scale.x / 2f, TileData.Scale.y / 2f, TileData.Scale.z / 2f);
                Vector3 EndPosition = TileData.Position + new Vector3(TileData.Scale.x / 2f, TileData.Scale.y / 2f, TileData.Scale.z / 2f);

                for (float x = StartPosition.x; x < EndPosition.x; x++)
                {
                    for (float y = StartPosition.y; y < EndPosition.y; y++)
                    {
                        for (float z = StartPosition.z; z < EndPosition.z; z++)
                        {
                            SceneTile New = TileData.Clone();
                            New.Position = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
                            New.Scale = Vector3Int.one;
                            CreateObject(New);
                        }
                    }
                }
            }
            else
            {
                CreateObject(TileData);
            }
        }
    }


    public void CreateObject(SceneTile C_TileData)
    {
        switch (C_TileData.Type)
        {
            case TileType.Wall:
                //Create tile prefab
                GameObject NewWall = Instantiate(TilePrefab, transform);
                NewWall.GetComponent<WallScript>().Bootup(C_TileData);
                NewWall.GetComponent<WallScript>().TileData.Parent = NewWall;
                break;

            case TileType.Scaffold:
                //Do nothing
                break;

            case TileType.Null:
                //Do nothing
                break;

            case TileType.PushPiston:
                //Create a piston prefab
                GameObject NewPushPiston = Instantiate(PistonPrefab, transform);
                NewPushPiston.GetComponent<PistonScript>().Bootup(C_TileData);
                NewPushPiston.GetComponent<PistonScript>().TileData.Parent = NewPushPiston;
                break;

            case TileType.PivotPiston:
                //Create a piston prefab
                GameObject NewPivortPiston = Instantiate(PistonPrefab, transform);
                NewPivortPiston.GetComponent<PistonScript>().Bootup(C_TileData);
                NewPivortPiston.GetComponent<PistonScript>().TileData.Parent = NewPivortPiston;
                break;

            case TileType.Collectable:
                //Create a collectable prefab
                GameObject NewCollectable = Instantiate(CollectablePrefab, transform);
                NewCollectable.GetComponent<CollectableScript>().Bootup(C_TileData);
                NewCollectable.GetComponent<CollectableScript>().TileData.Parent = NewCollectable;
                break;

            case TileType.DangerTile:
                //Create a timer tile prefab
                GameObject NewDangerTile = Instantiate(TimerPrefab, transform);
                NewDangerTile.GetComponent<TimerScript>().Bootup(C_TileData);
                NewDangerTile.GetComponent<TimerScript>().TileData.Parent = NewDangerTile;
                break;

            case TileType.PushTile:
                //Create a timer tile prefab
                GameObject NewPushTile = Instantiate(TimerPrefab, transform);
                NewPushTile.GetComponent<TimerScript>().Bootup(C_TileData);
                NewPushTile.GetComponent<TimerScript>().TileData.Parent = NewPushTile;
                break;

            case TileType.TextPoint:
                //Do nothing
                break;

            case TileType.IONode:
                //Create a node prefab
                GameObject NewIONode = Instantiate(NodePrefab, transform);
                NewIONode.GetComponent<IONodeScript>().Bootup(C_TileData);
                NewIONode.GetComponent<IONodeScript>().TileData.Parent = NewIONode;
                break;

            case TileType.SuperNode:
                //Create a node prefab

                break;

            case TileType.Checkpoint:
                //Do nothing
                break;
        }
    }


    public void BackToMenu()
    {
        LastPosition = Vector3.zero;
        Canvas.transform.GetChild(0).gameObject.SetActive(true);
        Canvas.transform.GetChild(1).gameObject.SetActive(false);
        Mode = GameMode.Menu;
        FogManager.LerpTo(new Color(0.32f, 0.4f, 0.4f, 1), 1);
    }


    /// <summary>
    /// For switching from menu mode to level mode
    /// </summary>
    public void LoadLevel(int C_Level, bool C_Return)
    {
        LevelData = LevelCatalogue.GetLevelAtIndex(C_Level);
        LevelData.GenerateAtPositionList();
        ExitBeam.ExitAt(Vector3.zero);
        Canvas.transform.GetChild(0).gameObject.SetActive(false);
        Canvas.transform.GetChild(1).gameObject.SetActive(true);
        PlayerObject.GetComponent<Player>().BackToCheckpoint();

        foreach (SceneTile TileData in LevelCatalogue.GetLevelAtIndex(C_Level).Scenetiles)
        {
            if (TileData.Breakable)
            {
                Vector3 StartPosition = TileData.Position - new Vector3(TileData.Scale.x / 2f, TileData.Scale.y / 2f, TileData.Scale.z / 2f);
                Vector3 EndPosition = TileData.Position + new Vector3(TileData.Scale.x / 2f, TileData.Scale.y / 2f, TileData.Scale.z / 2f);

                for (float x = StartPosition.x; x < EndPosition.x; x++)
                {
                    for (float y = StartPosition.y; y < EndPosition.y; y++)
                    {
                        for (float z = StartPosition.z; z < EndPosition.z; z++)
                        {
                            SceneTile New = TileData.Clone();
                            New.Position = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
                            New.Scale = Vector3Int.one;
                            CreateObject(New);
                        }
                    }
                }
            }
            else
            {
                CreateObject(TileData);
            }
        }

        Mode = C_Return ? GameMode.LevelReturn : GameMode.LevelEndless;
        FogManager.LerpTo(new Color(0.4f, 0.4f, 0.4f, 1), 1);
    }


    public void LoadLevelReturn(int C_Level)
    {
        LoadLevel(C_Level, true);
    }

    public void LoadLevelContinuous(int C_Level)
    {
        LoadLevel(C_Level, false);
    }

    public enum GameMode
    {
        Menu,
        LevelReturn,
        LevelEndless,
    }
}
