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

    public int[] getHighscores()
    {
        //returns a sorted list of highscores, if unpopulated returns 0s
        int[] highscores = new int[3];
        for (int i=0; i<3; i++)
        {
            if (PlayerPrefs.HasKey("Position"+i+1))
            {
                highscores[i] = PlayerPrefs.GetInt("Position" + i+1);
            }
            else
            {
                highscores[i] = 0;
            }
        }
        return(highscores);
    }
    
    public void enterHighscore(int score)
    {
        //Saves the highscore in the correct order, if invalid highscore is passed it's not saved
        int[] highscores = getHighscores();
        for (int i=0;i<highscores.Length;i++)
        {
            if(score>highscores[i])
            {
                PlayerPrefs.SetInt("Position" + i+1, score);
                score = highscores[i];
            }
        }
    }

    public bool checkIfHighscore(int score)
    {
        //returns true if a new highscore has been set
        int[] highscores = getHighscores();
        for(int i=0;i<highscores.Length; i++)
        {
            if (highscores[i] < score)
                return true;
        }
        return false;
    }

    public void resetGame()
    {
        score = 0;
    }
}
