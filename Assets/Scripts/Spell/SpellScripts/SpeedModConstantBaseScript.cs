using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace DigitalRuby.PyroParticles
{
    /// <summary>
    /// Script for objects such as wall of fire that never expire unless manually stopped
    /// </summary>
    public class SpeedModConstantBaseScript : FireConstantBaseScript, ITriggerHandler
    {

        [Tooltip("how should speed of enemies change in the area.")]
        public float speedMultiplier = 0.5f;
        public Elements.elemEnum elementType;

        private List<Monster> affectedMonsters = new List<Monster>();


        void ITriggerHandler.OnTriggerEnter(GameObject obj, Collider c)
        {
            Debug.Log("[SpeedModConstantBaseScript.OnTriggerEnter] GameObject: " + obj + " | Collision: " + c);

            if (c.tag.Equals("Monster"))
            {
                Monster monster = c.gameObject.GetComponent<Monster>();
                monster.applySpeedChange(speedMultiplier, elementType);
                affectedMonsters.Add(monster);
            }
        }


        void ITriggerHandler.OnTriggerExit(GameObject obj, Collider c)
        {
            Debug.Log("[SpeedModConstantBaseScript.OnTriggerEnter] GameObject: " + obj + " | Collision: " + c);

            if (c.tag.Equals("Monster"))
            {
                Monster monster = c.gameObject.GetComponent<Monster>();
                monster.applySpeedChange(1 / speedMultiplier, elementType);
                affectedMonsters.Remove(monster);
            }
        }

        private void OnDestroy()
        {
            Debug.Log("OnDestroy");
            foreach (Monster monster in affectedMonsters)
            {
                monster.applySpeedChange(1 / speedMultiplier, elementType);
            }
        }

    }
}