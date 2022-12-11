using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip Song1;
    [SerializeField]
    private AudioClip Song2;
    int C_CurrentSong = 0;

    public AudioClip GetSong(int C_Index)
    {
        return C_Index switch
        {
            1 => Song1,
            2 => Song2,
            _ => null,
        };
    }

    public void PlaySong(int C_Song)
    {
        if (C_Song != 0)
        {
            if (C_Song != C_CurrentSong)
            {
                GetComponent<AudioSource>().clip = GetSong(C_Song);
                GetComponent<AudioSource>().Play();
                C_CurrentSong = C_Song;
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }
    }
}
