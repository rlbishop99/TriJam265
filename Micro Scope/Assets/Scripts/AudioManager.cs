using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource music;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(music);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(music);
        }

        music.Play();
    }
    public void destoryObjects()
    {
        DestroyObject(music);
    }
}
