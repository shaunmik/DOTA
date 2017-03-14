using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpellController : MonoBehaviour
{
    public bool pause = false;	// Game paused or not

    // game object that needs to be activated on element selection
    public GameObject fireSelector;
    public GameObject waterSelector;
    public GameObject earthSelector;
    public GameObject windSelector;

    // sprites used for hand indicator
    public Sprite emptyHandSprite;
    public Sprite fireHandSprite;
    public Sprite waterHandSprite;
    public Sprite earthHandSprite;
    public Sprite windHandSprite;

    // location of hand's spell image indicator
    public Image LeftHandElement1;
    public Image LeftHandElement2;
    public Image RightHandElement1;
    public Image RightHandElement2;


    private Dictionary<Elements.elemEnum, GameObject> elementToSelectorGameObjectDict;
    private Dictionary<Elements.elemEnum, Sprite> elementToHandSpriteDict;

    private ElementsPair leftHandElementsPair = new ElementsPair(); // Spell held on left hand
    private ElementsPair rightHandElementsPair = new ElementsPair();// Spell held on right hand
    private ElementsPair elemsSelected = new ElementsPair();  // Current one or two elements selected


    private FireLevelController fireLevelController;
    private WaterLevelController waterLevelController;
    private EarthLevelController earthLevelController;
    private WindLevelController windLevelController;

    private bool isControlDisabled;

    public void setIsControlDisabled(bool b)
    {
        isControlDisabled = b;
    }

    public ElementsPair LeftHandElementsPair
    {
        get { return leftHandElementsPair; }
    }


    public ElementsPair RightHandElementsPair
    {
        get { return rightHandElementsPair; }
    }


    void Start()
    {
        // TODO: 
        // pause = GetComponent <Something> ();

        elementToSelectorGameObjectDict = new Dictionary<Elements.elemEnum, GameObject>();
        elementToHandSpriteDict = new Dictionary<Elements.elemEnum, Sprite>();

        fireLevelController = FindObjectOfType<FireLevelController>();
        waterLevelController = FindObjectOfType<WaterLevelController>();
        earthLevelController = FindObjectOfType<EarthLevelController>();
        windLevelController = FindObjectOfType<WindLevelController>();

        elementToSelectorGameObjectDict = new Dictionary<Elements.elemEnum, GameObject>();


        elementToSelectorGameObjectDict.Add(Elements.elemEnum.none, null);
        elementToSelectorGameObjectDict.Add(Elements.elemEnum.fire, fireSelector);
        elementToSelectorGameObjectDict.Add(Elements.elemEnum.water, waterSelector);
        elementToSelectorGameObjectDict.Add(Elements.elemEnum.earth, earthSelector);
        elementToSelectorGameObjectDict.Add(Elements.elemEnum.wind, windSelector);

        elementToHandSpriteDict.Add(Elements.elemEnum.none, emptyHandSprite);
        elementToHandSpriteDict.Add(Elements.elemEnum.fire, fireHandSprite);
        elementToHandSpriteDict.Add(Elements.elemEnum.water, waterHandSprite);
        elementToHandSpriteDict.Add(Elements.elemEnum.earth, earthHandSprite);
        elementToHandSpriteDict.Add(Elements.elemEnum.wind, windHandSprite);
    }

    /* Update is called once per frame
	*/
    void Update()
    {
        if (isControlDisabled)
        {
            return;
        }
        if (ActionControlListener.isLeftConfirmPressed())
        {
            // Confirm left hand spell choice
            ConfirmSpell(Hands.handEnum.left);
        }
        if (ActionControlListener.isRightConfirmPressed())
        {
            // Confirm right hand spell
            ConfirmSpell(Hands.handEnum.right);
        }
        if (ActionControlListener.isFireButtonPressed())
        {
            QueueElement(Elements.elemEnum.fire);
        }
        if (ActionControlListener.isWaterButtonPressed())
        {
            QueueElement(Elements.elemEnum.water);
        }
        if (ActionControlListener.isWindButtonPressed())
        {
            QueueElement(Elements.elemEnum.wind);
        }
        if (ActionControlListener.isEarthButtonPressed())
        {
            QueueElement(Elements.elemEnum.earth);
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
	  * Where Elements is the Element.elemEnum
	  **/
    void QueueElement(Elements.elemEnum elem)
    {
        // Remove all elements if there are two already
        if (elemsSelected.getNumAssignedElements() == 2)
        {
            clearCurrentSelectedElements();
        }
        elemsSelected.pushIfPossibleElseClearAndPush(elem);
        HighlightElementSelection(elem, true);
    }

    /**
      * Highlights the elements from the queue in the GUI
      *
      * Example call:
      * SelectElement(Elements.fire, true)
      */
    void HighlightElementSelection(Elements.elemEnum elem, bool isSelect)
    {
        GameObject highlightGameObject = elementToSelectorGameObjectDict[elem];
        if (highlightGameObject != null)
        {
            highlightGameObject.SetActive(isSelect);
        }
    }

    /**
    * Update the specified hand sprite with current element
    */
    void updateHandElementsSelectionSprite(Hands.handEnum hand)
    {
        if (hand == Hands.handEnum.left)
        {
            ChangeImage(LeftHandElement1, elementToHandSpriteDict[leftHandElementsPair.First]);
            ChangeImage(LeftHandElement2, elementToHandSpriteDict[leftHandElementsPair.Second]);
        }
        else if (hand == Hands.handEnum.right)
        {
            ChangeImage(RightHandElement1, elementToHandSpriteDict[rightHandElementsPair.First]);
            ChangeImage(RightHandElement2, elementToHandSpriteDict[rightHandElementsPair.Second]);
        }
    }

    // Change the given image's source image
    void ChangeImage(Image imageObject, Sprite sourceImage)
    {
        imageObject.sprite = sourceImage;
    }


    // Used to reset hand's elements selection
    public void RemoveElementsFromHand(Hands.handEnum hand)
    {
        if (hand == Hands.handEnum.left)
        {
            leftHandElementsPair.clear();
        }
        else if (hand == Hands.handEnum.right)
        {
            rightHandElementsPair.clear();
        }
        updateHandElementsSelectionSprite(hand);
    }

    // remove any element that is empty
    public int[] RemoveEmptyElements(int[] elemArray)
    {
        List<int> elemList = new List<int>();
        for (int i = 0; i < elemArray.Length; i++)
        {
            int elem = elemArray[i];
            if (elem == 1 && !fireLevelController.IsEmpty)
            {
                elemList.Add(elem);
            }
            else if (elem == 2 && !fireLevelController.IsEmpty)
            {
                elemList.Add(elem);
            }
            else if (elem == 3 && !earthLevelController.IsEmpty)
            {
                elemList.Add(elem);
            }
            else if (elem == 4 && !windLevelController.IsEmpty)
            {
                elemList.Add(elem);
            }
        }
        return elemList.ToArray();

    }

    public bool hasEnoughResourceToCast(ElementsPair elementPair, int firstResourceCost, int secondResourceCost)
    {
        bool hasEnoughResource = true;
        int firstElementIndex = (int)elementPair.First;
        int secondElementIndex = (int)elementPair.Second;

        var elementsEnumCount = Enum.GetNames(typeof(Elements.elemEnum)).Length;
        int[] elementCosts = Enumerable.Repeat(0, elementsEnumCount).ToArray();
        elementCosts[firstElementIndex] = firstResourceCost;
        elementCosts[secondElementIndex] = secondResourceCost;

        List<Elements.elemEnum> depletedElements = new List<Elements.elemEnum>();
        if (fireLevelController.CurrentLevel < elementCosts[(int)Elements.elemEnum.fire])
        {
            hasEnoughResource = false;
        }
        if (waterLevelController.CurrentLevel < elementCosts[(int)Elements.elemEnum.water])
        {
            hasEnoughResource = false;
        }
        if (earthLevelController.CurrentLevel < elementCosts[(int)Elements.elemEnum.earth])
        {
            hasEnoughResource = false;
        }
        if (windLevelController.CurrentLevel < elementCosts[(int)Elements.elemEnum.wind])
        {
            hasEnoughResource = false;
        }
        

        return hasEnoughResource;
    }

    /**
	  * Get spell on given hand.
	  */
    public string GetSpell(bool isLeftHand)
    {
        return "not impl";
    }

    /**
	  * Change the spell held in given hand to the spell 
	  * according to element(s) in queue 
	  * 
	  * Example call:
	  * ConfirmSpell(Hands.left)
	**/
    void ConfirmSpell(Hands.handEnum hand)
    {
        if (elemsSelected.isNonePair())
        {
            Debug.Log("[SpellController][ConfirmSpell] no elements are chosen");
            return;
        }


        RemoveElementsFromHand(hand); // first reset hand's current selection

        // todo: may want to sort? - Shaun
        if (hand == Hands.handEnum.left)
        {
            leftHandElementsPair.clear();
            leftHandElementsPair.pushIfPossibleElseClearAndPush(elemsSelected.First);
            leftHandElementsPair.pushIfPossibleElseClearAndPush(elemsSelected.Second);
        }
        else if (hand == Hands.handEnum.right)
        {
            rightHandElementsPair.clear();
            rightHandElementsPair.pushIfPossibleElseClearAndPush(elemsSelected.First);
            rightHandElementsPair.pushIfPossibleElseClearAndPush(elemsSelected.Second);
        }
        else
        {
            Debug.Log("ConfirmSpell was given an invalid hand.");
        }
        updateHandElementsSelectionSprite(hand);

        clearCurrentSelectedElements();
    }

    public List<Elements.elemEnum> getDepletedElements()
    {
        List<Elements.elemEnum> depletedElements = new List<Elements.elemEnum>();
        if (fireLevelController.IsEmpty)
        {
            depletedElements.Add(Elements.elemEnum.fire);
        }
        if (waterLevelController.IsEmpty)
        {
            depletedElements.Add(Elements.elemEnum.water);
        }
        if (earthLevelController.IsEmpty)
        {
            depletedElements.Add(Elements.elemEnum.earth);
        }
        if (windLevelController.IsEmpty)
        {
            depletedElements.Add(Elements.elemEnum.wind);
        }

        return depletedElements;
    }

    // todo: make enum for elements & use it
    // Decrement the elements by the specified amount
    public void DecrementElement(Elements.elemEnum element, int amount)
    {
        if (element == Elements.elemEnum.fire)
        {
            fireLevelController.DecrementElement(amount);
        }
        else if (element == Elements.elemEnum.water)
        {
            waterLevelController.DecrementElement(amount);
        }
        else if (element == Elements.elemEnum.earth)
        {
            earthLevelController.DecrementElement(amount);
        }
        else if (element == Elements.elemEnum.wind)
        {
            windLevelController.DecrementElement(amount);
        }
    }

    private void clearCurrentSelectedElements()
    {
        HighlightElementSelection(elemsSelected.First, false);
        HighlightElementSelection(elemsSelected.Second, false);
        elemsSelected.clear();
    }
}

