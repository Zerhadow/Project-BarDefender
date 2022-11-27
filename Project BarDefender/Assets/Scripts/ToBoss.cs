using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ToBoss : MonoBehaviour
{
    public void toNextScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
