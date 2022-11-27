using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitState : PauseMenuState
{
    [SerializeField] int _exitMenuIndex = 2;
    [SerializeField] Button _primaryButton;

    public override void Enter()
    {
        for (int i = 0; i < StateMachine.MenuPanels.Length; i++)
        {
            StateMachine.MenuPanels[i].SetActive(false);
        }

        StateMachine.MenuPanels[_exitMenuIndex].SetActive(true);

        _primaryButton.Select();
    }

    public override void Exit()
    {
        StateMachine.MenuPanels[_exitMenuIndex].SetActive(false);
    }

    public void ExitToMainMenu(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }
}
