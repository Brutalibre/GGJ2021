using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour {
    public bool inMenu = true;
    void Start() {
        AudioManager.instance.ChangeScene(inMenu);
    }
}
