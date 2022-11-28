using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Footstep_Script : MonoBehaviour
{
    public AudioSource footStepSound;
    [SerializeField] PlayerController playerController;

    private void Awake()
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
    }



    void Update()
    {
        footStepSound.enabled = ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && playerController.isGrounded == true);
            if ((Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))) 
            {
            footStepSound.Stop();
            }
    }
}
