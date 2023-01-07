using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> Sprites = new List<Sprite>()
    {

    };

    [SerializeField]
    private GameObject Danger;
    [SerializeField]
    private GameObject Push;

    private LevelManager LevelManager => GameObject.FindGameObjectWithTag("Level manager").GetComponent<LevelManager>();
    private TimerList Timer;
    private bool Active = false;
    private LerpOnLoop RotationLerper;

    private float StartRotation = 0;
    private Timer AnimationTimer;
    private bool AnimationPlaying = false;

    public SceneTile TileData;


    public AnimationBrickList OpacityShake = new AnimationBrickList( new List<AnimationBrick>
    {
        new AnimationBrick(0, 0.8f,     0, -0.45f, 0,           0, 0, 0),
        new AnimationBrick(0.2f, 0.9f,  0.01f, -0.45f, -0.01f,  0, 0, 0),
        new AnimationBrick(0.4f, 0.6f,  0, -0.45f, 0.01f,       0, 0, 0),
        new AnimationBrick(0.6f, 0.85f, -0.01f, -0.45f, 0,      0, 0, 0),
        new AnimationBrick(0.8f, 0.9f,  0, -0.45f, 0,           0, 0, 0),
        new AnimationBrick(1, 0.8f,     0, -0.45f, 0,           0, 0, 0),
    },
        1f
    );


    public void Bootup(SceneTile C_TileData)
    {
        RotationLerper = new LerpOnLoop(0, 360);
        TileData = C_TileData;
        transform.position = C_TileData.Position;

        List<TimerList.TimerListSegmant> Timers = new List<TimerList.TimerListSegmant>();

        if (TileData.Type == TileType.PushTile)
        {
            GameObject PushTile = Instantiate(Push, transform);
            PushTile.transform.eulerAngles = ((PushTile)TileData).Directions[0];

            for (int Index = 0; Index < ((PushTile)TileData).TimesBetween.Count; Index++)
            {
                Timers.Add(new TimerList.TimerListSegmant(new TriggerableTimerComplete() { Timer = this, TileData = C_TileData, Index = Index }, ((PushTile)TileData).TimesBetween[Index]));
            }
        }
        else
        {
            _ = Instantiate(Danger, transform);
            for (int Index = 0; Index < ((DangerTile)TileData).TimesBetween.Count; Index++)
            {
                Timers.Add(new TimerList.TimerListSegmant(new TriggerableTimerComplete() { Timer = this, TileData = C_TileData, Index = Index }, ((DangerTile)TileData).TimesBetween[Index]));
            }
        }

        GetComponent<TileLoader>().Bootup();
        Timer = new TimerList(Timers);
    }


    public void Update()
    {
        if (Timer.Update(Active))
        {
            if (Active)
            {
                Active = false;
                if (TileData.Type == TileType.DangerTile)
                {
                    transform.position -= Vector3.up;
                }
            }
        }

        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Sprites[Mathf.FloorToInt(Timer.GetCurrentSegmant().Percent * 33)];

        if (AnimationPlaying)
        {
            if (AnimationTimer)
            {
                AnimationPlaying = false;
            }

            OpacityShake.SetValues(transform.GetChild(2).gameObject, AnimationTimer, true, false, false, true);

            if (TileData.Type == TileType.PushTile)
            {
                float NewY = RotationLerper.LerpBetween(StartRotation, ((PushTile)TileData).Rotations()[Timer.TimerIndex].y, AnimationTimer);
                transform.GetChild(2).eulerAngles = new Vector3(0, NewY, 0);
            }
        }
    }


    public void PlayerIn()
    {
        Active = true;
        transform.position += Vector3.up;
    }


    public void PlayerOut()
    {
        Active = false;
        transform.position -= Vector3.up;
    }


    public void PlayAnimation()
    {
        StartRotation = transform.GetChild(2).eulerAngles.y;
        AnimationPlaying = true;
        AnimationTimer = new Timer(0.2f);
    }
}
