using UnityEngine;
using System.Collections;
using System;

namespace DigitalRuby.PyroParticles
{
    /// <summary>
    /// Provides an easy wrapper to looping audio sources with nice transitions for volume when starting and stopping
    /// </summary>
    public class LoopingAudioSource
    {
        public AudioSource AudioSource { get; private set; }
        public float TargetVolume { get; private set; }

        private float startMultiplier;
        private float stopMultiplier;
        private float currentMultiplier;

        public LoopingAudioSource(MonoBehaviour script, AudioSource audioSource, float startMultiplier, float stopMultiplier)
        {
            AudioSource = audioSource;
            if (audioSource != null)
            {
                AudioSource.loop = true;
                AudioSource.volume = 0.0f;
                AudioSource.Stop();
            }

            TargetVolume = 1.0f;

            this.startMultiplier = currentMultiplier = startMultiplier;
            this.stopMultiplier = stopMultiplier;
        }

        public void Play()
        {
            Play(TargetVolume);
        }

        public void Play(float targetVolume)
        {
            if (AudioSource != null && !AudioSource.isPlaying)
            {
                AudioSource.volume = 0.0f;
                AudioSource.Play();
                currentMultiplier = startMultiplier;
            }
            TargetVolume = targetVolume;
        }

        public void Stop()
        {
            if (AudioSource != null && AudioSource.isPlaying)
            {
                TargetVolume = 0.0f;
                currentMultiplier = stopMultiplier;
            }
        }

        public void Update()
        {
            if (AudioSource != null && AudioSource.isPlaying &&
                (AudioSource.volume = Mathf.Lerp(AudioSource.volume, TargetVolume, Time.deltaTime / currentMultiplier)) == 0.0f)
            {
                AudioSource.Stop();
            }
        }
    }

    /// <summary>
    /// Script for objects such as wall of fire that never expire unless manually stopped
    /// </summary>
    public class FireConstantBaseScript : FireBaseScript, ITriggerHandler
    {

        [HideInInspector]
        public LoopingAudioSource LoopingAudioSource;

        [Tooltip("Damage of this spell.")]
        public int dotDamage = 50;
        [Tooltip("Score gained on every enemy hit.")]
        public int dotScore = 1; // todo: this probablly need revisit for valance adjustment

        [Tooltip("Fire element gained on every enemy hit.")]
        public int dotFireGain = 0;
        [Tooltip("Water element gained on every enemy hit.")]
        public int dotWaterGain = 0;
        [Tooltip("Earth element gained on every enemy hit.")]
        public int dotEarthGain = 0;
        [Tooltip("Wind element gained on every enemy hit.")]
        public int dotWindGain = 0;



        [Tooltip("how often should the damage apply in Seconds.")]
        public float dotTickRateSeconds = 0.5f; // Warning: if this isn't frequient enough, and collider box of spell is small, monster may not get damaged at all



        private GameManager gameManager;
        //private PlayerScoreController playerScore;
        private FireLevelController fireLevelController;
        private WaterLevelController waterLevelController;
        private EarthLevelController earthLevelController;
        private WindLevelController windLevelController;

        private float nextTickTimer;

        protected override void Awake()
        {
            base.Awake();
            
            LoopingAudioSource = new LoopingAudioSource(this, AudioSource, StartTime, StopTime);
        }

        protected override void Update()
        {
            base.Update();

            LoopingAudioSource.Update();
        }

        protected override void Start()
        {
            base.Start();

            LoopingAudioSource.Play();

            gameManager = FindObjectOfType<GameManager>();
            //playerScore = FindObjectOfType<PlayerScoreController>();
            fireLevelController = FindObjectOfType<FireLevelController>();
            waterLevelController = FindObjectOfType<WaterLevelController>();
            earthLevelController = FindObjectOfType<EarthLevelController>();
            windLevelController = FindObjectOfType<WindLevelController>();

            nextTickTimer = Time.time;
        }

        public override void Stop()
        {
            LoopingAudioSource.Stop();

            base.Stop();
        }

        void ITriggerHandler.OnTriggerEnter(GameObject obj, Collider c)
        {
        }

        void ITriggerHandler.OnTriggerStay(GameObject obj, Collider c)
        {
            if (Time.time > nextTickTimer)
            {
                Debug.Log("[FireConstantBaseScript.OnTriggerStay] GameObject: " + obj + " | Collision: " + c);

                if (c.tag.Equals("Monster"))
                {
                    // damage the monster
                    Monster monster = c.gameObject.GetComponent<Monster>();
                    monster.takeDamage(dotDamage);
                    
                    // increament the score
                    //gameManager.addScore(dotScore);

                    // increament the level of all elements
                    fireLevelController.IncrementElement(dotFireGain);
                    waterLevelController.IncrementElement(dotWaterGain);
                    earthLevelController.IncrementElement(dotEarthGain);
                    windLevelController.IncrementElement(dotWindGain);
                }

                nextTickTimer = Time.time + dotTickRateSeconds;
            }
        }

        void ITriggerHandler.OnTriggerExit(GameObject obj, Collider c)
        {
        }
    }
}