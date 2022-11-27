using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class BarSceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName; 
    public bool onBar ;
    public PlayerInputActions playerControls;

    void OnAwake(){
        onBar = false; 
        
    }
    void OnTriggerEnter2D(Collider2D other) { 
        if(other.tag == "Player") { 
            onBar = true; 
        }
    }

    void OnTriggerExit2D(Collider2D other) { 
        if(other.tag == "Player") { 
            onBar = false; 
        }
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.E) && onBar){
            EnterNextScene();
        }
           
    }

    private void EnterNextScene(){
        SceneManager.LoadScene(sceneName);
    }
}
