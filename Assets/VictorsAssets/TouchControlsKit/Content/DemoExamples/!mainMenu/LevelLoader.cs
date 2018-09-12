using UnityEngine;
using UnityEngine.SceneManagement;
using TouchControlsKit;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private bool isMainMenu = false;


    // Update is called once per frame
    void Update()
    {
        if( isMainMenu )
        {
            if( TCKInput.GetAction( "btnFps", EActionEvent.Down ) )
            {
                SceneManager.LoadScene( "FirstPerson", LoadSceneMode.Single );
            }
            //
            if( TCKInput.GetAction( "btnPlatf", EActionEvent.Down ) )
            {
                SceneManager.LoadScene( "2DPlatformer", LoadSceneMode.Single );
            }
            //            
            if( TCKInput.GetAction( "btnBal", EActionEvent.Down ) )
            {
                SceneManager.LoadScene( "TiltBallDemo", LoadSceneMode.Single );
            }
            //
            if( TCKInput.GetAction( "btnCar", EActionEvent.Down ) )
            {
                SceneManager.LoadScene( "WheelCarDemo", LoadSceneMode.Single );
            }
        }
        else
        {
            //
            if( TCKInput.GetAction( "mButton", EActionEvent.Down ) )
            {
                SceneManager.LoadScene( "mainMenu", LoadSceneMode.Single );
            }
        }        
    }
}
