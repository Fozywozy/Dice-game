using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelSave LevelData;


    public void Start()
    {
        LevelData = new LevelSave();
    }


    public void LoadLevel(EntryExitNode C_Node)
    {
        LevelData = LevelCatalogue.GetLevelAtIndex(C_Node.TileData.LevelTo);
        LevelData.GenerateAtPositionList();
    }
}
