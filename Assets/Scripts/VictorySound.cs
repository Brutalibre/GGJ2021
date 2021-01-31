using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictorySound : MonoBehaviour {
    void OnEnable() {
        GetComponent<AudioSource>().Play();
    }
}
