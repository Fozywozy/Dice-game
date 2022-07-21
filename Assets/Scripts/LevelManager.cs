using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static Dictionary<SceneTile, Vector4> LinkNodes = new Dictionary<SceneTile, Vector4>
    {

    };

    public LevelSave LevelData = new LevelSave();


    public void Start()
    {

    }


    public void LoadLevel(SceneTile C_ExitNode)
    {
        LevelData = LevelCatalogue.GetLevelAtIndex((int)LinkNodes[C_ExitNode].w);
        LevelData.GenerateAtPositionList();
    }
}
