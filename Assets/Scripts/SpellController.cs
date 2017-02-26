using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{

    public bool pause = false;	// Game paused or not
    public bool fireIsEmpty = false;
    public bool waterIsEmpty = false;
    public bool earthIsEmpty = false;
    public bool windIsEmpty = false;
    public GameObject fire;
    public GameObject water;
    public GameObject earth;
    public GameObject wind;
    public Image LeftHandElement1;
    public Image LeftHandElement2;
    public Image RightHandElement1;
    public Image RightHandElement2;

    public enum Elements { none, fire, water, earth, wind };    // Elements enumerator
    public enum Spells
    {
        none, fire, water, earth, wind,
        fire2, water2, earth2, wind2,
        steam, magma, elec, mud, storm, sandstorm
    }; // Spell enumerator
    public enum Hands { left, right }   // Enumerator for hands

    public static string[] leftElem = new string[2]; // Spell held on left hand
    public static string[] rightElem = new string[2];// Spell held on right hand
    Hashtable elemToSpell;                           // Element to spell hashtable
    Hashtable elemToGo;                              // Element to Element GameObject hashtable
    Hashtable elemToImg;

    Queue elemsChosen;  // Current one or two elements selected

    void Start()
    {
        // TODO: 
        // pause = GetComponent <Something> ();

        elemsChosen = new Queue();
        elemToSpell = new Hashtable();
        elemToGo = new Hashtable();
        elemToImg = new Hashtable();

        // Create element combination to spell conversion
        elemToSpell.Add("1", (int)Spells.fire);
        elemToSpell.Add("2", (int)Spells.water);
        elemToSpell.Add("3", (int)Spells.earth);
        elemToSpell.Add("4", (int)Spells.wind);
        elemToSpell.Add("11", (int)Spells.fire);
        elemToSpell.Add("22", (int)Spells.water);
        elemToSpell.Add("33", (int)Spells.earth);
        elemToSpell.Add("44", (int)Spells.wind);
        elemToSpell.Add("12", (int)Spells.steam);
        elemToSpell.Add("13", (int)Spells.magma);
        elemToSpell.Add("14", (int)Spells.elec);
        elemToSpell.Add("23", (int)Spells.mud);
        elemToSpell.Add("24", (int)Spells.storm);
        elemToSpell.Add("34", (int)Spells.sandstorm);

        elemToGo.Add((int)Elements.fire, fire);
        elemToGo.Add((int)Elements.water, water);
        elemToGo.Add((int)Elements.earth, earth);
        elemToGo.Add((int)Elements.wind, wind);

        elemToImg.Add((int)Elements.fire, "fire_core");
        elemToImg.Add((int)Elements.water, "water_core");
        elemToImg.Add((int)Elements.earth, "earth_core");
        elemToImg.Add((int)Elements.wind, "wind_core");

        // Initialize the left hand with water element
        leftElem[0] = ((int)Elements.water).ToString();
        leftElem[1] = ((int)Spells.water).ToString();
        ChangeImage(LeftHandElement1, (string)elemToImg[(int)Elements.water]);

        // Initialize the right hand with fire element
        rightElem[0] = ((int)Elements.fire).ToString();
        rightElem[1] = ((int)Spells.fire).ToString();
        ChangeImage(RightHandElement1, (string)elemToImg[(int)Elements.fire]);

    }

    /* Update is called once per frame
	*/
    void Update()
    {
        if (ActionControlListener.isLeftConfirmPressed())
        {
            // Confirm left hand spell choice
            ConfirmSpell(Hands.left);
        }
        if (ActionControlListener.isRightConfirmPressed())
        {
            // Confirm right hand spell
            ConfirmSpell(Hands.right);
        }
        if (ActionControlListener.isFireButtonPressed())
        {
            QueueElement(Elements.fire);
        }
        if (ActionControlListener.isWaterButtonPressed())
        {
            QueueElement(Elements.water);
        }
        if (ActionControlListener.isWindButtonPressed())
        {
            QueueElement(Elements.wind);
        }
        if (ActionControlListener.isEarthButtonPressed())
        {
            QueueElement(Elements.earth);
        }
        // TODO: remove this
        // DEBUG CODE BEGINS
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Testing: " + GetSpell(true));
        }
    }


    /**
	  * Add element to the element queue.
	  * Max 2 elements; adding element when the queue
	  * has 2 already results in removal of the least
	  * recently added element.
	  * 
	  * Example call:
	  * Call this with QueueElement(Elements.fire)
	  * Where Elements is the public enum above
	  **/
    void QueueElement(Elements elem)
    {
        // Remove all elements if there are two already
        if (elemsChosen.Count == 2)
        {
            SelectElement((int)elemsChosen.Dequeue(), true);
            SelectElement((int)elemsChosen.Dequeue(), true);
            //elemsChosen.Clear();
        }
        elemsChosen.Enqueue((int)elem);
        SelectElement((int)elem, false);
    }

    /**
      * Highlights the elements from the queue in the GUI
      *
      * Example call:
      * SelectElement(Elements.fire, true)
      */
    void SelectElement(int elem, bool deselect)
    {
        //Deselect an element -- remove the spinning circle inside the element 
        if (deselect)
        {
            ((GameObject)elemToGo[elem]).SetActive(false);
        }
        else
        {
            //Select an element -- show the spinning circle inside the element 
            ((GameObject)elemToGo[elem]).SetActive(true);
        }
    }

    /**
    * Update the specified hand with provided elements
    */
    void updateHandElements(Hands hand, int[] elems)
    {
        int elemCount = elemsChosen.Count;
        if (hand == Hands.left)
        {
            if (elems.Length == 2)
            {
                ChangeImage(LeftHandElement1, (string)elemToImg[elems[0]]);
                ChangeImage(LeftHandElement2, (string)elemToImg[elems[1]]);
            }
            else
            {
                ChangeImage(LeftHandElement1, (string)elemToImg[elems[0]]);
            }
        }
        else
        {
            if (elems.Length == 2)
            {
                ChangeImage(RightHandElement1, (string)elemToImg[elems[0]]);
                ChangeImage(RightHandElement2, (string)elemToImg[elems[1]]);
            }
            else
            {
                ChangeImage(RightHandElement1, (string)elemToImg[elems[0]]);
            }
        }
    }

    // Change the given image's source image
    void ChangeImage(Image hand, string sourceImage)
    {
        hand.sprite = Resources.Load<Sprite>(sourceImage);
    }


    public void RemoveElementFromHandArray(bool isLeftHand, int elementNum)
    {
        if (isLeftHand)
        {
            if (leftElem[0].Length == 1)
            {
                leftElem[0] = "";
                leftElem[1] = "";
            }
            else
            {
                int i = Math.Abs(elementNum - 1);
                leftElem[0] = (leftElem[0][i]).ToString();
                int spell = (int)elemToSpell[leftElem[0]];
                leftElem[1] = spell.ToString();
            }
        }
        else
        {
            if (rightElem[0].Length == 1)
            {
                rightElem[0] = "";
                rightElem[1] = "";
            }
            else
            {
                int i = Math.Abs(elementNum - 1);
                rightElem[0] = (rightElem[0][i]).ToString();
                int spell = (int)elemToSpell[rightElem[0]];
                rightElem[1] = spell.ToString();
            }
        }
    }

    // check if elem is in isLeftHand hand, returns -1 if not in the hand
    public int ElementInOppositeHand(bool isLeftHand, string elem)
    {
        if (isLeftHand)
        {
            return leftElem[0].IndexOf(elem);
        }
        else
        {
            return rightElem[0].IndexOf(elem);
        }
    }

    // Remove the element image from hand
    public void RemoveHandElement(bool isLeftHand, string e)
    {
        if (isLeftHand)
        {
            if ((elemToImg[elemToSpell[e]]).Equals(LeftHandElement1.sprite.name))
            {
                ChangeImage(LeftHandElement1, "None");
            }
            else
            {
                ChangeImage(LeftHandElement2, "None");
            }
        }
        else
        {
            if ((elemToImg[elemToSpell[e]]).Equals(RightHandElement1.sprite.name))
            {
                ChangeImage(RightHandElement1, "None");
            }
            else
            {
                ChangeImage(RightHandElement2, "None");
            }
        }
    }

    // when the element is fully depleted, remove it from the hand that has it
    public void RemoveElementFromHand(bool isLeftHand, int elementNum, string e)
    {
        //Debug.Log("LeftHandElement1: " + LeftHandElement1.sprite.name);
        RemoveHandElement(isLeftHand, e);
        RemoveElementFromHandArray(isLeftHand, elementNum);

        // if element is also in the opposite hand, remove it
        int index = ElementInOppositeHand(!isLeftHand, e);
        if (index > -1)
        { // if found
            RemoveHandElement(!isLeftHand, e);
            RemoveElementFromHandArray(!isLeftHand, index);
        }
    }

    // remove any element that is empty
    public int[] RemoveEmptyElements(int[] elemArray)
    {
        List<int> elemList = new List<int>();
        for (int i = 0; i < elemArray.Length; i++)
        {
            int elem = elemArray[i];
            if (elem == 1 && !fireIsEmpty)
            {
                elemList.Add(elem);
            }
            else if (elem == 2 && !waterIsEmpty)
            {
                elemList.Add(elem);
            }
            else if (elem == 3 && !earthIsEmpty)
            {
                elemList.Add(elem);
            }
            else if (elem == 4 && !windIsEmpty)
            {
                elemList.Add(elem);
            }
        }
        return elemList.ToArray();

    }

    /**
	  * Get spell on given hand.
	  */
    public string GetSpell(bool isLeftHand)
    {
        if (isLeftHand)
        {
            return leftElem[1];
        }
        else
        {
            return rightElem[1];
        }
    }

    /**
	  * Change the spell held in given hand to the spell 
	  * according to element(s) in queue 
	  * 
	  * Example call:
	  * ConfirmSpell(Hands.left)
	**/
    void ConfirmSpell(Hands hand)
    {
        int[] elemArr = queueToSortedArray();
        int[] elemArray = RemoveEmptyElements(elemArr);
        if (elemArray.Length > 0)
        {
            string spellCode = getSpell(elemArray);
            int spell = (int)elemToSpell[spellCode];
            if (hand == Hands.left)
            {
                //leftElem = spell;
                leftElem[0] = spellCode;
                leftElem[1] = spell.ToString();
                updateHandElements(hand, elemArray);
            }
            else if (hand == Hands.right)
            {
                //rightElem = spell;
                rightElem[0] = spellCode;
                rightElem[1] = spell.ToString();
                updateHandElements(hand, elemArray);
            }
            else
            {
                Debug.Log("ConfirmSpell was given an invalid hand.");
            }

        }
        // Clear the current element queue
        if (elemArr.Length == 2)
        {
            SelectElement(elemArr[0], true);
            SelectElement(elemArr[1], true);
        }
        else if (elemArr.Length == 1)
        {
            SelectElement(elemArr[0], true);
        }
        //elemsChosen.Clear();

    }

    /**
	  * Get spell code from element
	  */
    string getSpell(int[] elemArray)
    {
        string spell = "";
        for (int i = 0; i < elemArray.Length; i++)
        {
            spell += elemArray[i].ToString();
        }
        return spell;
    }

    /**
	  * Returns a array form of elements chosen queue.
	  * Assuming the queue only has 2 elements.
	  */
    int[] queueToSortedArray()
    {
        int elemCount = elemsChosen.Count;
        int[] elemArray = new int[elemCount];
        if (elemCount > 0)
        {
            for (int i = 0; i < elemCount; i++)
            {
                elemArray[i] = (int)elemsChosen.Dequeue();
            }
            if (elemCount > 1)
            {
                // Sorting the order of elements in order of spell number
                // This needs to be changed to a for loop for proper sort
                // if more than 2 elements can be combined!
                if (elemArray[0] > elemArray[1])
                {
                    int temp = elemArray[0];
                    elemArray[0] = elemArray[1];
                    elemArray[1] = temp;
                }
            }
        }
        return elemArray;
    }
}

