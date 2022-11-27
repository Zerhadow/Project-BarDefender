using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsState : PauseMenuState
{
    [SerializeField] int _optionsIndex = 1;
    [SerializeField] Button _primaryButton;

    public override void Enter()
    {
        for (int i = 0; i < StateMachine.MenuPanels.Length; i++)
        {
            StateMachine.MenuPanels[i].SetActive(false);
        }

        StateMachine.MenuPanels[_optionsIndex].SetActive(true);

        _primaryButton.Select();
    }

    public override void Exit()
    {
        StateMachine.MenuPanels[_optionsIndex].SetActive(false);
    }
}
