/*******************************************************
/********************************************
 * Copyright(c): 2018 Victor Klepikov       *
 *                                          *
 * Profile: 	 http://u3d.as/5Fb		    *
 * Support:      http://smart-assets.org    *
 ********************************************/


using UnityEngine.Events;

namespace TouchControlsKit
{
    public delegate void ActionEventHandler();
    public delegate void ActionAlwaysHandler( ETouchPhase touchPhase );

    public delegate void AxesEventHandler( float axisX, float axisY );
    public delegate void AxesAlwaysHandler( float axisX, float axisY, ETouchPhase touchPhase );

    
    [System.Serializable] public class ActionEvent : UnityEvent { }
    [System.Serializable] public class AlwaysActionEvent : UnityEvent<ETouchPhase> { }

    [System.Serializable] public class AxesEvent : UnityEvent<float, float> { }
    [System.Serializable] public class AlwaysAxesEvent : UnityEvent<float, float, ETouchPhase> { }
}
