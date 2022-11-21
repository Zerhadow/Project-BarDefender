using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioFile
{
    public string audioName;

    public AudioClip audioClip;

    [Range(0f,1f)]
    public float volume;

    [Range(0f,1f)]
    public float pitchVariation;
    
    [HideInInspector]
    public AudioSource source;

}
