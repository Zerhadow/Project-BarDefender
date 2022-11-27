using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitState : MainMenuState
{
    [SerializeField] int _quitMenuIndex = 3;
    [SerializeField] Button _primaryButton;

    public override void Enter()
    {
        for (int i = 0; i < _quitMenuIndex; i++)
        {
            StateMachine.MenuPanels[i].SetActive(false);
        }

        StateMachine.MenuPanels[_quitMenuIndex].SetActive(true);

        _primaryButton.Select();
    }

    public override void Exit()
    {
        StateMachine.MenuPanels[_quitMenuIndex].SetActive(false);
    }

    public void OnPressedQuit()
    {
        Application.Quit();
    }
}
