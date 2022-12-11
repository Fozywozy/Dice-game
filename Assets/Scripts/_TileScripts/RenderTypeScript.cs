using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTypeScript : MonoBehaviour
{
    private LevelManager LevelManager => GameObject.FindGameObjectWithTag("Level manager").GetComponent<LevelManager>();

    public RenderType TileData;

    private Timer LightningTimer;

    private AnimationBrickList LightningAnimation = new AnimationBrickList(new List<AnimationBrick>
    {
        new AnimationBrick(0, 0,        0, 0, 0,    0, 0, 0),
        new AnimationBrick(0.2f, 1,     0, 0, 0,    0, 0, 0),
        new AnimationBrick(0.4f, 0.5f,  0, 0, 0,    0, 0, 0),
        new AnimationBrick(0.6f, 0.75f, 0, 0, 0,    0, 0, 0),
        new AnimationBrick(0.8f, 0,     0, 0, 0,    0, 0, 0),
        new AnimationBrick(1f, 0,       0, 0, 0,    0, 0, 0),
    },
        1f
    );

    public void Bootup(SceneTile C_TileData)
    {
        TileData = (RenderType)C_TileData;
        transform.position = C_TileData.Position;
        transform.localScale = C_TileData.Scale;
        GetComponent<TileLoader>().Bootup();
        Debug.Log(TileData.MeshType);
        if (TileData.MeshType != TileMesh.Blank)
        {
            transform.GetChild(0).GetComponent<TileRenderer>().BootUp(LevelManager.LevelData.MeshAssignment[C_TileData.MeshType]);
        }
    }

    public void Update()
    {
        if (LightningTimer)
        {
            enabled = false;
        }

        LightningAnimation.SetValues(transform.GetChild(0).gameObject, LightningTimer, false, false, false, true);
    }

    public void Trigger()
    {
        if (TileData.RenderingType == RenderingType.LightningBolt)
        {
            enabled = true;
            LightningTimer = new Timer(0.5f);
        }
    }

    public void PlayerOn()
    {
        if (TileData.RenderingType == RenderingType.Crushable)
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
            transform.position -= new Vector3(0, 0.25f, 0);
        }
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
