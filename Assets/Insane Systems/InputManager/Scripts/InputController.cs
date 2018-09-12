using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InsaneSystems.InputManager
{
    /// <summary>
    /// This class is needed just to simplify work with input. Really, only two methods of this class is neccessary:
    /// GetAxisAction() and GetKeyAction().
    /// </summary>
    public static class InputController
    {
        public static bool GetKeyActionIsActive(string actionName)
        {
            return GetKeyAction(actionName).IsActive();
        }
		
		public static bool GetKeyActionIsDown(string actionName)
        {
            return GetKeyAction(actionName).IsDown();
        }
		
		public static bool GetKeyActionIsUp(string actionName)
        {
            return GetKeyAction(actionName).IsUp();
        }

        public static float GetAxisActionValue(string actionName)
        {
            return GetAxisAction(actionName).GetValue();
        }

        public static AxisAction GetAxisAction(string actionName)
        {
            return InputStorage.Singleton.GetAxisByName(actionName);
        }

        public static KeyAction GetKeyAction(string actionName)
        {
            return InputStorage.Singleton.GetKeyByName(actionName);
        }
    }
}