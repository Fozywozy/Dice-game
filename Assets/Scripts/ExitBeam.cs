using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBeam : MonoBehaviour
{
    private GameObject PlayerObject => GameObject.FindGameObjectWithTag("Player");
    private GameObject Board => GameObject.FindGameObjectWithTag("Move board");
    private LevelManager LevelManager => GameObject.FindGameObjectWithTag("Level manager").GetComponent<LevelManager>();

    private Transform Beam1 => transform.GetChild(0);
    private Transform Beam2 => transform.GetChild(1);
    private Transform DecorativeBeam1 => transform.GetChild(2);
    private Transform DecorativeBeam2 => transform.GetChild(3);

    private bool Exiting = false;

    private AnimationBrickList Beam1Animation = new(new List<AnimationBrick>
    {
        new AnimationBrick(0, 0.4f,     0, 4.5f, 0,     0, 0, 0,        0, 0, 2),
        new AnimationBrick(0.2f, 0.4f,  0, 4.5f, 0,     0, -36, 0,        1, 2, 1),
        new AnimationBrick(0.8f, 0.4f,  0, 4.5f, 0,     0, -144, 0,        1, 2, 1),
        new AnimationBrick(1f, 0.4f,    0, 4.5f, 0,     0, -180, 0,        0, 2, 0),
    });

    private AnimationBrickList Beam2Animation = new(new List<AnimationBrick>
    {
        new AnimationBrick(0, 0,        0, 4.5f, 0,     0, 0, 0,        0, 2, 0),
        new AnimationBrick(0.2f, 0.2f,  0, 4.5f, 0,     0, 36, 0,        2, 2, 2),
        new AnimationBrick(0.4f, 0.2f,  0, 4.5f, 0,     0, 72, 0,        1.5f, 2, 1.5f),
        new AnimationBrick(0.6f, 0.2f,  0, 4.5f, 0,     0, 108, 0,        0, 2, 0),
        new AnimationBrick(1, 0.2f,     0, 4.5f, 0,     0, 180, 0,        0, 2, 0),
    });

    private AnimationBrickList DecorativeBeamAnimation = new(new List<AnimationBrick>
    {
        new AnimationBrick(0, 0,        0, 3.25f, 0,    0, 0, 0,        0, 0, 0),
        new AnimationBrick(0.05f, 0.2f, 0, 2f, 0,       0, 0, 0,        1, 1, 1),
        new AnimationBrick(0.5f, 0.1f,  0, 3.5f, 0,     0, 0, 0,        1, 1, 1),
        new AnimationBrick(0.95f, 0,    0, 3.75f, 0,    0, 0, 0,        1, 1, 1),
        new AnimationBrick(1f, 0,       0, 3.7f, 0,     0, 0, 0,        0, 0, 0),
    });


    private void Update()
    {
        Beam1Animation.SetValues(Beam1.gameObject, TimerManager.GetTimer("ExitBeam"), true, true, true, true);
        Beam2Animation.SetValues(Beam2.gameObject, TimerManager.GetTimer("ExitBeam"), true, true, true, true);
        DecorativeBeamAnimation.SetValues(DecorativeBeam1.gameObject, TimerManager.GetTimer("ExitBeam"), true, true, true, true);
        DecorativeBeamAnimation.SetValues(DecorativeBeam2.gameObject, TimerManager.GetTimer("ExitBeam"), true, true, true, true);

        if (TimerManager.GetTimer("ExitBeam"))
        {
            if (Exiting)
            {
                if (TimerManager.GetTimer("EntryBeam"))
                {
                    EnterAt(LevelManager.LastPosition);
                }
            }
            else
            {
                PlayerObject.GetComponent<MeshRenderer>().enabled = true;
                PlayerObject.GetComponent<Player>().SideUp = 1;
                Board.GetComponent<MeshRenderer>().enabled = true;
                Exiting = false;
                enabled = false;
            }
        }
    }


    public void ExitAt(Vector3 C_Position)
    {
        PlayerObject.GetComponent<MeshRenderer>().enabled = false;
        Board.GetComponent<MeshRenderer>().enabled = false;
        transform.position = C_Position;
        TimerManager.NewTimer("ExitBeam", 0.5f);
        TimerManager.NewTimer("EntryBeam", 2f);
        enabled = true;
        Exiting = true;
    }


    public void EnterAt(Vector3 C_Position)
    {
        PlayerObject.transform.position = C_Position;
        PlayerObject.transform.eulerAngles = Vector3.zero;
        PlayerObject.GetComponent<MeshRenderer>().enabled = false;

        Board.transform.position = C_Position += Vector3.down * 0.9f;
        Board.GetComponent<MeshRenderer>().enabled = false;
        transform.position = C_Position;
        TimerManager.NewTimer("ExitBeam", 0.5f);
        enabled = true;
        Exiting = false;
    }
}
