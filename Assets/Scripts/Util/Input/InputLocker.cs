using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLocker
{

    //private float delay = 1f;
    bool[,] axisLocks = new bool[9, 8];
    bool[,] buttonLocks = new bool[9, 10];
    float[,] axisTimes = new float[9, 8];

    public bool HasLock(Control<CAxis> control)
    {
        if (control.pi == PlayerIndex.Any) return false;
        return axisLocks[(int)control.pi, (int)control.control];
    }

    public void Unlock(Control<CAxis> control)
    {

        if (control.pi == PlayerIndex.Any) return;
        axisLocks[(int)control.pi, (int)control.control] = false;
    }

    public void Lock(Control<CAxis> control)
    {
        if (control.pi == PlayerIndex.Any) return;
        axisLocks[(int)control.pi, (int)control.control] = true;
    }



    public bool HasLock(Control<CButton> control)
    {
        if (control.pi == PlayerIndex.Any) return false;
        return buttonLocks[(int)control.pi, (int)control.control];
    }

    public void Unlock(Control<CButton> control)
    {

        if (control.pi == PlayerIndex.Any) return;
        buttonLocks[(int)control.pi, (int)control.control] = false;
    }

    public void Lock(Control<CButton> control)
    {
        if (control.pi == PlayerIndex.Any) return;
        buttonLocks[(int)control.pi, (int)control.control] = true;
    }
}
