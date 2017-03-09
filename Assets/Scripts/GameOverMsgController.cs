using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMsgController : MonoBehaviour {

    public Text score;

    private GameManager gameManager;
	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
        int playerscore = gameManager.getScore();
        if (gameManager.checkIfHighscore(playerscore))
        {
            gameManager.enterHighscore(playerscore);
            score.text = "New Highscore: " + gameManager.getScore().ToString();
        }
        else
        {
            score.text = "Score: " + gameManager.getScore().ToString();
        }
        int[] highscores = gameManager.getHighscores();
        for (int i = 0;i<highscores.Length;i++)
        {
            score.text+="\n" +"#" +(i+1)+": " + highscores[i];
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
