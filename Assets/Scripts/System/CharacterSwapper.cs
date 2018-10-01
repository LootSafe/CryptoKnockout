using System.Collections;
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

    private static Characters GetRandom()
    {
        int choice = Random.Range((int)0, System.Enum.GetNames(typeof(Characters)).Length);
        return (Characters)choice;
    }

    public static GameObject GetCharacter(Characters c)
    {
        switch (c)
        {
            case Characters.BJORN:
                return GetInstance().Bjorn;
            case Characters.DOGE:
                return GetInstance().Doge;
            case Characters.ETHBOT:
                return GetInstance().EthBot;
            case Characters.BITCOINBOY:
                return GetInstance().BTCBoy;
            case Characters.MONERO:
                return GetInstance().Monero;
            case Characters.RANDOM:
                return GetCharacter(GetRandom());

            default:
                throw new System.Exception();
        }
    }
}
