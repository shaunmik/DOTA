using DigitalRuby.PyroParticles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.UI;

public class CastingControl : MonoBehaviour {
    public bool mouseMode;
    public GameObject LeftHand;
    public GameObject RightHand;
    public Camera cam;
    public GameObject[] Prefabs;
    private FireLevelController fireLevelController;
    private WaterLevelController waterLevelController;
    private EarthLevelController earthLevelController;
    private WindLevelController windLevelController;   

    private float nextSpellLeft;
    private float nextSpellRight;
    private FireBaseScript prefabScript; // TODO: move this variable to a spell class placeholder

    // Use this for initialization
    void Start () {
        fireLevelController = FindObjectOfType<FireLevelController>();
        waterLevelController = FindObjectOfType<WaterLevelController>();
        earthLevelController = FindObjectOfType<EarthLevelController>();
        windLevelController = FindObjectOfType<WindLevelController>();
        nextSpellLeft = Time.time;
        nextSpellRight = Time.time;
    }
	
	// Update is called once per frame
    void Update () {    
        if (ActionControlListener.isRightTriggerFullyPressed() && Time.time > nextSpellLeft){ 
            DecrementElements(false);    
            CastSpell(RightHand, false); 
        }       
        if (ActionControlListener.isLeftTriggerFullyPressed() && Time.time > nextSpellRight){
            DecrementElements(true);
            CastSpell(LeftHand, true);
        } 
    }


    // Decrement the elements set in the specified hand
    void DecrementElements(bool isLeftHand){
	  if (isLeftHand){
	      if (!string.IsNullOrEmpty(SpellController.leftElem[0])){
                  DecrementElement(SpellController.leftElem[0], 2);
              }
	  } else {
	      if (!string.IsNullOrEmpty(SpellController.rightElem[0])){
                  DecrementElement(SpellController.rightElem[0], 2);
              }
	  }
    }

    // Decrement the elements by the specified amount
    void DecrementElement(string elements, int amount){
         int elemCount = elements.Length;
         for (int i = 0; i < elemCount; i++) {
             if (elements[i] == '1'){
                fireLevelController.DecrementElement(amount);
             } else if(elements[i] == '2'){
                waterLevelController.DecrementElement(amount);
             } else if(elements[i] == '3'){
                earthLevelController.DecrementElement(amount);
             } else if(elements[i] == '4'){
                windLevelController.DecrementElement(amount);   
             }
	     //((FireLevelController)elemToCtrl[elements[i]]).DecrementElement(amount);
	}

    }

    // TODO: this needs to take another argument of spell s.t. spell.castRate, spell.delay from spell class
    private void CastSpell (GameObject hand, bool isLeftHand)
    {
        if (isLeftHand) 
            nextSpellLeft = Time.time + 0.5f;
        else 
            nextSpellRight = Time.time + 0.5f;

        Vector3 pos;
        float yRot = hand.transform.rotation.eulerAngles.y;
        Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        Vector3 forward = hand.transform.forward;
        Vector3 right = hand.transform.right;
        Vector3 up = hand.transform.up;
        Vector3 rotation = new Vector3(0,0,0);

        var spellPrefab = GameObject.Instantiate(Prefabs[0]);
        prefabScript = spellPrefab.GetComponent<FireConstantBaseScript>();

        if (prefabScript == null)
        {
            // temporary effect, like a fireball
            prefabScript = spellPrefab.GetComponent<FireBaseScript>();
            if (spellPrefab.GetComponent<FireBaseScript>().IsProjectile)
            {
                // set the start point near the hand
                rotation = cam.transform.rotation.eulerAngles;
                pos = hand.transform.position + forward;
            }
            else
            {
                prefabScript = spellPrefab.GetComponent<FireConstantBaseScript>();
                // set the start point in front of the player a ways
                pos = hand.transform.position + (forwardY * 10.0f);
            }
        }
        else
        {
            // set the start point in front of the player a ways, rotated the same way as the player
            pos = hand.transform.position + (forwardY * 5.0f);
            rotation = cam.transform.rotation.eulerAngles;
            pos.y = 0.0f;
        }

        spellPrefab.transform.position = pos;
        spellPrefab.transform.eulerAngles = rotation;
    }
}
