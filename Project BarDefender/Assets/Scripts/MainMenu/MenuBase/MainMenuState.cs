using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MainMenuSM))]
public class MainMenuState : State
{
    protected MainMenuSM StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = GetComponent<MainMenuSM>();
    }

    public void OnPressedBack()
    {
        StateMachine.RevertState();
    }
}
