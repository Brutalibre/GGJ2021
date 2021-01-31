using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixMesh : MonoBehaviour {
    void Start() {
        SkinnedMeshRenderer mesh = GetComponent<SkinnedMeshRenderer>();
        mesh.sharedMesh.RecalculateBounds();
        mesh.updateWhenOffscreen = true;
        mesh.allowOcclusionWhenDynamic = false;
    }
}
