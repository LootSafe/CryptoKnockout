using System;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Game.GameMode currentGameMode;

    /* Parallax Vars */

    float deadZoneOffset = 2.0f;
    float incrementBetweenSprites = 0.02f;
    float speed = 1;

    List<GameObject> parallaxSprites;
    GameObject leftBoundarySprite, rightBoundarySprite;
    float xCurrentPos, yCurrentPos, lastPosX, lastPosY;

    /* Camera Vars */

    float minZoom = 4.0f;
    float maxZoom = 5.0f;
    float distance, midpointX;

    /* Methods */

    void Awake()
    {
        leftBoundarySprite = GameObject.FindGameObjectWithTag("LeftBoundary");
        rightBoundarySprite = GameObject.FindGameObjectWithTag("RightBoundary");

        parallaxSprites = new List<GameObject>();

        foreach (Transform childLax in GameObject.Find("Parrallax").transform)
        {
            parallaxSprites.Add(childLax.gameObject);
        }
    }

    void FixedUpdate()
    {
        /* Listening for a player to join */

        currentGameMode = Game.GetInstance().GetGameMode();

        if (IsMultiplayer())
        {
            distance = Vector3.Distance(GetPlayerPosition(0), GetPlayerPosition(1)) / 2;

            midpointX = GetPlayerPosition(0).x + (GetPlayerPosition(1).x - GetPlayerPosition(0).x) / 2;
            Camera.main.transform.position = new Vector3(midpointX, 0, -10);

            if (distance < maxZoom && distance > minZoom)
            {
                Camera.main.orthographicSize = distance;
            }

        }

    }

    void Update ()
    {
        /* Parallax Stuff */

        if (IsMultiplayer())
        {
            //xCurrentPos = GetMidMultiplayer().x;
            //yCurrentPos = GetMidMultiplayer().y;
        }
        else
        {
            Vector3 p1Position = GetPlayerPosition(0);

            xCurrentPos = p1Position.x;
            yCurrentPos = p1Position.y;
        }

        /* Sprite Movement Logic */

        if (HasPositionChanged() && InBounds())
        {
            for (int i = 0; i <= parallaxSprites.Count - 1; i++)
            {
                float xIncrement = 0;

                Vector3 current = parallaxSprites[i].transform.position;

                if (lastPosX != xCurrentPos)
                {
                    if (lastPosX > xCurrentPos)
                    {
                        xIncrement += (incrementBetweenSprites * speed) * (i + 1) / 10;
                    }
                    else
                    {
                        xIncrement -= (incrementBetweenSprites * speed) * (i + 1) / 10;
                    }

                    current.x += xIncrement;
                }

                parallaxSprites[i].transform.position = new Vector3(current.x, current.y, current.z);
            }

        }

        if (IsMultiplayer())
        {
            //lastPosX = GetMidMultiplayer().x;
            //lastPosY = GetMidMultiplayer().y;
        }
        else
        {
            Vector3 p1Position = GetPlayerPosition(0);

            lastPosX = p1Position.x;
            lastPosY = p1Position.y;
        }

    }

    private bool InBounds()
    {
        float leftX = leftBoundarySprite.transform.position.x;
        float rightX = rightBoundarySprite.transform.position.x;

        float playerX = 0f;

        if (IsMultiplayer())
        {
            //playerX = GetMidMultiplayer().x;
        }
        else
        {
            playerX = GetPlayerPosition(0).x;
        }

        if (leftX + deadZoneOffset >= playerX)
            return false;

        if (rightX - deadZoneOffset <= playerX)
            return false;

        return true;
    }

    private bool IsMultiplayer()
    {
        if (currentGameMode == Game.GameMode.NETWORKMULTIPLAYER && currentGameMode == Game.GameMode.LOCALMULTIPLAYER)
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
    
    private Player GetPlayer(int index)
    {
        return Game.GetInstance().GetPlayer(index);
    }

    private Vector3 GetPlayerPosition(int index)
    {
        return Game.GetInstance().GetPlayer(index).gameObject.transform.position;
    }

}