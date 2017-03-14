using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuHover : MonoBehaviour
{
    public float sightlength = 100f;
    public float hoverForwardDistance = 5f;
    private ResumeGame resume;
    private QuitGame quit;
    private PauseGameController pgc;

    void Start(){
         pgc = FindObjectOfType<PauseGameController>();
    }
    
    void Update(){
         if (resume != null && quit != null){ 
		 RaycastHit seen;
		 Ray raydirection = new Ray(transform.position, transform.forward);
		 if (Physics.Raycast(raydirection, out seen, sightlength)){
		     if (seen.collider.tag.Equals("ResumeGame")){
		         if (resume.load()){
		           pgc.resume();
		            
		         }
		     }else if(seen.collider.tag.Equals("QuitGame")){
		         if (quit.load()){
		            SceneManager.LoadScene("GameStart");
		         }
		     }else{
		       resume.unload();
		       quit.unload();
		     }             
		 }else{
		       resume.unload();
		       quit.unload();
		} 
        } else {
            resume = FindObjectOfType<ResumeGame>();
            quit = FindObjectOfType<QuitGame>();
       }
    }
}
