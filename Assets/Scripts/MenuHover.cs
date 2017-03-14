using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuHover : MonoBehaviour
{
    public float sightlength = 100f;
    public float hoverForwardDistance = 5f;
    private NewGame newgame;
    private ExitGame exitgame;

    void Start(){
         newgame = FindObjectOfType<NewGame>();
         //campaign = FindObjectOfType<Campaign>();
         //tutorial = FindObjectOfType<Tutorial>();
         //highscores = FindObjectOfType<HighScores>();
         exitgame = FindObjectOfType<ExitGame>();
    }
    
    void Update(){
         RaycastHit seen;
         Ray raydirection = new Ray(transform.position, transform.forward);
         if (Physics.Raycast(raydirection, out seen, sightlength)){
             if (seen.collider.tag.Equals("NewGame")){
                 if (newgame.load()){
                    SceneManager.LoadScene("Game");
                 }
             }else if(seen.collider.tag.Equals("ExitGame")){
                 if (exitgame.load()){
                    Application.Quit();
                 }
             }else{
               newgame.unload();
               exitgame.unload();
             }             
         }else{
               newgame.unload();
               exitgame.unload();
        }   
    }
}
