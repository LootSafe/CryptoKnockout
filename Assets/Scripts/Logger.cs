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
                print(debugMessage);
                break;
            case DEVELOPER.ANDY:
                print(debugMessage);
                break;
            case DEVELOPER.BOTH:
                print(debugMessage);
                break;
            case DEVELOPER.NONE:
                break;
        }
    }
}
