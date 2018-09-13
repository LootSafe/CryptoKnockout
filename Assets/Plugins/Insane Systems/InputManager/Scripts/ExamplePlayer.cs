using UnityEngine;
using UnityEngine.SceneManagement;

namespace InsaneSystems.InputManager
{
	public class ExamplePlayer : MonoBehaviour
	{
		[SerializeField] float speed = 2.5f;

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Escape))
				SceneManager.LoadScene(0);

			float axisX = InputController.GetAxisActionValue("Move Side");
			transform.position += transform.right * axisX * speed * Time.deltaTime;
		   
			float axisY = InputController.GetAxisActionValue("Move Along");
			transform.position += transform.forward * axisY * speed * Time.deltaTime;
		}
	}
}