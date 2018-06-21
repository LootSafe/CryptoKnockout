using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum MOVESTATE { LEFT, RIGHT, CENTERAL };

public class Parallax : NetworkBehaviour
{

    public List<GameObject> parralaxSprites;
    public float baseIncrement = 0.01f;
    public float speed = 0.01f;

    float increment;
    MOVESTATE movestate;

    void Update () {

        if (!isLocalPlayer) return;

        /* State Logic */

        if (Input.GetKey(KeyCode.A))
        {
            movestate = MOVESTATE.LEFT;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movestate = MOVESTATE.RIGHT;
        }
        else
        {
            movestate = MOVESTATE.CENTERAL;
        }

        /* Move Logic */

        if (movestate == MOVESTATE.LEFT)
        {
            increment = baseIncrement;

            for (int i = 0; i <= parralaxSprites.Count - 1; i++)
            {
                increment += i * (speed / 10);

                Vector3 current = parralaxSprites[i].transform.position;
                parralaxSprites[i].transform.position = new Vector3(current.x += increment, current.y, current.z);
            }
        }
        else if(movestate == MOVESTATE.RIGHT)
        {
            increment = baseIncrement;

            for (int i = 0; i <= parralaxSprites.Count - 1; i++)
            {
                increment += i * (speed / 10); 

                Vector3 current = parralaxSprites[i].transform.position;
                parralaxSprites[i].transform.position = new Vector3(current.x -= increment, current.y, current.z);
            }
        }

    }
}