using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuHover : MonoBehaviour
{
    public float sightlength = 100f;
    public float hoverForwardDistance = 5f;
    private ResumeGame resume;
    private QuitGame quit;
    private MenuGame menu;
    private PauseGameController pgc;

    void Start()
    {
        pgc = FindObjectOfType<PauseGameController>();
    }

    void Update()
    {
        if (resume != null && quit != null && menu != null)
        {
            RaycastHit seen;
            Ray raydirection = new Ray(transform.position, transform.forward);
            
            if (Physics.Raycast(raydirection, out seen, sightlength))
            {
                if (seen.collider.tag.Equals("ResumeGame"))
                {  
                    if (resume.load())
                    {
                        pgc.resume();
                    }
                }
                else if (seen.collider.tag.Equals("QuitGame"))
                {
                    if (quit.load())
                    {
                        Application.Quit();
                    }
                }
                else if (seen.collider.transform.name.Equals("MenuGame"))
                {
                    if (menu.load())
                    {
                        pgc.inactivatePauseMenu();
                        Time.timeScale = 1;
                        SceneManager.LoadScene("GameStart");
                    }
                }
                else
                {
                    resume.unload();
                    quit.unload();
                    menu.unload();
                }
            }
            else
            {
                resume.unload();
                quit.unload();
                menu.unload();
            }
        }
        else
        {
            resume = FindObjectOfType<ResumeGame>();
            quit = FindObjectOfType<QuitGame>();
            menu = FindObjectOfType<MenuGame>();
        }
    }
}
