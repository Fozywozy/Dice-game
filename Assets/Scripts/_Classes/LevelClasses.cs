using System.Collections.Generic;
using UnityEngine;


public class LevelSave
{
    public BackgroundType BackgroundType;
    public int SongIndex;
    public List<SceneTile> Scenetiles;

    public DictionaryList<Vector3Int, SceneTile> TileAtPosition = new DictionaryList<Vector3Int, SceneTile>();
    public Dictionary<TileMesh, MeshRenderingAsset> MeshAssignment = new Dictionary<TileMesh, MeshRenderingAsset>();


    public void GenerateAtPositionList()
    {
        TileAtPosition = new DictionaryList<Vector3Int, SceneTile>();

        foreach (SceneTile TileData in Scenetiles)
        {
            Vector3 StartPosition = TileData.Position - new Vector3(TileData.Scale.x / 2f, TileData.Scale.y / 2f, TileData.Scale.z / 2f);
            Vector3 EndPosition = TileData.Position + new Vector3(TileData.Scale.x / 2f, TileData.Scale.y / 2f, TileData.Scale.z / 2f);

            for (float x = StartPosition.x; x < EndPosition.x; x++)
            {
                for (float y = StartPosition.y; y < EndPosition.y; y++)
                {
                    for (float z = StartPosition.z; z < EndPosition.z; z++)
                    {
                        TileAtPosition.Add(Vector3Int.RoundToInt(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f)), TileData);
                    }
                }
            }
        }
    }

    public List<SceneTile> TilesAt(Vector3Int C_Position)
    {
        if (TileAtPosition.Dictionary.ContainsKey(C_Position))
        {
            return TileAtPosition.Dictionary[C_Position];
        }
        return null;
    }


    public LevelSave(BackgroundType C_BackgroundType, int C_SongIndex, List<SceneTile> C_Scenetiles = null, Dictionary<TileMesh, MeshRenderingAsset> C_MeshAssignment = null)
    {
        BackgroundType = C_BackgroundType;
        SongIndex = C_SongIndex;
        Scenetiles = C_Scenetiles ?? new List<SceneTile>();

        Dictionary<TileMesh, MeshRenderingAsset> DefaultDictionary = new Dictionary<TileMesh, MeshRenderingAsset>
        {
            //Default
            [TileMesh.Wall1] = new MeshRenderingAsset(new List<string> { "System", "Cube", "Opaque", "Dull gray" }),
            [TileMesh.Wall2] = new MeshRenderingAsset(new List<string> { "Tile", "Bridge", "Opaque", "Dull gray", "Opaque", "Dull green" }),
            [TileMesh.Wall3] = new MeshRenderingAsset(new List<string> { "Tile", "Pad", "Opaque", "Dull gray", "Opaque", "Dull green" }),
            [TileMesh.Wall4] = new MeshRenderingAsset(new List<string> { "Tile", "Pillar", "Opaque", "Dull gray" }),
            [TileMesh.Wall5] = new MeshRenderingAsset(new List<string> { "Tile", "Scaffold", "Transparent", "Dull yellow" }),
            [TileMesh.Wall6] = new MeshRenderingAsset(new List<string> { "System", "Cube", "Opaque", "Dull gray" }),
            [TileMesh.Wall7] = new MeshRenderingAsset(new List<string> { "System", "Cube", "Opaque", "Dull gray" }),
            [TileMesh.Wall8] = new MeshRenderingAsset(new List<string> { "System", "Cube", "Opaque", "Dull gray" }),
            [TileMesh.Wall9] = new MeshRenderingAsset(new List<string> { "System", "Cube", "Opaque", "Dull gray" }),
            [TileMesh.Wall10] = new MeshRenderingAsset(new List<string> { "System", "Cube", "Opaque", "Dull gray" }),
            [TileMesh.Wall11] = new MeshRenderingAsset(new List<string> { "System", "Cube", "Opaque", "Dull gray" }),
            [TileMesh.Wall12] = new MeshRenderingAsset(new List<string> { "System", "Cube", "Opaque", "Dull gray" }),
            [TileMesh.Wall13] = new MeshRenderingAsset(new List<string> { "System", "Cube", "Opaque", "Dull gray" }),
            [TileMesh.Wall14] = new MeshRenderingAsset(new List<string> { "System", "Cube", "Opaque", "Dull gray" }),
            [TileMesh.Wall15] = new MeshRenderingAsset(new List<string> { "System", "Cube", "Opaque", "Dull gray" }),
            [TileMesh.Wall16] = new MeshRenderingAsset(new List<string> { "System", "LightningBolt", "System", "Lightning", "System", "Lightning" }),

            [TileMesh.Collectable1] = new MeshRenderingAsset(new List<string> { "Collectable", "Coin", "Opaque", "Shiny yellow" }),
            [TileMesh.Collectable2] = new MeshRenderingAsset(new List<string> { "Collectable", "Coin", "Opaque", "Shiny yellow" }),
            [TileMesh.Collectable3] = new MeshRenderingAsset(new List<string> { "Collectable", "Coin", "Opaque", "Shiny yellow" }),
            [TileMesh.Collectable4] = new MeshRenderingAsset(new List<string> { "Collectable", "Coin", "Opaque", "Shiny yellow" }),
            [TileMesh.Collectable5] = new MeshRenderingAsset(new List<string> { "Collectable", "Coin", "Opaque", "Shiny yellow" }),
            [TileMesh.Collectable6] = new MeshRenderingAsset(new List<string> { "Collectable", "Coin", "Opaque", "Shiny yellow" }),
            [TileMesh.Collectable7] = new MeshRenderingAsset(new List<string> { "Collectable", "Coin", "Opaque", "Shiny yellow" }),
            [TileMesh.Collectable8] = new MeshRenderingAsset(new List<string> { "Collectable", "Coin", "Opaque", "Shiny yellow" }),

            [TileMesh.IONode1] = new MeshRenderingAsset(new List<string> { "IONode", "Blank", "Opaque", "Dull gray", "Opaque", "Dull green" }),
            [TileMesh.IONode2] = new MeshRenderingAsset(new List<string> { "IONode", "Blank", "Opaque", "Dull gray", "Opaque", "Dull green" }),
            [TileMesh.IONode3] = new MeshRenderingAsset(new List<string> { "IONode", "Blank", "Opaque", "Dull gray", "Opaque", "Dull green" }),
            [TileMesh.IONode4] = new MeshRenderingAsset(new List<string> { "IONode", "Blank", "Opaque", "Dull gray", "Opaque", "Dull green" }),

            [TileMesh.Piston1] = new MeshRenderingAsset(new List<string> { "Tile", "Push Piston", "Opaque", "Dull gray", "Opaque", "Dull green" }),
            [TileMesh.Piston2] = new MeshRenderingAsset(new List<string> { "Tile", "Pivot Piston", "Opaque", "Dull gray", "Opaque", "Dull green" }),
            [TileMesh.Piston3] = new MeshRenderingAsset(new List<string> { "System", "Cube", "Opaque", "Dull gray" }),
            [TileMesh.Piston4] = new MeshRenderingAsset(new List<string> { "System", "Cube", "Opaque", "Dull gray" }),
        };
        Dictionary<TileMesh, MeshRenderingAsset> NewDicitonary = new Dictionary<TileMesh, MeshRenderingAsset>();

        if (C_MeshAssignment == null)
        {
            NewDicitonary = DefaultDictionary;
        }
        else
        {
            foreach (TileMesh TileMesh in DefaultDictionary.Keys)
            {
                if (C_MeshAssignment.ContainsKey(TileMesh))
                {
                    NewDicitonary.Add(TileMesh, C_MeshAssignment[TileMesh]);
                }
                else
                {
                    NewDicitonary.Add(TileMesh, DefaultDictionary[TileMesh]);
                }
            }
        }

        MeshAssignment = NewDicitonary;
    }
}

