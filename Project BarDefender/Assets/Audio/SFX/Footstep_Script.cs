using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep_Script : MonoBehaviour
{
    public AudioSource footStepSound;

    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            footStepSound.enabled = true;
        }
        else
        {
            footStepSound.enabled = false;
        }
    }
}
