using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource audioSourceMenu;
    public AudioSource audioSourceGame;
    public AudioClip musicIntro;
    
    public bool inMenu = true;
    public static AudioManager instance = null;

    void Start() {
        if (instance) Destroy(this.gameObject);
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        audioSourceMenu.enabled = true;
        audioSourceGame.enabled = false;
    }

    public void ChangeScene(bool menu) {
        if (inMenu == menu) return;
        if (menu) {
            audioSourceMenu.enabled = true;
            audioSourceGame.enabled = false;
        } else {
            audioSourceMenu.enabled = false;
            audioSourceGame.enabled = true;
            audioSourceGame.PlayOneShot(musicIntro);
            audioSourceGame.PlayScheduled(AudioSettings.dspTime + musicIntro.length);
        }
        inMenu = menu;
    }
}
