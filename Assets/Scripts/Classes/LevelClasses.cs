using System.Collections.Generic;
using UnityEngine;


public class LevelSave
{
    public List<SceneTile> Scenetiles;
    public List<Collectable> Collectables;
    public List<IONode> Nodes;
    public List<TextPoint> TextPoints;
    public List<DangerTile> DangerTiles;

    public Dictionary<Vector3Int, SceneTile> TileAtPosition = new Dictionary<Vector3Int, SceneTile>();
    public Dictionary<Vector3Int, Collectable> CollectableAtPosition = new Dictionary<Vector3Int, Collectable>();
    public Dictionary<Vector3Int, IONode> NodeAtPosition = new Dictionary<Vector3Int, IONode>();
    public Dictionary<Vector3Int, TextPoint> TextPointAtPosition = new Dictionary<Vector3Int, TextPoint>();
    public Dictionary<Vector3Int, DangerTile> DangerTileAtPosition = new Dictionary<Vector3Int, DangerTile>();

    public void GenerateAtPositionList()
    {
        TileAtPosition = new Dictionary<Vector3Int, SceneTile>();
        CollectableAtPosition = new Dictionary<Vector3Int, Collectable>();
        NodeAtPosition = new Dictionary<Vector3Int, IONode>();
        TextPointAtPosition = new Dictionary<Vector3Int, TextPoint>();
        DangerTileAtPosition = new Dictionary<Vector3Int, DangerTile>();

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

        foreach (Collectable CollectableData in Collectables)
        {
            CollectableAtPosition.Add(Vector3Int.RoundToInt(CollectableData.Position), CollectableData);
        }

        foreach (IONode NodeData in Nodes)
        {
            Vector3 StartPosition = NodeData.Position - new Vector3(NodeData.Scale.x / 2f, NodeData.Scale.y / 2f, NodeData.Scale.z / 2f);
            Vector3 EndPosition = NodeData.Position + new Vector3(NodeData.Scale.x / 2f, NodeData.Scale.y / 2f, NodeData.Scale.z / 2f);

            for (float x = StartPosition.x; x < EndPosition.x; x++)
            {
                for (float y = StartPosition.y; y < EndPosition.y; y++)
                {
                    for (float z = StartPosition.z; z < EndPosition.z; z++)
                    {
                        NodeAtPosition.Add(Vector3Int.RoundToInt(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f)), NodeData);
                    }
                }
            }
        }

        foreach (TextPoint TextData in TextPoints)
        {
            TextPointAtPosition.Add(Vector3Int.RoundToInt(TextData.Position), TextData);
        }
    }


    public SceneTile TileAt(Vector3Int C_Position)
    {
        if (TileAtPosition.ContainsKey(C_Position))
        {
            return TileAtPosition[C_Position];
        }
        return null;
    }


    public Collectable CollectableAt(Vector3Int C_Position)
    {
        if (CollectableAtPosition.ContainsKey(C_Position))
        {
            return CollectableAtPosition[C_Position];
        }
        return null;
    }


    public IONode NodeAt(Vector3Int C_Position)
    {
        if (NodeAtPosition.ContainsKey(C_Position))
        {
            return NodeAtPosition[C_Position];
        }
        return null;
    }


    public TextPoint TextAt(Vector3Int C_Position)
    {
        if (TextPointAtPosition.ContainsKey(C_Position))
        {
            return TextPointAtPosition[C_Position];
        }
        return null;
    }


    public LevelSave(List<SceneTile> C_Scenetiles = null, List<Collectable> C_Collectables = null, List<IONode> C_Nodes = null, List<TextPoint> C_Texts = null)
    {
        Scenetiles = C_Scenetiles ?? new List<SceneTile>();
        Collectables = C_Collectables ?? new List<Collectable>();
        Nodes = C_Nodes ?? new List<IONode>();
        TextPoints = C_Texts ?? new List<TextPoint>();
    }
}


public class SceneTile
{
    public TileType Type;
    public Vector3 Position;
    public Vector3Int Scale;

    public bool Solid => !(Type == TileType.Null || Type == TileType.Scaffold); //False

    public SceneTile(TileType C_Type, float C_XPos, float C_YPos, float C_ZPos, int C_XScale = 1, int C_YScale = 1, int C_ZScale = 1)
    {
        Type = C_Type;
        Position = new Vector3(C_XPos, C_YPos, C_ZPos);
        Scale = new Vector3Int(C_XScale, C_YScale, C_ZScale);
    }
}


public class Collectable
{
    public Vector3 Position;
    public CollectableType Type;
    public bool Collected;
}


public class IONode
{
    public int LevelTo;
    public Vector3 PositionTo;

    public Vector3 Position;
    public Vector3Int Scale;

    public IONode(int C_LevelTo, int C_XPosTo, int C_YPosTo, int C_ZPosTo, float C_XPos, float C_YPos, float C_ZPos, int C_XScale = 1, int C_YScale = 1, int C_ZScale = 1)
    {
        LevelTo = C_LevelTo;
        PositionTo = new Vector3(C_XPosTo, C_YPosTo, C_ZPosTo);
        Position = new Vector3(C_XPos, C_YPos, C_ZPos);
        Scale = new Vector3Int(C_XScale, C_YScale, C_ZScale);
    }
}


public class TextPoint
{
    public Vector3 Position;
    public Vector3Int Scale;
    public string Text;

    public TextPoint(string C_Text, int C_XPos, int C_YPos, int C_ZPos, int C_XScale = 1, int C_YScale = 1, int C_ZScale = 1)
    {
        Text = C_Text;
        Position = new Vector3(C_XPos, C_YPos, C_ZPos);
        Scale = new Vector3Int(C_XScale, C_YScale, C_ZScale);
    }
}


public class DangerTile
{
    public Vector3 Position;
    public Vector3Int Scale;
    public List<float> TimesBetween;

    public bool Dangerous;
}


public enum TileType
{
    Wall,
    Scaffold,
    Null,
    PushPiston,
    PivotPiston,
}


public enum CollectableType
{

}


public enum BackgroundType
{
    Boxy,
    Clouds,

}


public static class LevelCatalogue
{
    //Start level
    public static LevelSave LevelZero = new LevelSave(
        new List<SceneTile>
        {
            new SceneTile(TileType.Wall, 0, -1, 0, 9, 1, 9),
        },
        new List<Collectable>
        {

        },
        new List<IONode>
        {
            new IONode(1, 0, 0, 0, 0, -1, 6, 3, 1, 3),
        },
        new List<TextPoint>
        {
            new TextPoint("To slide, use the arrow keys, the board around the dice points up, left and right, with each corresponding to the direction the key will move the dice in", 0, 0, 0),

        });

    //Level one
    public static LevelSave LevelOne = new LevelSave(
        new List<SceneTile>
        {
            new SceneTile(TileType.Wall, 0, -1, 0, 5, 1, 5),
            new SceneTile(TileType.Wall, 0, 0, 4, 3, 1, 3),
        },
        new List<Collectable>
        {

        },
        new List<IONode>
        {
            new IONode(1, 0, 0, 0, 0, -1, -3),
        },
        new List<TextPoint>
        {

        });

    public static LevelSave GetLevelAtIndex(int C_Index)
    {
        return C_Index switch
        {
            0 => LevelZero,
            1 => LevelOne,


            _ => new LevelSave(new List<SceneTile>(), new List<Collectable>()),
        };
    }
}
