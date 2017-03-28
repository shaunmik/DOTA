using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static int score = 0;

    public static bool isCampaign = false;

    private PlayerScoreController playerScore;
    private ElementsPair elemsSelected = new ElementsPair();  // Current one or two elements selected


    // Use this for initialization
    void Start () {
        playerScore = FindObjectOfType<PlayerScoreController>();
    }


    public ElementsPair getElemsSelected()
    {
        return elemsSelected;
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

    public int getAverageScore()
    {
        return PlayerPrefs.HasKey("Average")?(int) PlayerPrefs.GetFloat("Average"):0;
    }
    public void enterScore(int score)
    {
        //Calculates average based on score, but doesn't accept 0 so that scene restarts don't count
        if (score != 0)
        {
            int timesplayed = PlayerPrefs.GetInt("TimesPlayed");
            float total = PlayerPrefs.GetFloat("Average") * timesplayed;
            float average = (total + score) / (timesplayed + 1);
            PlayerPrefs.SetFloat("Average", average);
            PlayerPrefs.SetInt("TimesPlayed", timesplayed + 1);
        }
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

    public void setCampaign(bool b)
    {
        isCampaign = b;
    }

    public bool getCampaign()
    {
        return isCampaign;
    }
}
