using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartState : MainMenuState
{
    public override void Enter()
    {
        // Reconsider loading a specific scene by name / string
        int currentActiveSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentActiveSceneIndex + 1);
    }
}
