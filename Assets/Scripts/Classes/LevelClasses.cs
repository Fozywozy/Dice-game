using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class LevelSave
{
    public List<SceneTile> Scenetiles;
    public List<Collectable> Collectables;
    public List<IONode> Nodes;

    [System.NonSerialized]
    public Dictionary<Vector3, SceneTile> TileAtPosition = new Dictionary<Vector3, SceneTile>();
    [System.NonSerialized]
    public Dictionary<Vector3, Collectable> CollectableAtPosition = new Dictionary<Vector3, Collectable>();
    [System.NonSerialized]
    public Dictionary<Vector3, IONode> NodeAtPosition = new Dictionary<Vector3, IONode>();

    public void GenerateAtPositionList()
    {
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
                        TileAtPosition.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), TileData);
                    }
                }
            }
        }


        foreach (Collectable CollectableData in Collectables)
        {
            CollectableAtPosition.Add(CollectableData.Position, CollectableData);
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
                        NodeAtPosition.Add(new Vector3(x + 0.5f, y + 0.5f, z + 0.5f), NodeData);
                    }
                }
            }
        }
    }


    public LevelSave(List<SceneTile> C_Scenetiles = null, List<Collectable> C_Collectables = null, List<IONode> C_Nodes = null)
    {
        Scenetiles = C_Scenetiles ?? new List<SceneTile>();
        Collectables = C_Collectables ?? new List<Collectable>();
        Nodes = C_Nodes ?? new List<IONode>();
    }
}


[System.Serializable]
public class SceneTile
{
    public TileType Type;
    public Vector3 Position;
    public Vector3Int Scale;

    public bool Standable => Type == TileType.Null || Type == TileType.Scaffold;

    public SceneTile(TileType C_Type, float C_XPos, float C_YPos, float C_ZPos, int C_XScale = 1, int C_YScale = 1, int C_ZScale = 1)
    {
        Type = C_Type;
        Position = new Vector3(C_XPos, C_YPos, C_ZPos);
        Scale = new Vector3Int(C_XScale, C_YScale, C_ZScale);
    }
}


[System.Serializable]
public class Collectable
{
    public Vector3 Position;
    public CollectableType Type;
    public bool Collected;
}


[System.Serializable]
public class IONode
{
    public int LevelTo;
    public Vector3 PositionTo;

    public Vector3 Position;
    public Vector3Int Scale;

    public bool Standable = false;

    public IONode(int C_LevelTo, Vector3 C_PositionTo, float C_XPos, float C_YPos, float C_ZPos, int C_XScale = 1, int C_YScale = 1, int C_ZScale = 1)
    {
        LevelTo = C_LevelTo;
        PositionTo = C_PositionTo;
        Position = new Vector3(C_XPos, C_YPos, C_ZPos);
        Scale = new Vector3Int(C_XScale, C_YScale, C_ZScale);
    }
}

public enum TileType
{
    Wall,
    Scaffold,
    Null,
}


public enum CollectableType
{

}


public class LevelCatalogue
{
    public static LevelSave LevelOne = new LevelSave(
        new List<SceneTile>
        {
            new SceneTile(TileType.Wall, 0, 0, 0, 20, 1, 20)
        },
        new List<Collectable>
        {

        });

    public static LevelSave GetLevelAtIndex(int C_Index)
    {
        return C_Index switch
        {
            1 => LevelOne,


            _ => new LevelSave(new List<SceneTile>(), new List<Collectable>()),
        };
    }
}