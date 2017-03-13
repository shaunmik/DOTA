using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour {

        private float StartingHealth = 100f;
        public Image HealthBar;
        private float CurrentHealth; 
        private bool isDead; 

        public bool IsDead { get { return isDead; } }

        //Initialize the variables.
	void Start() {
            isDead = false;
            CurrentHealth = StartingHealth;
            HealthBar.fillAmount = 1f;	
	}
	
        //Damage control.
	public void TakeDamage(int damage) {
            if (IsDead) {
                return;
            }

            // Decrement the current health by the damage but make sure it stays between the min and max.
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, StartingHealth);

            // Set the health bar to show the normalised health amount.
            HealthBar.fillAmount = CurrentHealth / StartingHealth;

            // If the current health is approximately equal to zero
            if (Mathf.Abs(CurrentHealth) < float.Epsilon){
                isDead = true;
                Wait(2);
                GameOver();
            }
		
	}

    public void Wait(float seconds)
    {
        StartCoroutine(_wait(seconds));
    }
    IEnumerator _wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

}
