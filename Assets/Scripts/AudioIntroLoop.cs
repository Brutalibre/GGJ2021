using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIntroLoop : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip musicIntro;

    void Start() {
        audioSource.PlayOneShot(musicIntro);
        audioSource.PlayScheduled(AudioSettings.dspTime + musicIntro.length);
    }
}
