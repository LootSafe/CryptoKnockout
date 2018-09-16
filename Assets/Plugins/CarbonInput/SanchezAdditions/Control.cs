using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control<T>
{
    public T control;
    public PlayerIndex pi;

    public Control(T control, PlayerIndex pi)
    {
        if (control.GetType() == typeof(CButton) || control.GetType() == typeof(CAxis))
        {
            this.control = control;
            this.pi = pi;
        }
        else
        {
            Debug.LogWarning("Control of type <T> should only be used to hold CAxis and CButton Types");
        }

    }
}
