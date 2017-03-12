using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{
    public interface ITriggerHandler
    {
        void OnTriggerEnter(GameObject obj, Collider c);
        void OnTriggerStay(GameObject obj, Collider c);
        void OnTriggerExit(GameObject obj, Collider c);
    }

    /// <summary>
    /// This script simply allows forwarding trigger events for the objects that collide with something. This
    /// allows you to have a generic trigger handler and attach a trigger forwarder to your child objects.
    /// In addition, you also get access to the game object that is colliding, along with the object being
    /// triggered into, which is helpful.
    /// </summary>
    public class TriggerForwardScript : MonoBehaviour
    {
        public ITriggerHandler TriggerHandler;

        public void OnTriggerEnter(Collider col)
        {
            TriggerHandler.OnTriggerEnter(gameObject, col);
        }

        public void OnTriggerStay(Collider col)
        {
            TriggerHandler.OnTriggerStay(gameObject, col);
        }

        public void OnTriggerExit(Collider col)
        {
            TriggerHandler.OnTriggerExit(gameObject, col);
        }
    }
}
