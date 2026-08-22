using System;
using System.Collections.Generic;
using UnityEngine;

public class GameStateContext : Context
{
    public GameStateContext(params Component[] Services)
        => Initialize(Services);
}
