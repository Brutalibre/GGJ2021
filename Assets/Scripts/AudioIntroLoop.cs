using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIntroLoop : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip musicIntro;

    public bool launchGame = false;

    void Start() {
        audioSource.PlayOneShot(musicIntro);
        audioSource.PlayScheduled(AudioSettings.dspTime + musicIntro.length);
        //audioSource.timeSamples
    }

    private void Update() {
        
    }
}
