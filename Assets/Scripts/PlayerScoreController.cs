using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreController : MonoBehaviour
{

    public Text score;
    //private int count;

    void Start()
    {
        //count = 0;
    }

    public void setScore(int amount)
    {
        score.text = "SCORE:" + amount.ToString();
    }

    /*
    public void addScore(int amount)
    {
        count += amount;
        score.text = "SCORE:" + count.ToString();
    }
    */
}
