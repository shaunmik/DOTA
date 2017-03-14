using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{
    /// <summary>
    /// Handle collision of a fire projectile
    /// </summary>
    /// <param name="script">Script</param>
    /// <param name="pos">Position</param>
    public delegate void FireProjectileCollisionDelegate(FireProjectileScript script, Vector3 pos);

    /// <summary>
    /// This script handles a projectile such as a fire ball
    /// </summary>
    public class FireProjectileScript : FireBaseScript, ICollisionHandler
    {
        [Tooltip("The collider object to use for collision and physics.")]
        public GameObject ProjectileColliderObject;

        [Tooltip("The sound to play upon collision.")]
        public AudioSource ProjectileCollisionSound;

        [Tooltip("The particle system to play upon collision.")]
        public ParticleSystem ProjectileExplosionParticleSystem;

        [Tooltip("The radius of the explosion upon collision.")]
        public float ProjectileExplosionRadius = 50.0f;

        [Tooltip("The force of the explosion upon collision.")]
        public float ProjectileExplosionForce = 50.0f;

        [Tooltip("An optional delay before the collider is sent off, in case the effect has a pre fire animation.")]
        public float ProjectileColliderDelay = 0.0f;

        [Tooltip("The speed of the collider.")]
        public float ProjectileColliderSpeed = 450.0f;

        [Tooltip("The direction that the collider will go. For example, flame strike goes down, and fireball goes forward.")]
        public Vector3 ProjectileDirection = Vector3.forward;

        [Tooltip("What layers the collider can collide with.")]
        public LayerMask ProjectileCollisionLayers = Physics.AllLayers;

        [Tooltip("Particle systems to destroy upon collision.")]
        public ParticleSystem[] ProjectileDestroyParticleSystemsOnCollision;

        [Tooltip("The damage of this projectile")]
        public int bulletDamage = 100;

        [HideInInspector]
        public FireProjectileCollisionDelegate CollisionDelegate;

        private bool collided;
        
        private FireLevelController fireLevelController;
    	private WaterLevelController waterLevelController;
    	private EarthLevelController earthLevelController;
    	private WindLevelController windLevelController;

        private IEnumerator SendCollisionAfterDelay()
        {
            yield return new WaitForSeconds(ProjectileColliderDelay);

            Vector3 dir = ProjectileDirection * ProjectileColliderSpeed;
            dir = ProjectileColliderObject.transform.rotation * dir;
            ProjectileColliderObject.GetComponent<Rigidbody>().velocity = dir;
        }

        protected override void Start()
        {
            base.Start();

            StartCoroutine(SendCollisionAfterDelay());
            
            //playerScore = FindObjectOfType<PlayerScoreController>();
            fireLevelController = FindObjectOfType<FireLevelController>();
            waterLevelController = FindObjectOfType<WaterLevelController>();
            earthLevelController = FindObjectOfType<EarthLevelController>();
            windLevelController = FindObjectOfType<WindLevelController>();
        }

        public void HandleCollision(GameObject obj, Collision c)
        {
            
            if (collided)
            {
                // already collided, don't do anything
                return;
            }

            // stop the projectile
            collided = true;
            Stop();

            // destroy particle systems after a slight delay
            if (ProjectileDestroyParticleSystemsOnCollision != null)
            {
                foreach (ParticleSystem p in ProjectileDestroyParticleSystemsOnCollision)
                {
                    GameObject.Destroy(p);
                }
            }

            // play collision sound
            if (ProjectileCollisionSound != null)
            {
                ProjectileCollisionSound.Play();
            }

            if (c.collider.tag.Equals("Monster"))
            {
                // destroy the monster
                //Destroy(c.collider.gameObject);
                Elements.elemEnum firstElement = Elements.elemEnum.none;
                Elements.elemEnum secondElement = Elements.elemEnum.none;
                if (obj.name == "WindboltCollider")
                    firstElement = Elements.elemEnum.wind;
                else if (obj.name == "FireboltCollider")
                    firstElement = Elements.elemEnum.fire;
                else if (obj.name == "EarthboltCollider")
                    firstElement = Elements.elemEnum.earth;
                else if (obj.name == "WaterboltCollider")
                    firstElement = Elements.elemEnum.water;


                Monster monster = c.collider.gameObject.GetComponent<Monster>();
                monster.takeDamage(bulletDamage, firstElement,secondElement);

                // increament the level of all elements
                fireLevelController.IncrementElement(1);
                waterLevelController.IncrementElement(1);
                earthLevelController.IncrementElement(1);
                windLevelController.IncrementElement(1);
            }

            // if we have contacts, play the collision particle system and call the delegate
            if (c.contacts.Length != 0)
            {
                ProjectileExplosionParticleSystem.transform.position = c.contacts[0].point;
                ProjectileExplosionParticleSystem.Play();
                FireBaseScript.CreateExplosion(c.contacts[0].point, ProjectileExplosionRadius, ProjectileExplosionForce);
                if (CollisionDelegate != null)
                {
                    CollisionDelegate(this, c.contacts[0].point);
                }
            }
        }
    }
}
