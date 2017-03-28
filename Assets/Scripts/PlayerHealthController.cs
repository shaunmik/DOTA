using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObject;
    SteamVR_Controller.Device device1;
    SteamVR_Controller.Device device2;

    private float StartingHealth = 100f;
    public Image HealthBar;
    public Image PainEffect;

    private float CurrentHealth;
    private bool isDead;

    public bool IsDead { get { return isDead; } }

    private DamageEffect dmgEffect;

    //Initialize the variables.
    void Start()
    {
        device1 = SteamVR_Controller.Input(3);
        device2 = SteamVR_Controller.Input(4);

        isDead = false;
        CurrentHealth = StartingHealth;
        HealthBar.fillAmount = 1f;

        dmgEffect = PainEffect.GetComponent<DamageEffect>();
    }

    //Damage control.
    public void TakeDamage(int damage)
    {
        if (IsDead)
        {
            return;
        }

        dmgEffect.FadeIn();
        if (device1 == null)
        {
            Debug.Log("device 1 is missing");
        }

        if (device2 == null)
        {
            Debug.Log("device 2 is missing");
        }

        rumbleController();


        // Decrement the current health by the damage but make sure it stays between the min and max.
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, StartingHealth);

        // Set the health bar to show the normalised health amount.
        HealthBar.fillAmount = CurrentHealth / StartingHealth;

        // If the current health is approximately equal to zero
        if (Mathf.Abs(CurrentHealth) < float.Epsilon)
        {
            isDead = true;
            Wait(2);
        }

    }



    public void Wait(float seconds)
    {
        StartCoroutine(_wait(seconds));
    }
    IEnumerator _wait(float time)
    {
        yield return new WaitForSeconds(time);
        GameOver();
    }

    void rumbleController()
    {
        StartCoroutine(LongVibration(1, 3999));
    }

    IEnumerator LongVibration(float length, float strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            device1.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            device2.TriggerHapticPulse((ushort)Mathf.Lerp(0, 3999, strength));
            yield return null;
        }
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

}
