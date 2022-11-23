using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsState : MainMenuState
{
    [SerializeField] int _settingsMenuIndex = 2;
    [SerializeField] Button _primaryButton;

    public override void Enter()
    {
        for (int i = 0; i < _settingsMenuIndex; i++)
        {
            StateMachine.MenuPanels[i].SetActive(false);
        }

        StateMachine.MenuPanels[_settingsMenuIndex].SetActive(true);


        _primaryButton.Select();
    }

    public override void Exit()
    {
        StateMachine.MenuPanels[_settingsMenuIndex].SetActive(false);
    }
}
