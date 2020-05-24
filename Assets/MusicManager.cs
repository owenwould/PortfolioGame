using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource music;

    private void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    public void startMusic()
    {
        music.Play();
    }

    public void stopMusic()
    {
        music.Stop();
    }




}
