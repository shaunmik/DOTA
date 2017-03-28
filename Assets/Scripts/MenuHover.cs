using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuHover : MonoBehaviour
{
    public float sightlength = 100f;
    public float hoverForwardDistance = 5f;
    private Survival survival;
    private ExitGame exitgame;
    private Tutorial tutorial;
    private HighScores highscores;
    private Campaign campaign;
    private GameStart_Back back;

    public GameObject MainMenuObj;
    public GameObject HighScoreObj;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        survival = FindObjectOfType<Survival>();
        campaign = FindObjectOfType<Campaign>();
        tutorial = FindObjectOfType<Tutorial>();
        highscores = FindObjectOfType<HighScores>();
        exitgame = FindObjectOfType<ExitGame>();
        back = FindObjectOfType<GameStart_Back>();
    }

    void Update()
    {
        RaycastHit seen;
        Ray raydirection = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(raydirection, out seen, sightlength))
        {
            if (seen.collider.transform.name.Equals("Survival"))
            {
                gameManager.setCampaign(false);
                exitgame.unload();
                campaign.unload();
                tutorial.unload();
                highscores.unload();

                if (survival.load())
                {
                    SceneManager.LoadScene("Game");
                }
            }
            else if (seen.collider.transform.name.Equals("ExitGame"))
            {
                survival.unload();
                campaign.unload();
                tutorial.unload();
                highscores.unload();

                if (exitgame.load())
                {
                    Application.Quit();
                }
            }
            else if (seen.collider.transform.name.Equals("HighScores"))
            {
                survival.unload();
                exitgame.unload();
                campaign.unload();
                tutorial.unload();

                highscores = FindObjectOfType<HighScores>();
                if (highscores.load())
                {
                    MainMenuObj.SetActive(false);
                    HighScoreObj.SetActive(true);
                }
            }
            else if (seen.collider.transform.name.Equals("Campaign"))
            {
                gameManager.setCampaign(true);

                survival.unload();
                exitgame.unload();
                tutorial.unload();
                highscores.unload();

                if (campaign.load())
                {
                    SceneManager.LoadScene("Game_Level1");
                }
            }
            else if (seen.collider.transform.name.Equals("Tutorial"))
            {
                gameManager.setCampaign(false);

                survival.unload();
                exitgame.unload();
                campaign.unload();
                highscores.unload();

                if (tutorial.load())
                {
                    SceneManager.LoadScene("Tutorial");
                }
            }
            else if (seen.collider.transform.name.Equals("Back"))
            {
                survival.unload();
                exitgame.unload();
                campaign.unload();
                tutorial.unload();
                highscores.unload();

                back = FindObjectOfType<GameStart_Back>();
                if (back.load())
                {
                    back.unload();
                    MainMenuObj.SetActive(true);
                    HighScoreObj.SetActive(false);
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
        exitgame.unload();
        campaign.unload();
        tutorial.unload();
        highscores.unload();
    }
}
