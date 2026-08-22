using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context
{
    protected readonly List<Component> services = new List<Component>();
    
    protected void Initialize(Component[] Services)
    {
        foreach (Component service in Services)
        {
            if (service is not null)
                services.Add(service);
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