public class SceneTile
{
    public string Name => "(" + Type + ", " + MeshType + ", " + Position.x + ", " + Position.y + ", " + Position.z + ")";

    //Default
    public Vector3 Position;
    public Vector3Int Scale = Vector3Int.one;
    public TileType Type;
    public TileMesh MeshType;
    public bool Breakable = false;
    public GameObject Parent;

    public bool Solid()
    {
        return Type switch
        {
            TileType.Wall => true,
            TileType.Scaffold => false,
            TileType.Null => false,

            //Pistons
            TileType.PushPiston => true,
            TileType.PivotPiston => true,

            //Collectables
            TileType.Collectable => false,

            //Danger tiles
            TileType.DangerTile => false,

            //PushTiles
            TileType.PushTile => false,

            //Text points
            TileType.TextPoint => false,

            //Nodes
            TileType.IONode => true,
            TileType.SuperNode => true,
            TileType.Checkpoint => false,

            _ => false,
        };
    }

    public virtual SceneTile Clone()
    {
        return new SceneTile();
    }
}

public class Wall : SceneTile
{
    public Wall(TileType C_Type, TileMesh C_MeshType, float C_PosX, float C_PosY, float C_PosZ, int C_ScaleX = 1, int C_ScaleY = 1, int C_ScaleZ = 1)
    {
        Breakable = true;
        Type = C_Type;
        MeshType = C_MeshType;
        Position = new Vector3(C_PosX, C_PosY, C_PosZ);
        Scale = new Vector3Int(C_ScaleX, C_ScaleY, C_ScaleZ);
    }

    public override SceneTile Clone()
    {
        return new Wall(Type, MeshType, Position.x, Position.y, Position.z, Scale.x, Scale.y, Scale.z);
    }
}

public class Piston : SceneTile
{
    public Piston(TileType C_Type, TileMesh C_MeshType, float C_PosX, float C_PosY, float C_PosZ, int C_ScaleX = 1, int C_ScaleY = 1, int C_ScaleZ = 1)
    {
        Breakable = false;
        Type = C_Type;
        MeshType = C_MeshType;
        Position = new Vector3(C_PosX, C_PosY, C_PosZ);
        Scale = new Vector3Int(C_ScaleX, C_ScaleY, C_ScaleZ);
    }

    public override SceneTile Clone()
    {
        return new Piston(Type, MeshType, Position.x, Position.y, Position.z, Scale.x, Scale.y, Scale.z);
    }
}

