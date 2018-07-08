using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{    
    public float speed;
    public float incrementBetweenSprites = 0.01f;
    public float deadZoneOffset = 2.0f;

    List<GameObject> pSprites;
    List<Vector3> pSpritesOrgPos;
    float xCurrentPos, yCurrentPos, lastPosX, lastPosY;
    GameObject leftBoundarySprite, rightBoundarySprite, daddyLax;

    Game game;
    Game.GameMode gamemode;

    void Awake()
    {
        game = Game.GetInstance();
        daddyLax = GameObject.Find("Parrallax");
        leftBoundarySprite = GameObject.FindGameObjectWithTag("LeftBoundary");
        rightBoundarySprite = GameObject.FindGameObjectWithTag("RightBoundary");

        pSprites = new List<GameObject>();
        pSpritesOrgPos = new List<Vector3>();

        foreach (Transform childLax in daddyLax.transform)
        {
            pSprites.Add(childLax.gameObject);
            pSpritesOrgPos.Add(childLax.gameObject.transform.position);
        }

        if (Game.GetInstance().GetPlayer(1) != null)
        {
            gamemode = Game.GetInstance().GetGameMode();
        }
        else
        {
            gamemode = Game.GameMode.SINGLEPLAYER;
        }

        lastPosX = transform.position.x;
        lastPosY = transform.position.y;
    }

    void Update ()
    {
        float xMovement = Input.GetAxis("Horizontal");
        
        if (onePlayerOnScreen())
        {
            xCurrentPos = transform.position.x;
            yCurrentPos = transform.position.y;
        }
        else
        {
            xCurrentPos = GetMidPoint().x;
            yCurrentPos = GetMidPoint().y;
        }

        if (HasPositionChanged() && inBounds())
        {
            for (int i = 0; i <= pSprites.Count - 1; i++)
            {
                float xIncrement = 0;

                Vector3 current = pSprites[i].transform.position;

                if (lastPosX != xCurrentPos)
                {
                    if (xMovement < 0)
                    {
                        xIncrement += (incrementBetweenSprites * speed) * (i + 1) / 10;
                    }
                    else
                    {
                        xIncrement -= (incrementBetweenSprites * speed) * (i + 1) / 10;
                    }

                    current.x += xIncrement;
                }

                pSprites[i].transform.position = new Vector3(current.x, current.y, current.z);
            }

        }

        if (onePlayerOnScreen())
        {
            lastPosX = transform.position.x;
            lastPosY = transform.position.y;
        }
        else
        {
            lastPosX = GetMidPoint().x;
            lastPosY = GetMidPoint().y;
        }

    }

    bool onePlayerOnScreen()
    {
        if(gamemode == Game.GameMode.NETWORKMULTIPLAYER || gamemode == Game.GameMode.SINGLEPLAYER)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool HasPositionChanged()
    {
        if (lastPosX != xCurrentPos)
            return true;
        if (lastPosY != yCurrentPos)
            return true;

        return false;
    }

    bool inBounds()
    {
        float leftX = leftBoundarySprite.transform.position.x;
        float rightX = rightBoundarySprite.transform.position.x;

        float playerX;

        if (gamemode == Game.GameMode.NETWORKMULTIPLAYER || gamemode == Game.GameMode.SINGLEPLAYER)
        {
            playerX = transform.position.x;
        }
        else
        {            
            playerX = GetMidPoint().x;
        }

        if (leftX + deadZoneOffset >= playerX)
            return false;

        if (rightX - deadZoneOffset <= playerX)
            return false;

        return true;
    }

    Vector3 GetMidPoint()
    {
        Player p1 = Game.GetInstance().GetPlayer(0);
        Player p2 = Game.GetInstance().GetPlayer(1);
        return p1.gameObject.transform.position - (p2.gameObject.transform.position / 2);
    }
}