using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuSM : StateMachine
{
    [SerializeField] GameObject[] _menuPanels;
    public GameObject[] MenuPanels => _menuPanels;

    private void Start()
    {
        ChangeState<PlayState>();
    }
}
