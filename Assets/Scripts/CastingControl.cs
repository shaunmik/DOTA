using DigitalRuby.PyroParticles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class CastingControl : MonoBehaviour {
    public bool mouseMode;
    public GameObject LeftBulletPoint;
    public GameObject RightBulletPoint;
    public Camera cam;
    public GameObject[] Prefabs;

    private float nextSpellLeft;
    private float nextSpellRight;
    private FireBaseScript prefabScript; // TODO: move this variable to a spell class placeholder

    // Use this for initialization
    void Start () {
        nextSpellLeft = Time.time;
        nextSpellRight = Time.time;
    }
	
	// Update is called once per frame
	void Update () {    
        if (ActionControlListener.isRightTriggerFullyPressed() && Time.time > nextSpellRight)
            CastRightHandSpell(RightBulletPoint);
        if (ActionControlListener.isLeftTriggerFullyPressed() && Time.time > nextSpellLeft)
            CastLeftHandSpell(LeftBulletPoint);
    }
    
    private void CastLeftHandSpell(GameObject bulletPoint)
    {
        nextSpellLeft = Time.time + 0.5f;
        CastSpell(bulletPoint);
    }

    private void CastRightHandSpell(GameObject bulletPoint)
    {
        nextSpellRight = Time.time + 0.5f;
        CastSpell(bulletPoint);
    }

    // TODO: this needs to take another argument of spell s.t. spell.castRate, spell.delay from spell class
    private void CastSpell (GameObject bulletPoint)
    {
        Vector3 pos = bulletPoint.transform.position;
        Vector3 rotation = new Vector3(0,0,0);

        var spellPrefab = GameObject.Instantiate(Prefabs[0]);
        prefabScript = spellPrefab.GetComponent<FireConstantBaseScript>();

        if (prefabScript == null)
        {
            // temporary effect, like a fireball
            if (spellPrefab.GetComponent<FireBaseScript>().IsProjectile)
            {
                // set the start point near the hand
                rotation = cam.transform.rotation.eulerAngles;
                Debug.Log("rotation: " + rotation.ToString() + "| pos:" + pos.ToString());
            }
            else
            {
                prefabScript = spellPrefab.GetComponent<FireConstantBaseScript>();
            }
        }
        else
        {
            // set the start point in front of the player a ways, rotated the same way as the player
            pos = bulletPoint.transform.position;
            rotation = cam.transform.rotation.eulerAngles;
            pos.y = 0.0f;
        }

        spellPrefab.transform.position = pos;
        spellPrefab.transform.eulerAngles = rotation;
    }
}
