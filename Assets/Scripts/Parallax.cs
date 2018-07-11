using System;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Game.GameMode currentGameMode;

    /* Parallax Vars */

    public float deadZoneOffset = 2.0f;
    public float incrementBetweenSprites = 0.01f;
    public float speed = 0.2f;

    List<GameObject> parallaxSprites;
    GameObject leftBoundarySprite, rightBoundarySprite;
    float xCurrentPos, lastPosX;

    /* Camera Vars */

    float minZoom = 4.0f;
    float maxZoom = 5.0f;
    float distance, midpointX;
    Vector3 cameraStartPos;

    /* Methods */

    void Awake()
    {
        cameraStartPos = Camera.main.transform.position;

        leftBoundarySprite = GameObject.FindGameObjectWithTag("LeftBoundary");
        rightBoundarySprite = GameObject.FindGameObjectWithTag("RightBoundary");
        parallaxSprites = new List<GameObject>();

        foreach (Transform childLax in GameObject.Find("Parrallax").transform)
        {
            parallaxSprites.Add(childLax.gameObject);
        }

        /* REMOVE ME LATER */

        currentGameMode = Game.GameMode.LOCALMULTIPLAYER;
    }

    void FixedUpdate()
    {
        /* Listening for a player to join */

        /* Uncomment me later */

        //currentGameMode = Game.GetInstance().GetGameMode();

        if (IsMultiplayer())
        {
            distance = Vector2.Distance(GetPlayerPosition(0), GetPlayerPosition(1)) / 2;

            midpointX = GetPlayerPosition(0).x + (GetPlayerPosition(1).x - GetPlayerPosition(0).x) / 2;
            Camera.main.transform.position = new Vector3(midpointX, cameraStartPos.y, -10);

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
            xCurrentPos = GetMultiMidpointX();
        }
        else
        {
            xCurrentPos = GetPlayerPosition(0).x;
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
                        xIncrement += ((incrementBetweenSprites * speed) * (i + 1)) / 100;
                    }
                    else
                    {
                        xIncrement -= ((incrementBetweenSprites * speed) * (i + 1)) / 100;
                    }

                    current.x += xIncrement;
                }

                parallaxSprites[i].transform.position = new Vector3(current.x, current.y, current.z);
            }
        }

        if (IsMultiplayer())
        {
            lastPosX = GetMultiMidpointX();
        }
        else
        {
            Vector3 p1Position = GetPlayerPosition(0);
            lastPosX = p1Position.x;
        }
    }

    private bool InBounds()
    {
        float leftX = leftBoundarySprite.transform.position.x;
        float rightX = rightBoundarySprite.transform.position.x;

        float playerX = 0f;

        if (IsMultiplayer())
        {
            playerX = GetMultiMidpointX();
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
        if (currentGameMode == Game.GameMode.NETWORKMULTIPLAYER || currentGameMode == Game.GameMode.LOCALMULTIPLAYER)
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

    private float GetMultiMidpointX()
    {
        return GetPlayerPosition(0).x + (GetPlayerPosition(1).x - GetPlayerPosition(0).x) / 2;
    }

}