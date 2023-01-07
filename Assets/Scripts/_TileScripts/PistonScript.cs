using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonScript : MonoBehaviour
{
    private LevelManager LevelManager => GameObject.FindGameObjectWithTag("Level manager").GetComponent<LevelManager>();

    public Piston TileData;

    public void Bootup(SceneTile C_TileData)
    {
        TileData = (Piston)C_TileData;

        transform.position = C_TileData.Position;
        transform.localScale = C_TileData.Scale;
        GetComponent<TileLoader>().Bootup();

        if (TileData.MeshType != TileMesh.Blank)
        {
            transform.GetChild(0).GetComponent<TileRenderer>().BootUp(LevelManager.LevelData.MeshAssignment[C_TileData.MeshType]);

            MeshRenderingAsset RenderingAsset = new MeshRenderingAsset
            {
                MaterialReferences = new List<AddressableReference> { LevelManager.LevelData.MeshAssignment[C_TileData.MeshType].MaterialReferences[0] },
                MeshReference = new AddressableReference("System", "Cube")
            };
            transform.GetChild(1).GetComponent<TileRenderer>().BootUp(RenderingAsset);
        }
    }

    private bool Active = false;
    private float StartTime = 0;

    private AnimationBrickList PushOut => new AnimationBrickList(new List<AnimationBrick>
    {
        new AnimationBrick(0, 0,    0, 0, 0,    0, 0, 0),
        new AnimationBrick(1, 0,    0, 1, 0,    0, 0, 0),
    });

    private AnimationBrickList PushIn => new AnimationBrickList(new List<AnimationBrick>
    {
        new AnimationBrick(0, 0,    0, 1, 0,    0, 0, 0),
        new AnimationBrick(1, 0,    0, 0, 0,    0, 0, 0),
    });

    private AnimationBrickList PivotOut => new AnimationBrickList(new List<AnimationBrick>
    {
        new AnimationBrick(0, 0,    0, 0, 0,    0, 0, 0),
        new AnimationBrick(1, 0,    0, 1, 0,    0, 90, 0),
    });

    private AnimationBrickList PivotIn => new AnimationBrickList(new List<AnimationBrick>
    {
        new AnimationBrick(0, 0,    0, 1, 0,    0, 90, 0),
        new AnimationBrick(1, 0,    0, 0, 0,    0, 0, 0),
    });

    public void Update()
    {
        if (StartTime == 0)
        {
            StartTime = Time.fixedUnscaledTime;
        }

        float TimePassed = 5 * (Time.fixedUnscaledTime - StartTime);

        if (TimePassed >= 1)
        {
            TimePassed = 0.999f;
            StartTime = 0;
            enabled = false;
        }

        AnimationBrickList Animation = (Active, TileData.Type) switch
        {
            (true, TileType.PushPiston) => PushOut,
            (false, TileType.PushPiston) => PushIn,
            (true, TileType.PivotPiston) => PivotOut,
            (false, TileType.PivotPiston) => PivotIn,
            _ => null,
        };

        transform.GetChild(0).localPosition = Animation.Position(TimePassed);
        transform.GetChild(0).localEulerAngles = Animation.Rotation(TimePassed);
    }

    public void PlayerOn()
    {
        Active = true;
        enabled = true;
    }

    public void PlayerOff()
    {
        Active = false;
        enabled = true;
    }
}
