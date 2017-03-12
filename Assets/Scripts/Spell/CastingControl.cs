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

    [SerializeField]
    private LayerMask laycastLayerMask;
    [SerializeField]
    private List<Spells.InspectableSpellDictionaryEntry> spellSettings;

    private SpellController spellController;

    private Dictionary<Spells.spellEnum, Spells.SpellDetails> spellEnumToSpellDetails;
    private float nextSpellCooldownLeft;
    private float nextSpellCooldownRight;
    private FireBaseScript prefabScript; // TODO: move this variable to a spell class placeholder

    // Use this for initialization
    void Start()
    {
        spellController = FindObjectOfType<SpellController>();
        nextSpellCooldownLeft = Time.time;
        nextSpellCooldownRight = Time.time;
    }

    private void Awake()
    {
        spellEnumToSpellDetails = Spells.createSpellsEnumToSpellDetailsMap(spellSettings);
    }

    // Update is called once per frame
    void Update()
    {
        if (ActionControlListener.isRightTriggerFullyPressed() && Time.time > nextSpellCooldownRight)
        {
            CastSpell(Hands.handEnum.right);
        }
        if (ActionControlListener.isLeftTriggerFullyPressed() && Time.time > nextSpellCooldownLeft)
        {
            CastSpell(Hands.handEnum.left);
        }
    }
    
    private void CastSpell(Hands.handEnum hand)
    {
        ElementsPair elementPair;
        GameObject bulletPoint;
        if (hand == Hands.handEnum.left)
        {
            elementPair = spellController.LeftHandElementsPair;
            bulletPoint = LeftBulletPoint;
            nextSpellCooldownLeft = Time.time + 0.5f;
        } else if (hand == Hands.handEnum.right)
        {
            elementPair = spellController.RightHandElementsPair;
            bulletPoint = RightBulletPoint;
            nextSpellCooldownRight = Time.time + 0.5f;
        } else
        {
            Debug.Log("[CastingControl][CastSpell] Error: inappropriate hand is provided - " + hand);
            return;
        }

        if (elementPair.isNonePair())
        {
            return;
        }


        Spells.spellEnum spellEnum = Spells.elementsPairToSpellEnum[elementPair];
        Spells.SpellDetails spellDetail = spellEnumToSpellDetails[spellEnum];

        spellController.DecrementElement(elementPair.First, spellDetail.firstElementCost);
        spellController.DecrementElement(elementPair.Second, spellDetail.secondElementCost);
        // === setup done. shoot out bullet

        Vector3 pos = bulletPoint.transform.position;
        Vector3 rotation = new Vector3(0, 0, 0);

        var spellPrefab = GameObject.Instantiate(spellDetail.spellObject);
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
        }
        else
        {
            // TODO: Not sure if this way of checking for spell type is good idea. Maybe it is better to add "isProjectile" variable to Spells, but w/e
            // set the start point in front of the player a ways, rotated the same way as the player
            RaycastHit hit;

            pos = bulletPoint.transform.position;
            rotation = cam.transform.rotation.eulerAngles;
            rotation.x = 0; // this sets the spell up virtically
            pos.y = 0.0f; 

            Physics.Raycast(bulletPoint.transform.position, cam.transform.forward, out hit, 9000000f, laycastLayerMask);
            pos = hit.point;


        }

        spellPrefab.transform.position = pos;
        spellPrefab.transform.eulerAngles = rotation;
    }
}