public class Collectable : SceneTile
{
    public bool Collected; //Internal
    public CollectableType CollectableType;

    public Collectable(CollectableType C_CollectableType, float C_PosX, float C_PosY, float C_PosZ)
    {
        CollectableType = C_CollectableType;

        Type = TileType.Collectable;
        Position = new Vector3(C_PosX, C_PosY, C_PosZ);
    }

    public override SceneTile Clone()
    {
        return new Collectable(CollectableType, Position.x, Position.y, Position.z);
    }
}

public class DangerTile : SceneTile
{
    public List<float> TimesBetween;

    public DangerTile(List<float> C_TimesBetween, float C_PosX, float C_PosY, float C_PosZ)
    {
        TimesBetween = C_TimesBetween;

        Type = TileType.DangerTile;
        Position = new Vector3(C_PosX, C_PosY, C_PosZ);
    }

    public override SceneTile Clone()
    {
        return new DangerTile(TimesBetween, Position.x, Position.y, Position.z);
    }
}

public class PushTile : SceneTile
{
    public float StartTime = 0;

    public List<float> TimesBetween;
    public List<Vector3Int> Directions;

    public PushTile(List<float> C_TimesBetween, List<Vector3Int> C_Directions, float C_PosX, float C_PosY, float C_PosZ)
    {
        TimesBetween = C_TimesBetween;
        Directions = C_Directions;

        Type = TileType.PushTile;
        Position = new Vector3(C_PosX, C_PosY, C_PosZ);

        StartTime = Time.fixedUnscaledTime;
    }

    public List<Vector3Int> Rotations()
    {
        List<Vector3Int> Output = new List<Vector3Int>();

        foreach (Vector3Int Direction in Directions)
        {
            Output.Add((Direction.x, Direction.z) switch
            {
                (0, -1) => Vector3Int.zero,
                (-1, 0) => Vector3Int.up * 90,
                (0, 1) => Vector3Int.up * 180,
                (1, 0) => Vector3Int.up * 270,
                _ => Vector3Int.zero,
            });
        }

        return Output;
    }

    public override SceneTile Clone()
    {
        return new PushTile(TimesBetween, Directions, Position.x, Position.y, Position.z);
    }
}

public class TextPoint : SceneTile
{
    public string Text;
    public TextImage TextImage;
    public bool Repeatable;

    public TextPoint(string C_Text, TextImage C_TextImage, bool C_Repeatable, float C_PosX, float C_PosY, float C_PosZ, int C_ScaleX = 1, int C_ScaleY = 1, int C_ScaleZ = 1)
    {
        Breakable = false;
        Text = C_Text;
        TextImage = C_TextImage;
        Repeatable = C_Repeatable;

        Type = TileType.TextPoint;
        Position = new Vector3(C_PosX, C_PosY, C_PosZ);
        Scale = new Vector3Int(C_ScaleX, C_ScaleY, C_ScaleZ);
    }

    public override SceneTile Clone()
    {
        return new TextPoint(Text, TextImage, Repeatable, Position.x, Position.y, Position.z, Scale.x, Scale.y, Scale.z);
    }
}

public class IONode : SceneTile
{
    public int LevelTo;
    public Vector3Int PositionTo;

    public IONode(int C_LevelTo, int C_PosToX, int C_PosToY, int C_PosToZ, TileMesh C_MeshType, float C_PosX, float C_PosY, float C_PosZ, int C_ScaleX = 1, int C_ScaleY = 1, int C_ScaleZ = 1)
    {
        Breakable = false;
        LevelTo = C_LevelTo;
        PositionTo = new Vector3Int(C_PosToX, C_PosToY, C_PosToZ);

        Type = TileType.IONode;
        MeshType = C_MeshType;
        Position = new Vector3(C_PosX, C_PosY, C_PosZ);
        Scale = new Vector3Int(C_ScaleX, C_ScaleY, C_ScaleZ);
    }

    public override SceneTile Clone()
    {
        return new IONode(LevelTo, PositionTo.x, PositionTo.y, PositionTo.z, MeshType, Position.x, Position.y, Position.z, Scale.x, Scale.y, Scale.z);
    }
}

public class SuperNode : SceneTile
{
    public bool Active; //Internal

    public SuperNode(TileMesh C_MeshType, float C_PosX, float C_PosY, float C_PosZ, int C_ScaleX = 1, int C_ScaleY = 1, int C_ScaleZ = 1)
    {
        Breakable = false;
        Type = TileType.SuperNode;
        MeshType = C_MeshType;
        Position = new Vector3(C_PosX, C_PosY, C_PosZ);
        Scale = new Vector3Int(C_ScaleX, C_ScaleY, C_ScaleZ);
    }

    public override SceneTile Clone()
    {
        return new SuperNode(MeshType, Position.x, Position.y, Position.z, Scale.x, Scale.y, Scale.z);
    }
}

public class Checkpoint : SceneTile
{
    public bool Collected; //Internal

    public Checkpoint(float C_PosX, float C_PosY, float C_PosZ, int C_ScaleX = 1, int C_ScaleY = 1, int C_ScaleZ = 1)
    {
        Breakable = false;
        Type = TileType.Checkpoint;
        Position = new Vector3(C_PosX, C_PosY, C_PosZ);
        Scale = new Vector3Int(C_ScaleX, C_ScaleY, C_ScaleZ);
    }

