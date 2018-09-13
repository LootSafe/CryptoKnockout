using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InsaneSystems.InputManager.UI
{
	public class AxisDropdown : MonoBehaviour
	{
		Dropdown selfDropdown;

		DropdownCallback actionOnChange;

		public delegate void DropdownCallback(string value);

		void Awake()
		{
			selfDropdown = GetComponent<Dropdown>();

			selfDropdown.options.Clear();

			string[] axis = InputStorage.Singleton.UsingAxis;

			selfDropdown.AddOptions(new List<string>(axis));
		}

		public int Value
		{
			get { return selfDropdown.value; }
			private set { selfDropdown.value = value; }
		}

		public void SetupActionOnChange(DropdownCallback newActionOnChange)
		{
			actionOnChange = newActionOnChange;
		}

		public void OnChange()
		{
			if (actionOnChange != null)
			{
				string axisName = InputStorage.Singleton.UsingAxis[selfDropdown.value];
				actionOnChange(axisName);
			}
		}

		public void SetValueByAxisName(string name)
		{
			Value = Settings.GetAxisIdByName(name);
		}
	}
}