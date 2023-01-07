using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private ExitBeam ExitBeam => GameObject.FindGameObjectWithTag("Exit beam").GetComponent<ExitBeam>();
    private GameObject PlayerObject => GameObject.FindGameObjectWithTag("Player");
    private Background BackgroundManager => GameObject.FindGameObjectWithTag("Background Manager").GetComponent<Background>();
    private FogManager FogManager => GameObject.FindGameObjectWithTag("Fog Manager").GetComponent<FogManager>();
    private MusicManager MusicManager => GameObject.FindGameObjectWithTag("Music Manager").GetComponent<MusicManager>();
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

    public bool Return;


    /// <summary>
    /// For switching from level to level
    /// </summary>
    public void LoadLevel(int C_Level, Vector3Int C_Position)
    {
        if (Return)
        {
            BackToMenu();
            return;
        }

        LevelData = LevelCatalogue.GetLevelAtIndex(C_Level);
        LastPosition = C_Position;
        PlayerObject.GetComponent<Player>().BackToCheckpoint(true);
        MusicManager.PlaySong(LevelData.SongIndex);
        BackgroundManager.SetMode(LevelData.BackgroundType);

        foreach (Transform T in transform)
        {
            T.GetComponent<TileLoader>().LoadOut();
        }

        List<SceneTile> NewScenetiles = new List<SceneTile>();

        foreach (SceneTile TileData in LevelData.Scenetiles)
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
                            NewScenetiles.Add(CreateObject(New));
                        }
                    }
                }
            }
            else
            {
                NewScenetiles.Add(CreateObject(TileData));
            }
        }

        LevelData.Scenetiles = NewScenetiles;
        LevelData.GenerateAtPositionList();
    }


    /// <summary>
    /// For switching from menu mode to level mode
    /// </summary>
    public void LoadLevel(int C_Level, bool C_Return)
    {
        Return = C_Return;
        LevelData = LevelCatalogue.GetLevelAtIndex(C_Level);
        Canvas.transform.GetChild(0).GetComponent<MenuFade>().Fade(false);
        Canvas.transform.GetChild(1).GetComponent<MenuFade>().Fade(true);
        ExitBeam.EnterAt(Vector3.zero, true);

        Mode = C_Return ? GameMode.LevelReturn : GameMode.LevelEndless;
        FogManager.LerpTo(new Color(0.4f, 0.4f, 0.4f, 1), 1);
        MusicManager.PlaySong(LevelData.SongIndex);
        BackgroundManager.SetMode(LevelData.BackgroundType);

        List<SceneTile> NewScenetiles = new List<SceneTile>();

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
                            NewScenetiles.Add(CreateObject(New));
                        }
                    }
                }
            }
            else
            {
                NewScenetiles.Add(CreateObject(TileData));
            }
        }

        LevelData.Scenetiles = NewScenetiles;
        LevelData.GenerateAtPositionList();
    }


    public SceneTile CreateObject(SceneTile C_TileData)
    {
        switch (C_TileData.Type)
        {
            case TileType.Wall:
                //Create tile prefab
                GameObject NewWall = Instantiate(TilePrefab, transform);
                C_TileData.Parent = NewWall;
                NewWall.GetComponent<WallScript>().Bootup(C_TileData);
                break;

            case TileType.Scaffold:
                //Create tile prefab
                GameObject NewScaffold = Instantiate(TilePrefab, transform);
                C_TileData.Parent = NewScaffold;
                NewScaffold.GetComponent<WallScript>().Bootup(C_TileData);
                break;

            case TileType.Null:
                //Do nothing
                break;

            case TileType.PushPiston:
                //Create a piston prefab
                GameObject NewPushPiston = Instantiate(PistonPrefab, transform);
                C_TileData.Parent = NewPushPiston;
                NewPushPiston.GetComponent<PistonScript>().Bootup(C_TileData);
                break;

            case TileType.PivotPiston:
                //Create a piston prefab
                GameObject NewPivortPiston = Instantiate(PistonPrefab, transform);
                C_TileData.Parent = NewPivortPiston;
                NewPivortPiston.GetComponent<PistonScript>().Bootup(C_TileData);
                break;

            case TileType.Collectable:
                //Create a collectable prefab
                GameObject NewCollectable = Instantiate(CollectablePrefab, transform);
                C_TileData.Parent = NewCollectable;
                NewCollectable.GetComponent<CollectableScript>().Bootup(C_TileData);
                break;

            case TileType.DangerTile:
                //Create a timer tile prefab
                GameObject NewDangerTile = Instantiate(TimerPrefab, transform);
                C_TileData.Parent = NewDangerTile;
                NewDangerTile.GetComponent<TimerScript>().Bootup(C_TileData);
                break;

            case TileType.PushTile:
                //Create a timer tile prefab
                GameObject NewPushTile = Instantiate(TimerPrefab, transform);
                C_TileData.Parent = NewPushTile;
                NewPushTile.GetComponent<TimerScript>().Bootup(C_TileData);
                break;

            case TileType.TextPoint:
                //Do nothing
                break;

            case TileType.IONode:
                //Create a node prefab
                GameObject NewIONode = Instantiate(NodePrefab, transform);
                C_TileData.Parent = NewIONode;
                NewIONode.GetComponent<IONodeScript>().Bootup(C_TileData);
                break;

            case TileType.SuperNode:
                //Create a node prefab

                break;

            case TileType.Checkpoint:
                //Do nothing
                break;

            case TileType.RenderingFeature:
                //Create a rendering feature prefab
                GameObject NewRenderingFeature = Instantiate(RenderingFeaturePrefab, transform);
                C_TileData.Parent = NewRenderingFeature;
                NewRenderingFeature.GetComponent<RenderTypeScript>().Bootup(C_TileData);
                break;
        }

        return C_TileData;
    }


    public void BackToMenu()
    {
        LastPosition = Vector3.zero;
        ExitBeam.ExitAt(PlayerObject.transform.position, false, false);
        Canvas.transform.GetChild(0).GetComponent<MenuFade>().Fade(true);
        Canvas.transform.GetChild(1).GetComponent<MenuFade>().Fade(false);
        Mode = GameMode.Menu;
        BackgroundManager.SetMode(BackgroundType.Boxy);
        FogManager.LerpTo(new Color(0.32f, 0.4f, 0.4f, 1), 1);
        MusicManager.PlaySong(0);

        foreach (Transform T in transform)
        {
            T.GetComponent<TileLoader>().LoadOut();
        }
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
