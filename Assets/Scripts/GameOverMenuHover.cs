using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameOverMenuHover : MonoBehaviour
{
    public float sightlength = 100f;
    public float hoverForwardDistance = 5f;
    private Survival survival;
    private MenuGame back;

    public GameObject MainMenuObj;
    public GameObject HighScoreObj;

    private GameManager gameManager;
    void Start()
    {
        survival = FindObjectOfType<Survival>();
        back = FindObjectOfType<MenuGame>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        RaycastHit seen;
        Ray raydirection = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(raydirection, out seen, sightlength))
        {
            if (seen.collider.transform.name.Equals("MenuGame"))
            {
                survival.unload();

                if (back.load())
                {
                    gameManager.resetGame();
                    SceneManager.LoadScene("GameStart");
                }
            }
            else if (seen.collider.transform.name.Equals("QuitGame"))
            {
                back.unload();

                if (survival.load())
                {
                    Application.Quit();
                }
            }
            else
            {
                unloadEverything();
            }
        }
        else
        {
            unloadEverything();
        }
    }

    void unloadEverything()
    {
        survival.unload();
        back.unload();
    }
}
