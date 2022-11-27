using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PauseMenuSM))]
public class PauseMenuState : State
{
    protected PauseMenuSM StateMachine { get; private set; }

    private void Awake()
    {
        StateMachine = GetComponent<PauseMenuSM>();
    }

    public void OnPressedBack()
    {
        StateMachine.RevertState();
    }
}
