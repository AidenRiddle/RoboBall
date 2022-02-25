using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public int initialHealth = 100;
    public bool killSelf = false;
    public float killDelay = 0;
    public int lives = 1;

    Animator deathAnim;
    protected int health;
    protected bool alive;

    private void Awake()
    {
        deathAnim = gameObject.GetComponent<Animator>();
        health = initialHealth;
        alive = true;
        if (lives == 0) killSelf = false;
    }

    public void TakeDamage(int dmg)
    {
        if (!alive) return;

        health -= dmg;
        if(health <= 0)
        {
            lives--;
            if (lives == 0) alive = false;
            Kill();
            if (lives < 0) HealUp();
        }
    }

    private void Kill()
    {
        PlayDeathAnimation();
        OnDeath();
        if (killSelf) Destroy(gameObject, killDelay);
    }

    private void PlayDeathAnimation()
    {
        if (deathAnim != null) deathAnim.SetTrigger("Dead");
    }

    protected virtual void OnDeath()
    {
        return;
    }

    void HealUp()
    {
        health = initialHealth;
    }
}
