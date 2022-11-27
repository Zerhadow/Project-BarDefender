using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep_Script : PlayerController
{
    public AudioSource footStepSound;

    private bool GetIsGrounded()
    {
        return isGrounded;
    }

    void Update(bool isGrounded)
    {
        footStepSound.enabled = (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && isGrounded == true;
    }
}
