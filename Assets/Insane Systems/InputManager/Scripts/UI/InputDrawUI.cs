using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InsaneSystems.InputManager.UI
{
    public class InputDrawUI : MonoBehaviour
    {
        float updateTime = 0.1f;
        Text textElement;

        float updateTimer = 0f;
        List<AxisAction> allAxis = new List<AxisAction>();

        void Start()
        {
            textElement = GetComponent<Text>();
            allAxis = InputStorage.Singleton.Axis;
        }

        void Update()
        {
            if (updateTimer > 0)
            {
                updateTimer -= Time.deltaTime;
                return;
            }

			if (allAxis.Count == 0)
			{
				textElement.text = "<color=red>Please setup any new axis in Insane Systems Input Manager!";
			}
			else
			{
				textElement.text = "Here you will see an input value from first setted up axis and from default input manager axis, which overriden by new axis:" + System.Environment.NewLine + System.Environment.NewLine;
				textElement.text += "Standart Input Manager \"" + allAxis[0].AxisName + "\" axis: <color=red>" + Input.GetAxis(allAxis[0].AxisName) + "</color>" + System.Environment.NewLine;
				//textElement.text += "Standart Input Manager \"Joystick X\" axis: <color=red>" + Input.GetAxis("Joystick Axis X") + "</color>" + System.Environment.NewLine;
				textElement.text += "Insane Systems Input Manager \"" + allAxis[0].Name + "\" axis: <color=red>" + InputController.GetAxisActionValue(allAxis[0].Name) + "</color>" + System.Environment.NewLine;
			}

            updateTimer = updateTime;
        }
    }
}