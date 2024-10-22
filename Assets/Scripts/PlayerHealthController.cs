﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{

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

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

}
