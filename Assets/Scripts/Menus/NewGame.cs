using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour {

      private float StartingLoadLevel = 0f;
      public Image Game;
      private float CurrentLoadLevel;
      private bool loaded;

      void Start(){
            loaded = false;
            CurrentLoadLevel = StartingLoadLevel;
            Game.fillAmount = 0f;	
      }
      public bool load(){     
            if (loaded)
               return loaded; 
            CurrentLoadLevel += 0.01f;
            Game.fillAmount = CurrentLoadLevel;
            if (CurrentLoadLevel >= 1f){
                loaded = true;
            }
            return loaded;
      }
     public void unload(){
            CurrentLoadLevel = StartingLoadLevel;
            Game.fillAmount = CurrentLoadLevel;   
            loaded = false;
     }
}
