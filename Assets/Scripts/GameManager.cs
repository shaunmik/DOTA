using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static int score = 0;

    private PlayerScoreController playerScore;

    // Use this for initialization
    void Start () {
        playerScore = FindObjectOfType<PlayerScoreController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addScore(int scoreToAdd)
    {
        score += scoreToAdd;
        playerScore.setScore(score);
    }

    public int getScore()
    {
        return score;
    }

    public void resetGame()
    {
        score = 0;
    }
}
