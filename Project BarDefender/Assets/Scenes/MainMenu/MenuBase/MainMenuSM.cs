using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSM : StateMachine
{
    [SerializeField] GameObject[] _menuPanels;
    public GameObject[] MenuPanels => _menuPanels;

    private void Start()
    {
        ChangeState<MainState>();
    }
}
