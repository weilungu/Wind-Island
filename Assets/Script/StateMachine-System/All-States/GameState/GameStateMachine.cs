using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : StateMachine
{
    GameStateContext ctx;
    [SerializeField] GameState[] states;
    
    void Awake()
    {
        ctx = new GameStateContext(GetComponentsInChildren<Component>(true));
        
        stateTable = new Dictionary<Type, IState>(states.Length);
        
        foreach (GameState st in states)
        {
            st.Initialize(this, ctx);
            stateTable.Add(st.GetType(), st);
        }
    }
    
    void Start()
    {
        SwitchOn(stateTable[typeof(GameState_InGame)]);
    }
}
