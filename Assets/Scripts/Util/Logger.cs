using UnityEngine;

public enum DEVELOPER { ADAM, ANDY, BOTH, NONE };

public class Logger : MonoBehaviour
{    
    public DEVELOPER currentState; 

    private static Logger instance = null;
    private Logger() {}

    public static Logger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Logger();
            }
            return instance;
        }
    }

    public void Message(DEVELOPER state, string debugMessage)
    {
        switch (state)
        {
            case DEVELOPER.ADAM:
                print("[ADAM]: " + debugMessage);
                break;
            case DEVELOPER.ANDY:
                print("[ANDY]: " + debugMessage);
                break;
            case DEVELOPER.BOTH:
                print("[BOTH]: " + debugMessage);
                break;
            case DEVELOPER.NONE:
                break;
        }
    }
}
