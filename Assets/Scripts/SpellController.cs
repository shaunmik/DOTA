using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {

	public bool pause = false;	// Game paused or not

	public enum Elements {none, fire, water, earth, wind};	// Elements enumerator
	public enum Spells {none, fire, water, earth, wind, 
						   fire2, water2, earth2, wind2, 
						   steam, magma, elec, mud, storm, sandstorm}; // Spell enumerator
	public enum Hands {left, right}	// Enumerator for hands

	public int leftElem = (int) Spells.none;	// Spell held on left hand
	public int rightElem = (int) Spells.none;	// Spell held on right hand
	Hashtable elemToSpell;

	Queue elemsChosen;	// Current one or two elements selected

	void Start () {
		// TODO: 
		// pause = GetComponent <Something> ();
		
		elemsChosen = new Queue();
		elemToSpell = new Hashtable();

		// Create element combination to spell conversion
		elemToSpell.Add("1", (int) Spells.fire);
		elemToSpell.Add("2", (int) Spells.water);
		elemToSpell.Add("3", (int) Spells.earth);
		elemToSpell.Add("4", (int) Spells.wind);
		elemToSpell.Add("11", (int) Spells.fire);
		elemToSpell.Add("22", (int) Spells.water);
		elemToSpell.Add("33", (int) Spells.earth);
		elemToSpell.Add("44", (int) Spells.wind);
		elemToSpell.Add("12", (int) Spells.steam);
		elemToSpell.Add("13", (int) Spells.magma);
		elemToSpell.Add("14", (int) Spells.elec);
		elemToSpell.Add("23", (int) Spells.mud);
		elemToSpell.Add("24", (int) Spells.storm);
		elemToSpell.Add("34", (int) Spells.sandstorm);
	}
	
	// Update is called once per frame
	void Update () {
		if (ActionControlListener.isLeftConfirmPressed()) {
			// Confirm left hand spell choice
			ConfirmSpell(Hands.left);
		} 
		if (ActionControlListener.isRightTriggerPressed()) {
			// Confirm right hand spell 
			ConfirmSpell(Hands.right);
		} 
		if (ActionControlListener.isFireButtonPressed()) {
			QueueElement(Elements.fire);
		} 
		if (ActionControlListener.isWaterButtonPressed()) {
			QueueElement(Elements.water);
		}
		if (ActionControlListener.isWindButtonPressed()) {
			QueueElement(Elements.wind);
		}
		if (ActionControlListener.isEarthButtonPressed()) {
			QueueElement(Elements.earth);
		}

		// DEBUG CODE BEGINS
		if (Input.GetKeyDown("space")) {
			Debug.Log("Testing: " + GetSpell(true));
		}
	}


	/**
	  * Add element to the element queue.
	  * Max 2 elements; adding element when the queue
	  * has 2 already results in removal of the least
	  * recently added element.
	  **/
	void QueueElement(Elements elem) {
		// Remove all elements if there are two already
		if (elemsChosen.Count == 2) {
			elemsChosen.Clear();
		}
		elemsChosen.Enqueue((int)elem);
		// TODO: Call the UI function to display element (K)
	}


	/**
	  * Get spell on given hand.
	  */
	public int GetSpell(bool isLeftHand) {
		if (isLeftHand) {
			return leftElem;
		} else {
			return rightElem;
		}
	}
	
	/**
	  * Change the spell held in given hand to the spell 
	  * according to element(s) in queue 
	  **/
	void ConfirmSpell(Hands hand) {
		int[] elemArray = queueToSortedArray();
		string spellCode = getSpell(elemArray);
		int spell = (int) elemToSpell[spellCode];
		if (hand == Hands.left) {
			leftElem = spell;
		} else if (hand == Hands.right) {
			rightElem = spell;
		} else {
			Debug.Log("ConfirmSpell was given an invalid hand.");
		}
		// Clear the current element queue
		elemsChosen.Clear();
	}

	/**
	  * Get spell code from element
	  */
	string getSpell (int[] elemArray) {
		string spell = "";
		for (int i = 0; i < elemArray.Length; i++) {
			spell += elemArray[i].ToString();
		}
		return spell;
	}

	/**
	  * Returns a array form of elements chosen queue.
	  * Assuming the queue only has 2 elements.
	  */
	int[] queueToSortedArray () {
		int elemCount = elemsChosen.Count;
		int[] elemArray = new int[elemCount];
		for (int i = 0; i < elemCount; i++) {
			elemArray[i] = (int) elemsChosen.Dequeue();
		}
		if (elemCount > 1) {
			// Sorting the order of elements in order of spell number
			// This needs to be changed to a for loop for proper sort
			// if more than 2 elements can be combined!
			if (elemArray[0] > elemArray[1]) {
				int temp = elemArray[0];
				elemArray[0] = elemArray[1];
				elemArray[1] = temp;
			}
		}
		return elemArray;
	}
}

