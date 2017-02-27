using DigitalRuby.PyroParticles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.UI;

public class CastingControl : MonoBehaviour
{
    public bool mouseMode;
    public GameObject LeftBulletPoint;
    public GameObject RightBulletPoint;
    public Camera cam;
    public GameObject[] Prefabs;
    private FireLevelController fireLevelController;
    private WaterLevelController waterLevelController;
    private EarthLevelController earthLevelController;
    private WindLevelController windLevelController;
    private SpellController spellController;

    private float nextSpellLeft;
    private float nextSpellRight;
    private FireBaseScript prefabScript; // TODO: move this variable to a spell class placeholder

    // Use this for initialization
    void Start()
    {
        fireLevelController = FindObjectOfType<FireLevelController>();
        waterLevelController = FindObjectOfType<WaterLevelController>();
        earthLevelController = FindObjectOfType<EarthLevelController>();
        windLevelController = FindObjectOfType<WindLevelController>();
        spellController = FindObjectOfType<SpellController>();
        nextSpellLeft = Time.time;
        nextSpellRight = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (ActionControlListener.isRightTriggerFullyPressed() && Time.time > nextSpellRight)
        {
            CastRightHandSpell(RightBulletPoint);
        }
        if (ActionControlListener.isLeftTriggerFullyPressed() && Time.time > nextSpellLeft)
        {
            CastLeftHandSpell(LeftBulletPoint);
        }
    }

  
    // todo: make enum for elements & use it
    // Decrement the elements by the specified amount
    void DecrementElement(string elements, int amount, bool isLeftHand)
    {
        int elemCount = elements.Length;
        for (int i = 0; i < elemCount; i++)
        {
            if (elements[i] == '1')
            {
                if (fireLevelController.DecrementElement(amount)){
                    spellController.RemoveElementFromHand(isLeftHand,i,"1");
                    spellController.fireIsEmpty = true;
                }
            }
            else if (elements[i] == '2')
            {
                if (waterLevelController.DecrementElement(amount)){
                    spellController.RemoveElementFromHand(isLeftHand,i,"2");
                    spellController.waterIsEmpty = true;
                }
            }
            else if (elements[i] == '3')
            {
                if (earthLevelController.DecrementElement(amount)){
                    spellController.RemoveElementFromHand(isLeftHand,i,"3");
                    spellController.earthIsEmpty = true;
                }
            }
            else if (elements[i] == '4')
            {
                if (windLevelController.DecrementElement(amount)){
                    spellController.RemoveElementFromHand(isLeftHand,i,"4");
                    spellController.windIsEmpty = true; 
                }
            }
            //((FireLevelController)elemToCtrl[elements[i]]).DecrementElement(amount);
        }
    }

    private void CastLeftHandSpell(GameObject bulletPoint)
    {
        if (!string.IsNullOrEmpty(SpellController.leftElem[0]))
        {
           nextSpellLeft = Time.time + 0.5f;
           CastSpell(bulletPoint);
           DecrementElement(SpellController.leftElem[0], 10, true);
        }
    }

    private void CastRightHandSpell(GameObject bulletPoint)
    {
        if (!string.IsNullOrEmpty(SpellController.rightElem[0]))
        {
            nextSpellRight = Time.time + 0.5f;
            CastSpell(bulletPoint);
            DecrementElement(SpellController.rightElem[0], 10, false);
        }
    }

    // TODO: this needs to take another argument of spell s.t. spell.castRate, spell.delay from spell class
    private void CastSpell(GameObject bulletPoint)
    {
        Vector3 pos = bulletPoint.transform.position;
        Vector3 rotation = new Vector3(0, 0, 0);

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
            }
            else
            {
                prefabScript = spellPrefab.GetComponent<FireConstantBaseScript>();
            }
        }
        else
        {
            // TODO/DEBUG: TURNS OUT THIS IS ACTUALLY NEVER RAN
            // set the start point in front of the player a ways, rotated the same way as the player
            pos = bulletPoint.transform.position;
            rotation = cam.transform.rotation.eulerAngles;
            pos.y = 0.0f;
        }

        spellPrefab.transform.position = pos;
        spellPrefab.transform.eulerAngles = rotation;
    }
}
