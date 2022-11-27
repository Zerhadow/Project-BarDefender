using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Footstep_Script : MonoBehaviour
{
    public AudioSource footStepSound;

    
    

    

    void Update()
    {
        footStepSound.enabled = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && PlayerController.isGrounded == true;

    }
}