    public override SceneTile Clone()
    {
        return new Checkpoint(Position.x, Position.y, Position.z, Scale.x, Scale.y, Scale.z);
    }
}

public class RenderType : SceneTile
{
    public RenderingType RenderingType;

    public RenderType(RenderingType C_RenderingType, TileMesh C_MeshType, float C_PosX, float C_PosY, float C_PosZ, int C_ScaleX = 1, int C_ScaleY = 1, int C_ScaleZ = 1)
    {
        Breakable = false;
        RenderingType = C_RenderingType;

        Type = TileType.RenderingFeature;
        MeshType = C_MeshType;
        Position = new Vector3(C_PosX, C_PosY, C_PosZ);
        Scale = new Vector3Int(C_ScaleX, C_ScaleY, C_ScaleZ);
    }

    public override SceneTile Clone()
    {
        return new RenderType(RenderingType, MeshType, Position.x, Position.y, Position.z, Scale.x, Scale.y, Scale.z);
    }
}


public enum TileType
{
    Wall,
    Scaffold,
    Null,
    PushPiston,
    PivotPiston,
    Collectable,
    DangerTile,
    PushTile,
    TextPoint,
    IONode,
    SuperNode,
    Checkpoint,
    RenderingFeature,
}


public enum TileMesh
{
    Blank,

    Wall1,
    Wall2,
    Wall3,
    Wall4,
    Wall5,
    Wall6,
    Wall7,
    Wall8,
    Wall9,
    Wall10,
    Wall11,
    Wall12,
    Wall13,
    Wall14,
    Wall15,
    Wall16,

    Collectable1,
    Collectable2,
    Collectable3,
    Collectable4,
    Collectable5,
    Collectable6,
    Collectable7,
    Collectable8,

    Piston1,
    Piston2,
    Piston3,
    Piston4,

    IONode1,
    IONode2,
    IONode3,
    IONode4,
}


public enum Direction
{
    XPlus,
    XMinus,
    ZPlus,
    ZMinus,
    YPlus,
    YMinus,
}


public enum TextImage
{
    Dice,
    DiceHappy,
    DiceSad,
    DiceAnnoyed,
    DiceFlatFace,
    DiceSurprised,

    Wind,
    WindHappy,
    WindSad,
    WindAnnoyed,
    WindFlatFace,
    WindSurprised,
}


public enum RenderingType
{
    Crushable,
    Windy,
    ReallyWindy,
    LightningBolt,
}


public enum CollectableType
{
    CardStatue,
}


public enum BackgroundType
{
    Boxy,
    Sky,
}


public static class LevelCatalogue
{
    public static LevelSave GetLevelAtIndex(int C_Index)
    {
        return C_Index switch
        {
            //Tutorial level
            0 => new LevelSave(
                BackgroundType.Boxy,
                1,
                new List<SceneTile>
                {
                    new Wall(TileType.Wall, TileMesh.Wall2, -1, -1, 0, 3, 1, 1),

                    new Wall(TileType.Wall, TileMesh.Wall3, -3, 0, 0),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, 0, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, 0, -2),

                    new Wall(TileType.Wall, TileMesh.Wall3, -3, 0, -3),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -4, 0, -3),

                    new IONode(1, 0, 0, 0, TileMesh.IONode1, -5, 0, -3),
                }),

