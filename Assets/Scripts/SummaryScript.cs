using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SummaryScript : MonoBehaviour {

    Game game;

    public GameObject panel;

    public Text winnerText;

    public Text score1;
    public Text score2;

    public Text kill1;
    public Text kill2;

    public Text name1;
    public Text name2;
    

	// Use this for initialization
	void Start () {
        panel.SetActive(false);
    }
    public void Continue()
    {
        SceneManager.LoadScene("MainMenu");
    }


	// Update is called once per frame
	void Update () {
        if (!game)
        {
            game = Game.GetInstance();
            return;
        }
        
        if(game.GetState() == Game.State.SUMMARIZING)
        {
            panel.SetActive(true);

            winnerText.text = "Player " + (game.GetWinner()+1) + " wins the game!!!";

            kill1.text = game.GetPlayer(0).GetScore() + "";
            kill2.text = game.GetPlayer(1).GetScore() + "";

            score2.text = game.GetPlayer(0).GetDamageDealt() + "";
            score2.text = game.GetPlayer(1).GetDamageDealt() + "";

            name1.text = game.GetPlayer(0).name + "";
            name2.text = game.GetPlayer(1).name + "";
        }


        

	}
}
