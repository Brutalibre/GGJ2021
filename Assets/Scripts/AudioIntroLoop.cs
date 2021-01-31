using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIntroLoop : MonoBehaviour {
    public AudioSource audioSourceMenu;
    public AudioSource audioSourceGame;
    public AudioClip musicIntro;
    
    public bool inMenu = true;
    static bool instancied = false;

    void Start() {
        if (instancied) Destroy(this.gameObject);
        instancied = true;
        DontDestroyOnLoad(this.gameObject);

        audioSourceMenu.enabled = true;
        audioSourceGame.enabled = false;
    }

    public bool debug;
    public void Update() {
        if (debug)
            ChangeScene(!inMenu);

        debug = false;
    }

    public void ChangeScene(bool menu) {
        if(menu) {
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
