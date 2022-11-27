using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayState : PauseMenuState
{
    [SerializeField] GameObject _pausePanel;
    [SerializeField]

    public override void Enter()
    {
        _pausePanel.SetActive(false);

        for (int i = 0; i < StateMachine.MenuPanels.Length; i++)
        {
            StateMachine.MenuPanels[i].SetActive(false);
        }

        Time.timeScale = 1f;
    }
}
