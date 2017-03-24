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
        
        spellController = GetComponent< SpellController >();
        nextSpellCooldownLeft = Time.time;
        nextSpellCooldownRight = Time.time;
        
    }

    // 1

    private SteamVR_TrackedObject trackedObj;
    // 2
    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }

    }

    private void Awake() 
    {
        spellEnumToSpellDetails = Spells.createSpellsEnumToSpellDetailsMap(spellSettings);
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Controller.GetAxis() != Vector2.zero)
        {
            //Debug.Log(gameObject.name + Controller.GetAxis());
        }

        // 2
        if (Controller.GetHairTriggerDown())
        {
            Debug.Log(gameObject.name + " Trigger press");
            //CastSpell(Hands.handEnum.right);
        }

        // 3
        if (Controller.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + " Trigger Release");
        }

        // 4
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Press");
        }

        // 5
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + " Grip Release");
        }

        if (ActionControlListener.isRightTriggerFullyPressed() && Time.time > nextSpellCooldownRight)
        {
            Debug.Log("testright");
            CastSpell(Hands.handEnum.right);
        }
        if (ActionControlListener.isLeftTriggerFullyPressed() && Time.time > nextSpellCooldownLeft)
        {
            Debug.Log("testleft");
            CastSpell(Hands.handEnum.left);
        }
    }

    private void CastSpell(Hands.handEnum hand)
    {
        ElementsPair elementPair;
        GameObject bulletPoint;
        if (hand == Hands.handEnum.left)
        {
            Debug.Log("Left hand cast");
            Debug.Log(spellController.LeftHandElementsPair.First.ToString() + " " + spellController.LeftHandElementsPair.Second.ToString());
            elementPair = spellController.LeftHandElementsPair;
            bulletPoint = LeftBulletPoint;
            nextSpellCooldownLeft = Time.time + 0.5f;
        }
        else if (hand == Hands.handEnum.right)
        {
            Debug.Log("Right hand cast");
            elementPair = spellController.RightHandElementsPair;
            bulletPoint = RightBulletPoint;
            nextSpellCooldownRight = Time.time + 0.5f;
        }
        else
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

        bool shouldCreateBullet = spellController;
        // === setup done. shoot out bullet

        Vector3 pos = bulletPoint.transform.position;
        Vector3 rotation = new Vector3(0, 0, 0);

        prefabScript = spellDetail.spellObject.GetComponent<FireConstantBaseScript>();

        if (prefabScript == null)
        {
            // temporary effect, like a fireball
            prefabScript = spellDetail.spellObject.GetComponent<FireBaseScript>();
            if (spellDetail.spellObject.GetComponent<FireBaseScript>().IsProjectile)
            {
                // set the start point near the hand
                rotation = trackedObj.transform.rotation.eulerAngles;
            }
        }
        else
        {
            // TODO: Not sure if this way of checking for spell type is good idea. Maybe it is better to add "isProjectile" variable to Spells, but w/e
            // set the start point in front of the player a ways, rotated the same way as the player
            RaycastHit hit;

            pos = bulletPoint.transform.position;
            rotation = trackedObj.transform.rotation.eulerAngles;
            rotation.x = 0; // this sets the spell up virtically
            pos.y = 0.0f;

            Physics.Raycast(bulletPoint.transform.position, trackedObj.transform.forward, out hit, 9000000f, laycastLayerMask);
            if (hit.collider == null)
            {
                shouldCreateBullet = false;
            }

            pos = hit.point;


        }

        if (shouldCreateBullet && spellController.hasEnoughResourceToCast(elementPair, spellDetail.firstElementCost, spellDetail.secondElementCost))
        {
            var spellPrefab = GameObject.Instantiate(spellDetail.spellObject, pos, Quaternion.Euler(rotation));

            Elements.elemEnum curSecondElement = elementPair.Second; // this is because "DecrementElement" may modify this
            spellController.DecrementElement(elementPair.First, spellDetail.firstElementCost);
            spellController.DecrementElement(curSecondElement, spellDetail.secondElementCost);
        }
    }
}
