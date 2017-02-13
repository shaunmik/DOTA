using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthLevelController : MonoBehaviour {

        private float StartingLevel = 100f;
        public Image Earth;
        private float CurrentLevel; 
        private bool isEmpty; 

        public bool IsEmpty { get { return isEmpty; } }

        //Initialize the variables.
	void Start() {
            isEmpty = false;
            CurrentLevel = StartingLevel;
            Earth.fillAmount = 1f;	
	}
	
	public void DecrementElement(int amount) {
            if (IsEmpty)
                return;

            // Decrement the element by the amount specified but make sure it stays between the min and max.
            CurrentLevel -= amount;
            CurrentLevel = Mathf.Clamp(CurrentLevel, 0f, StartingLevel);

            // Set the element to show the normalised amount.
            Earth.fillAmount = CurrentLevel / StartingLevel;

            // If the current amount is approximately equal to zero
            if (Mathf.Abs(CurrentLevel) < float.Epsilon){
                isEmpty = true;
            }
		
	}
}
