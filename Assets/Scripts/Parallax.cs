using System;
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
    }

    void Update ()
    {
        /* Listening for a player to join */

        gamemode = Game.GetInstance().GetGameMode();

        /* Parallax Stuff */

        if (AtLeastOnePlayer())
        {
            float xMovement = Input.GetAxis("Horizontal");

            if (IsMultiplayer())
            {
                xCurrentPos = GetMidMultiplayer().x;
                yCurrentPos = GetMidMultiplayer().y;
            }
            else
            {
                xCurrentPos = GetMidSingleplayer().x;
                yCurrentPos = GetMidSingleplayer().y;
            }

            /* Sprite Movement Logic */

            if (HasPositionChanged() && InBounds())
            {
                Logger.Instance.Message(DEVELOPER.ANDY, "MOVING");

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

            if (IsMultiplayer())
            {
                lastPosX = GetMidMultiplayer().x;
                lastPosY = GetMidMultiplayer().y;

            }
            else
            {
                lastPosX = GetMidSingleplayer().x;
                lastPosY = GetMidSingleplayer().y;
            }
        }

    }

    private bool AtLeastOnePlayer()
    {
        if (Game.GetInstance().GetNumberOfPlayers() > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsMultiplayer()
    {
        if (Game.GetInstance().GetNumberOfPlayers() > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool HasPositionChanged()
    {
        if (lastPosX != xCurrentPos)
            return true;
        if (lastPosY != yCurrentPos)
            return true;

        return false;
    }

    private bool InBounds()
    {
        float leftX = leftBoundarySprite.transform.position.x;
        float rightX = rightBoundarySprite.transform.position.x;

        float playerX;

        if (IsMultiplayer())
        {
            playerX = GetMidMultiplayer().x;
        }
        else
        {
            playerX = GetMidSingleplayer().x;
        }

        if (leftX + deadZoneOffset >= playerX)
            return false;

        if (rightX - deadZoneOffset <= playerX)
            return false;

        return true;
    }

    private Vector3 GetMidSingleplayer()
    {
        return Game.GetInstance().GetPlayer(0).gameObject.transform.position;
    }

    private Vector3 GetMidMultiplayer()
    {
        Player p1 = Game.GetInstance().GetPlayer(0);
        Player p2 = Game.GetInstance().GetPlayer(1);
        return p1.gameObject.transform.position - (p2.gameObject.transform.position / 2);
    }
}