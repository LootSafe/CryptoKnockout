using UnityEngine;
using UnityEngine.SceneManagement;

namespace InsaneSystems.InputManager
{
    public class Menu : MonoBehaviour
    {
        public void LoadNextScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}