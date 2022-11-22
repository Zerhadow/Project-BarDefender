using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTrack : MonoBehaviour
{
    public AudioSource track;
    
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(FadeMusic.StartFade(track, 3f, 1f));
    }
}
