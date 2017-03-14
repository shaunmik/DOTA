using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeGame : MonoBehaviour {

      private float StartingLoadLevel = 0f;
      public Image Resume;
      private float CurrentLoadLevel;
      private bool loaded;

      void Start(){
            loaded = false;
            CurrentLoadLevel = StartingLoadLevel;
            Resume.fillAmount = 0f;	
      }
      public bool load(){     
            if (loaded)
               return loaded; 
            CurrentLoadLevel += 0.01f;
            Resume.fillAmount = CurrentLoadLevel;
            if (CurrentLoadLevel >= 1f){
                loaded = true;
            }
            return loaded;
      }
     public void unload(){
            CurrentLoadLevel = StartingLoadLevel;
            Resume.fillAmount = CurrentLoadLevel;   
            loaded = false;
     }
}
