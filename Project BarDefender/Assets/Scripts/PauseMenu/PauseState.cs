using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseState : PauseMenuState
{
    [SerializeField] int _pauseMenuIndex = 0;
    [SerializeField] Button _primaryButton;
    [SerializeField] GameObject _pausePanel;

    public override void Enter()
    {
        _pausePanel.SetActive(true);

        for (int i = 0; i < StateMachine.MenuPanels.Length; i++)
        {
            StateMachine.MenuPanels[i].SetActive(false);
        }

        StateMachine.MenuPanels[_pauseMenuIndex].SetActive(true);

        _primaryButton.Select();

        Time.timeScale= 0f;
    }

    public override void Exit()
    {
        StateMachine.MenuPanels[_pauseMenuIndex].SetActive(false);
    }

    #region Pause Menu Functions
    public void OnPressedResume()
    {
        StateMachine.ChangeState<PlayState>();
    }

    public void OnPressedOptions()
    {
        StateMachine.ChangeState<OptionsState>();
    }

    public void OnPressedExit()
    {
        StateMachine.ChangeState<ExitState>();
    }
    #endregion
}
