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

        spellController.DecrementElement(elementPair.First, 10, hand);
        spellController.DecrementElement(elementPair.Second, 10, hand);

        Spells.spellEnum spellEnum = Spells.elementsPairToSpellEnum[elementPair];
        Spells.SpellDetails spellDetail = spellEnumToSpellDetails[spellEnum];
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
