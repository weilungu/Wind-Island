using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateContext
{
    readonly List<Component> services = new List<Component>();

    public PlayerStateContext(params Component[] services)
    {
        foreach (Component service in services)
        {
            if (service is not null)
                this.services.Add(service);
        }
    }

    public T Get<T>() where T : class
    {
        foreach (Component service in services)
        {
            if (service is T typedService)
                return typedService;
        }

        throw new InvalidOperationException($"Missing state context service: {typeof(T).Name}");
    }
}
