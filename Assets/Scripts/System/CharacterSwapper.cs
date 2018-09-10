﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwapper : MonoBehaviour {

    public GameObject Doge;
    public GameObject Bjorn;
    public GameObject EthBot;
    public GameObject BTCBoy;
    public GameObject Monero;

    private static CharacterSwapper instance;

    public void Awake() {
        instance = this;
    }
    private static CharacterSwapper GetInstance()
    {
        return instance;
    }

    public static GameObject GetCharacter(Character.Characters c)
    {
        switch (c)
        {
            case Character.Characters.BJORN:
                return GetInstance().Bjorn;
            case Character.Characters.DOGE:
                return GetInstance().Doge;
            case Character.Characters.ETHBOT:
                return GetInstance().EthBot;
            case Character.Characters.BITCOINBOY:
                return GetInstance().BTCBoy;
            case Character.Characters.MONERO:
                return GetInstance().Monero;
            default:
                throw new System.Exception();
        }
    }
}