            1 => new LevelSave(
                BackgroundType.Boxy,
                1,
                new List<SceneTile>
                {
                    new Wall(TileType.Wall, TileMesh.Wall2, -1, -1, 0, 3, 1, 1),

                    new Wall(TileType.Wall, TileMesh.Wall3, 0, 0, -1),

                    new Wall(TileType.Wall, TileMesh.Wall3, -2, 0, -1),

                    new Wall(TileType.Wall, TileMesh.Wall3, -4, 0, -1),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -4, 0, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -4, 0, 1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -4, 0, 2),

                    new Wall(TileType.Wall, TileMesh.Wall2, -4, 0, 3),
                    new IONode(2, 0, 0, 0, TileMesh.IONode1, -5, 0, 3),
                }),

            2 => new LevelSave(
                BackgroundType.Boxy,
                1,
                new List<SceneTile>
                {
                    new Wall(TileType.Wall, TileMesh.Wall2, 0, -1, 0, 3, 1, 1),
                    new Wall(TileType.Wall, TileMesh.Wall3, -1, 0, -1),
                    new Wall(TileType.Wall, TileMesh.Wall3, -2, 0, 0),

                    new Wall(TileType.Wall, TileMesh.Wall3, -4, 0, -1),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, 0, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, 0, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, 0, -2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, 0, -3),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, 0, -4),

                    new Wall(TileType.Wall, TileMesh.Wall3, -3, 1, -5, 1, 3, 1),
                    new Wall(TileType.Wall, TileMesh.Wall2, -2, 0, -5),
                    new IONode(3, 0, 0, 0, TileMesh.IONode1, -2, 0, -6),
                }),

            3 => new LevelSave(
                BackgroundType.Boxy,
                1,
                new List<SceneTile>
                {
                    new Wall(TileType.Wall, TileMesh.Wall2, -1, -1, -1),
                    new Wall(TileType.Wall, TileMesh.Wall2, 1, -1, -1),
                    new Wall(TileType.Wall, TileMesh.Wall2, 0, -1, 0, 3, 1, 1),

                    new Wall(TileType.Wall, TileMesh.Wall2, -1, -1, -3),

                    new Wall(TileType.Wall, TileMesh.Wall2, -1, -1, -5),

                    new Wall(TileType.Wall, TileMesh.Wall2, -4, -1, -1),

                    new Wall(TileType.Wall, TileMesh.Wall2, -3, -1, 0),

                    new Wall(TileType.Wall, TileMesh.Wall2, -4, -1, -3),
                    new Wall(TileType.Wall, TileMesh.Wall3, -5, 0, -3, 1, 3, 1),

                    new IONode(4, 0, 0, 0, TileMesh.IONode1, -4, -1, -5),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -1, -1, -4),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -1, -1, -2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, -1, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, -1, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -4, -1, -4),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -4, -1, -2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, -1, -3),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, -1, -3),
                }),

            4 => new LevelSave(
                BackgroundType.Boxy,
                1,
                new List<SceneTile>
                {
                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, 0, -1, 1),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, 0, -1, 2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 0, -1, 3),

                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, 4),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -1, -1, 4),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, -1, 4),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, -1, 4),

                    new IONode(5, 0, 0, 0, TileMesh.IONode1, -4, -1, 4),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -4, -1, 3),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -4, -1, 2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -4, -1, 1),

                    new Wall(TileType.Wall, TileMesh.Wall3, -4, -1, 0),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, -1, 0),

                    new Wall(TileType.Wall, TileMesh.Wall2, -1, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, 1, -1, 0),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, 2, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 3, -1, 0),

                    new Wall(TileType.Wall, TileMesh.Wall3, 4, -1, 0),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, 4, -1, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 4, -1, -2),

                    new Wall(TileType.Wall, TileMesh.Wall3, 4, -1, -3),
                    new Wall(TileType.Wall, TileMesh.Wall3, 3, -1, -4),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, 2, -1, -4),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 1, -1, -4),

                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, -4),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, 0, -1, -3),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 0, -1, -2),

                    new Wall(TileType.Wall, TileMesh.Wall2, 0, -1, -1),
                }),

            5 => new LevelSave(
                BackgroundType.Boxy,
                1,
                new List<SceneTile>
                {
                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall1, 0, -2, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, 0, -2, 1),
                    new Wall(TileType.Wall, TileMesh.Wall2, 0, 0, -1),
                    new Wall(TileType.Wall, TileMesh.Wall2, 0, 0, -2),
                    new Wall(TileType.Wall, TileMesh.Wall2, -1, 0, 0),

                    new Wall(TileType.Wall, TileMesh.Wall4, -7, 0, -3),
                    new Wall(TileType.Wall, TileMesh.Wall2, -6, 0, -3),
                    new Wall(TileType.Wall, TileMesh.Wall2, -5, 0, -3),

                    new Wall(TileType.Wall, TileMesh.Wall2, -8, 1, -3, 3, 1, 1),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, 0, 0),

                    new Wall(TileType.Wall, TileMesh.Wall3, -3, 0, 1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, 0, 0),
                    new Wall(TileType.Wall, TileMesh.Wall3, -4, 0, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -4, 0, 1),
                    new Wall(TileType.Wall, TileMesh.Wall3, -5, 0, 1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -5, 0, 0),
                    new Wall(TileType.Wall, TileMesh.Wall3, -6, 0, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -6, 0, 1),

                    new IONode(6, 0, 0, 0, TileMesh.IONode1, -3, 0, 2),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -5, 0, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -5, 0, -2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -6, 0, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -6, 0, -2),
                }),

            6 => new LevelSave(
                BackgroundType.Boxy,
                1,
                new List<SceneTile>
                {
                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, 0),
                    new IONode(7, 0, 0, 0, TileMesh.IONode1, 0, -1, -2),

                    new Wall(TileType.Wall, TileMesh.Wall2, 0, 0, 1),
                    new Wall(TileType.Wall, TileMesh.Wall3, 1, 0, 1),
                    new Wall(TileType.Wall, TileMesh.Wall2, 1, 0, 0),

                    new Wall(TileType.Wall, TileMesh.Wall2, 0, 1, 2),
                    new Wall(TileType.Wall, TileMesh.Wall2, 1, 1, 2),
                    new Wall(TileType.Wall, TileMesh.Wall3, 2, 1, 2),
                    new Wall(TileType.Wall, TileMesh.Wall2, 2, 1, 1),
                    new Wall(TileType.Wall, TileMesh.Wall2, 2, 1, 0),

                    new Wall(TileType.Wall, TileMesh.Wall2, 3, 2, 1, 1, 1, 3),
                    new Wall(TileType.Wall, TileMesh.Wall3, 3, 2, 3),
                    new Wall(TileType.Wall, TileMesh.Wall2, 1, 2, 3, 3, 1, 1),

                    new Wall(TileType.Wall, TileMesh.Wall2, -4, 0, 1, 3, 1, 1),
                    new Wall(TileType.Wall, TileMesh.Wall2, -4, 1, 2, 3, 1, 1),
                    new Wall(TileType.Wall, TileMesh.Wall2, -4, 2, 3, 3, 1, 1),

                    new Wall(TileType.Wall, TileMesh.Wall2, -4, 0, 1, 3, 1, 1),
                    new Wall(TileType.Wall, TileMesh.Wall2, -4, 1, 2, 3, 1, 1),
                    new Wall(TileType.Wall, TileMesh.Wall2, -4, 2, 3, 3, 1, 1),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, 0, 1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -1, 0, 1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, 1, 2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -1, 1, 2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, 2, 3),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -1, 2, 3),
                }),

            7 => new LevelSave(
                BackgroundType.Boxy,
                1,
                new List<SceneTile>
                {
                    new Wall(TileType.Wall, TileMesh.Wall2, -2, 0, 0),

                    new Wall(TileType.Wall, TileMesh.Wall4, -1, -2, 0),
                    new Wall(TileType.Wall, TileMesh.Wall4, -1, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, -1, 0, 0),

                    new Wall(TileType.Wall, TileMesh.Wall4, 0, -2, 0),

                    new Wall(TileType.Wall, TileMesh.Wall2, 1, -2, 0),

                    new Wall(TileType.Wall, TileMesh.Wall4, 2, -2, 0),
                    new Wall(TileType.Wall, TileMesh.Wall4, 2, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, 2, 0, 0),

                    new Wall(TileType.Wall, TileMesh.Wall2, 4, -2, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, 3, 0, 0),

                    new Wall(TileType.Wall, TileMesh.Wall4, 5, -2, 0),
                    new Wall(TileType.Wall, TileMesh.Wall4, 5, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, 5, 0, 0),

                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, 0),
                    new IONode(8, 0, 0, 0, TileMesh.IONode1, 5, -2, 1),
                }),

            8 => new LevelSave(
                BackgroundType.Boxy,
                2,
                new List<SceneTile>
                {
                    new Wall(TileType.Wall, TileMesh.Wall2, 6, -2, 1),
                    new Wall(TileType.Wall, TileMesh.Wall2, 6, -2, 2),
                    new Wall(TileType.Wall, TileMesh.Wall2, 5, -2, 2, 1, 1, 3),

                    new Wall(TileType.Wall, TileMesh.Wall2, 4, -1, 5),
                    new Wall(TileType.Wall, TileMesh.Wall1, 5, -1, 4),
                    new Wall(TileType.Wall, TileMesh.Wall1, 6, -1, 3),

                    new Wall(TileType.Wall, TileMesh.Wall3, 6, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall3, 4, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall3, 2, -1, 0),

                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, -3),
                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, -6),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, 0, -1, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 0, -1, -2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 0, -1, -4),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 0, -1, -5),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, 1, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 3, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 5, -1, 0),

                    new Wall(TileType.Wall, TileMesh.Wall3, 4, 0, 3, 1, 3, 1),

                    new Wall(TileType.Wall, TileMesh.Wall3, 6, 0, 5),
                    new Wall(TileType.Wall, TileMesh.Wall3, 5, 0, 4),
                    new Wall(TileType.Wall, TileMesh.Wall3, 6, 0, 3),
                    new Wall(TileType.Wall, TileMesh.Wall3, 7, 0, 0),
                    new Wall(TileType.Wall, TileMesh.Wall3, -1, 0, 0, 1, 3, 1),

                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, 0),
                    new IONode(9, 0, 0, 0, TileMesh.IONode1, 5, -1, 5),
                }),

            9 => new LevelSave(
                BackgroundType.Boxy,
                2,
                new List<SceneTile>
                {
                    new Wall(TileType.Wall, TileMesh.Wall3, -4, 0, 0, 1, 3, 1),

                    new Piston(TileType.PushPiston, TileMesh.Piston1, -3, -1, 0),

                    new Wall(TileType.Wall, TileMesh.Wall2, -1, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, 1, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall1, -3, -1, 5),
                    new Wall(TileType.Wall, TileMesh.Wall3, -1, -1, 5),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, -1, 5),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -1, -1, 4),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -1, -1, 3),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -1, -1, 2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -1, -1, 1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 3, -1, 2),

                    new Wall(TileType.Wall, TileMesh.Wall4, 3, -1, -1),
                    new Wall(TileType.Wall, TileMesh.Wall2, 3, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, 3, -1, 1),
                    new Wall(TileType.Wall, TileMesh.Wall2, 3, -1, 4, 1, 1, 3),

                    new Wall(TileType.Wall, TileMesh.Wall2, 2, 0, 0),
                    new Wall(TileType.Wall, TileMesh.Wall4, 2, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, 3, 0, -1),

                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, 0),
                    new IONode(10, 0, 0, 0, TileMesh.IONode1, -3, 0, 5),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, -1, 4),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, -1, 3),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, -1, 2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, -1, 1),
                }),

            10 => new LevelSave(
                BackgroundType.Boxy,
                2,
                new List<SceneTile>
                {
                    new Wall(TileType.Wall, TileMesh.Wall2, 3, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, 2, -1, 0),
                    new Piston(TileType.PushPiston, TileMesh.Piston1, 1, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, -3, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, -1, -1, 0),

                    new Wall(TileType.Wall, TileMesh.Wall1, 1, -1, -2),
                    new Wall(TileType.Wall, TileMesh.Wall2, 0, -1, -2),
                    new Wall(TileType.Wall, TileMesh.Wall1, 0, -1, -3),

                    new Wall(TileType.Wall, TileMesh.Wall4, 1, -1, 1),
                    new Wall(TileType.Wall, TileMesh.Wall2, 1, 0, 2),
                    new Wall(TileType.Wall, TileMesh.Wall2, 1, 0, 1),

                    new Wall(TileType.Wall, TileMesh.Wall3, 1, 0, -2),

                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, 0),
                    new IONode(11, 0, 0, 0, TileMesh.IONode1, 0, 0, -3),

                    new Wall(TileType.Wall, TileMesh.Wall1, -3, -1, 1),
                    new Wall(TileType.Wall, TileMesh.Wall1, -3, 0, 1),
                    new Wall(TileType.Wall, TileMesh.Wall1, -3, 1, 1),
                    new Wall(TileType.Wall, TileMesh.Wall3, -3, 2, 1),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 0, -1, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 1, -1, -1),
                }),

            11 => new LevelSave(
                BackgroundType.Boxy,
                2,
                new List<SceneTile>
                {
                    new IONode(12, 0, 0, 0, TileMesh.IONode1, 3, -1, 8),

                    new Wall(TileType.Wall, TileMesh.Wall2, 3, -1, 5),
                    new Wall(TileType.Wall, TileMesh.Wall2, 4, -1, 3),
                    new Wall(TileType.Wall, TileMesh.Wall2, 3, -1, 3),
                    new Wall(TileType.Wall, TileMesh.Wall2, 2, -1, 4),
                    new Wall(TileType.Wall, TileMesh.Wall2, 1, -1, 5),

                    new Piston(TileType.PivotPiston, TileMesh.Piston2, 1, -1, 0),
                    new Piston(TileType.PivotPiston, TileMesh.Piston2, 2, -1, 0),
                    new Piston(TileType.PivotPiston, TileMesh.Piston2, 3, -1, 0),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, 1, -1, 1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 1, -1, 2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 1, -1, 3),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 1, -1, 4),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, 2, -1, 1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 2, -1, 2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 2, -1, 3),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 2, -1, 5),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, 3, -1, 1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 3, -1, 2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 3, -1, 4),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 3, -1, 6),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 3, -1, 7),

                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, 0),
                }),

            12 => new LevelSave(
                BackgroundType.Boxy,
                2,
                new List<SceneTile>
                {
                    new Wall(TileType.Wall, TileMesh.Wall2, 1, -1, 0),
                    new Piston(TileType.PivotPiston, TileMesh.Piston2, 2, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall3, 3, 0, 0, 1, 3, 1),

                    new Wall(TileType.Wall, TileMesh.Wall2, 0, -1, 1),
                    new Piston(TileType.PivotPiston, TileMesh.Piston2, 0, -1, 2),
                    new Wall(TileType.Wall, TileMesh.Wall3, 0, 0, 3, 1, 3, 1),

                    new Wall(TileType.Wall, TileMesh.Wall2, -1, -1, 0),
                    new Piston(TileType.PivotPiston, TileMesh.Piston2, -2, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall3, -3, 0, 0, 1, 3, 1),

                    new Wall(TileType.Wall, TileMesh.Wall2, 0, -1, -1),
                    new Piston(TileType.PivotPiston, TileMesh.Piston2, 0, -1, -2),
                    new Wall(TileType.Wall, TileMesh.Wall3, 0, 0, -3, 1, 3, 1),

                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, 0),
                    new IONode(13, 0, 0, 0, TileMesh.IONode1, 0, -1, -4),
                }),

            13 => new LevelSave(
                BackgroundType.Boxy,
                2,
                new List<SceneTile>
                {
                    new IONode(14, 0, 0, 0, TileMesh.IONode1, 1, -1, 5),
                    new Piston(TileType.PushPiston, TileMesh.Piston1, 2, -1, 2),
                    new Wall(TileType.Wall, TileMesh.Wall2, 1, -1, 2),
                    new Wall(TileType.Wall, TileMesh.Wall1, 2, -1, 0),
                    new Piston(TileType.PushPiston, TileMesh.Piston1, 1, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall1, -1, -1, 2),
                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall1, -1, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, -2, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall3, 3, 0, 2),
                    new Wall(TileType.Wall, TileMesh.Wall1, 3, -1, 2),
                    new Piston(TileType.PivotPiston, TileMesh.Piston2, 2, 0, 0),
                    new Wall(TileType.Wall, TileMesh.Wall3, -1, 0, 2),
                    new Wall(TileType.Wall, TileMesh.Wall3, -1, 0, 0),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, 1, -1, 1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 1, -1, 3),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 1, -1, 4),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 2, -1, 1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -1, 0, 1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 0, -1, 2),
                }),

            14 => new LevelSave(
                BackgroundType.Boxy,
                2,
                new List<SceneTile>
                {
                    new Piston(TileType.PivotPiston, TileMesh.Piston2, -6, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall1, -4, -1, -1),
                    new Wall(TileType.Wall, TileMesh.Wall1, -6, -1, -1),
                    new Wall(TileType.Wall, TileMesh.Wall1, -5, -1, -1),
                    new Wall(TileType.Wall, TileMesh.Wall2, -5, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, -4, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall1, -2, -1, -1),
                    new Wall(TileType.Wall, TileMesh.Wall1, -1, -1, -1),
                    new Wall(TileType.Wall, TileMesh.Wall2, -1, -1, 0),


                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, 0),

                    new Wall(TileType.Wall, TileMesh.Wall1, -6, 0, -1),
                    new Wall(TileType.Wall, TileMesh.Wall2, -5, 0, -1),
                    new Wall(TileType.Wall, TileMesh.Wall1, -4, 0, -1),
                    new Wall(TileType.Wall, TileMesh.Wall1, -2, 0, -1),
                    new Wall(TileType.Wall, TileMesh.Wall2, -1, 0, -1),

                    new IONode(15, 0, 0, 0, TileMesh.IONode1, -7, 1, -1),
                    new Wall(TileType.Wall, TileMesh.Wall1, -7, 0, -1),
                    new Wall(TileType.Wall, TileMesh.Wall1, -7, -1, -1),

                    new Wall(TileType.Wall, TileMesh.Wall3, -6, 1, -1),
                    new Wall(TileType.Wall, TileMesh.Wall3, -4, 1, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, 1, -1),
                    new Wall(TileType.Wall, TileMesh.Wall3, -2, 1, -1),
                }),

            15 => new LevelSave(
                BackgroundType.Boxy,
                2,
                new List<SceneTile>
                {
                    new Wall(TileType.Wall, TileMesh.Wall3, -8, -1, -2),
                    new Wall(TileType.Wall, TileMesh.Wall2, -8, -1, 1),
                    new Wall(TileType.Wall, TileMesh.Wall2, -4, -1, -2),
                    new Wall(TileType.Wall, TileMesh.Wall2, -8, -1, 2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -8, -1, -1),
                    new Wall(TileType.Wall, TileMesh.Wall2, -3, -1, -2),
                    new Wall(TileType.Wall, TileMesh.Wall2, -2, -1, -2),
                    new Wall(TileType.Wall, TileMesh.Wall2, -4, -1, 2),
                    new Wall(TileType.Wall, TileMesh.Wall2, -3, -1, 2),
                    new Wall(TileType.Wall, TileMesh.Wall2, -2, -1, 2),

                    new Wall(TileType.Wall, TileMesh.Wall3, 2, -1, 0),
                    new Piston(TileType.PivotPiston, TileMesh.Piston2, -1, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, -2, -1, 0),

                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, 0),
                    new IONode(16, 0, 0, 0, TileMesh.IONode1, -8, -1, 0),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, 1, -1, 0),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, -1, 1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, -1, -1),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, -1, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -3, -1, 1),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -4, -1, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -4, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -4, -1, 1),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -5, -1, -2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -5, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -5, -1, 2),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -6, -1, -2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -6, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -6, -1, 2),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -7, -1, -2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -7, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -7, -1, 2),

                }),

            16 => new LevelSave(
                BackgroundType.Boxy,
                2,
                new List<SceneTile>
                {
                    new Wall(TileType.Wall, TileMesh.Wall1, 3, -1, 0),
                    new Piston(TileType.PivotPiston, TileMesh.Piston2, 1, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall1, 3, -1, -2),
                    new Wall(TileType.Wall, TileMesh.Wall3, 0, -1, 0),
                    new Piston(TileType.PivotPiston, TileMesh.Piston2, 0, -1, -1),
                    new Wall(TileType.Wall, TileMesh.Wall1, 3, -1, -5),
                    new Wall(TileType.Wall, TileMesh.Wall1, -2, -1, 0),
                    new Wall(TileType.Wall, TileMesh.Wall2, -2, -1, -1),
                    new Wall(TileType.Wall, TileMesh.Wall2, -2, -1, -2),

                    new IONode(-1, 0, 0, 0, TileMesh.IONode1, -2, -1, -5),

                    new Wall(TileType.Wall, TileMesh.Wall3, 3, 0, 0),
                    new Wall(TileType.Wall, TileMesh.Wall3, 3, 0, -2),
                    new Wall(TileType.Wall, TileMesh.Wall3, 3, 0, -5),
                    new Wall(TileType.Wall, TileMesh.Wall1, -2, 0, 0),
                    new Wall(TileType.Wall, TileMesh.Wall3, -2, 1, 0),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, 2, -1, 0),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -1, -1, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 3, 0, -1),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 3, 0, -3),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 3, 0, -4),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, -1, -3),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, -2, -1, -4),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -1, -1, -2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 0, -1, -2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 1, -1, -2),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 2, -1, -2),

                    new Wall(TileType.Scaffold, TileMesh.Wall5, -1, -1, -5),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 0, -1, -5),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 1, -1, -5),
                    new Wall(TileType.Scaffold, TileMesh.Wall5, 2, -1, -5),
                }),


            _ => new LevelSave(BackgroundType.Boxy, 0, new List<SceneTile>()),
        };
    }
}
