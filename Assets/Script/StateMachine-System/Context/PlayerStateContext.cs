using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateContext : Context
{
    public PlayerStateContext(params Component[] Services)
        => Initialize(Services);
}
