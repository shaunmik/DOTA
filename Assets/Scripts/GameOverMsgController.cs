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

        score.text = "Score: " + gameManager.getScore().ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
