using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour {

      private float StartingLoadLevel = 0f;
      public Image Exit;
      private float CurrentLoadLevel;
      private bool loaded;

      void Start(){
            loaded = false;
            CurrentLoadLevel = StartingLoadLevel;
            Exit.fillAmount = 0f;	
      }
      public bool load(){     
            if (loaded)
               return loaded; 
            CurrentLoadLevel += 0.01f;
            Exit.fillAmount = CurrentLoadLevel;
            if (CurrentLoadLevel >= 1f){
                loaded = true;
            }
            return loaded;
      }
     public void unload(){
            CurrentLoadLevel = StartingLoadLevel;
            Exit.fillAmount = CurrentLoadLevel;  
            loaded = false; 
     }
}
