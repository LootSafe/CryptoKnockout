using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InsaneSystems.InputManager
{
    [System.Serializable]
    public abstract class InputAction
    {
        [SerializeField] protected string name;

        public string Name
        {
            get { return name; }
        }

        public abstract bool IsActive();
    }
}