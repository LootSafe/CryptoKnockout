using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InsaneSystems.InputManager
{
    [System.Serializable]
    public sealed class KeyAction : InputAction
    {
        [SerializeField] KeyCode key;
        [SerializeField] KeyCode alternativeKey;

        public KeyCode Key
        {
            get { return key; }
        }

        public KeyCode AlternativeKey
        {
            get { return alternativeKey; }
        }

        public string KeyName
        {
            get { return key.ToString(); }
        }

        public string AltKeyName
        {
            get { return alternativeKey.ToString(); }
        }

        public override bool IsActive()
        {
            return Input.GetKey(key) || Input.GetKey(alternativeKey);
        }
		
		public bool IsDown()
		{
			 return Input.GetKeyDown(key) || Input.GetKeyDown(alternativeKey);
		}

		public bool IsUp()
		{
			 return Input.GetKeyUp(key) || Input.GetKeyUp(alternativeKey);
		}
		
        public void UpdateKey(KeyCode newKey, bool updateAlternative)
        {
            if (updateAlternative)
                alternativeKey = newKey;
            else
                key = newKey;
        }
    }
}