using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHandler : MonoBehaviour {
    public int monsterLife = 100;

    bool dead = false;

    Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		if (monsterLife <= 0)
        {
            dead = true;
            anim.Play("ghost_die", 0);
            playDead();
        }
	}

    public bool isDead()
    {
        return dead;
    }

    // TODO: special resistance calculation should be done here
    //       add argument for element enum
    public void takeDamage (int damage)
    {
        Debug.Log(damage);
        monsterLife = monsterLife - damage;
    }

    void playDead ()
    {
        StartCoroutine(goDie());
    }

    IEnumerator goDie()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
