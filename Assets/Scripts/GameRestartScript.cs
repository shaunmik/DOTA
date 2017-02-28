using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestartScript : MonoBehaviour
{
    int degree = 2;
    float time = 0;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        // Rotate the camera around its local Y axis at 2 degree per second back and forth
        if (time <= 10)
        {
            transform.Rotate(new Vector3(0, degree, 0) * Time.deltaTime);
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
            if (degree == 2)
            {
                degree = -2;
            }
            else
            {
                degree = 2;
            }
        }

        if (Input.anyKey)
        {
            gameManager.resetGame();
            SceneManager.LoadScene("Game");
        }
    }
}
