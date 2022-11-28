using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{
    [SerializeField] public GameObject _musicManager;
    [SerializeField] public AudioClip _bossMusic;
    [SerializeField] public Camera _oldCamera;
    [SerializeField] public Camera _newCamera;
    [SerializeField] public GameObject _door;
 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Start Fight");
            //_musicManager.e
            _door.SetActive(true);
            _oldCamera.enabled = false;
            _newCamera.enabled = true;
            _musicManager.SetActive(true);
            
        }
    }
}
