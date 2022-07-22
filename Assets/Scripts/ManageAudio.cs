using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAudio : MonoBehaviour
{
    public bool musicPlaying=false;
    AudioSource bgmMusic;
    void Start()
    {
        bgmMusic=GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (musicPlaying==false){
            bgmMusic.Play(0);
            musicPlaying=true;
        }
        
    }
}
