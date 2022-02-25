using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Damageable
{
    GameManagerObject gameManagerInstance;
    [SerializeField] HealthBar healthBar;

    public int Lives => lives;
    public int HP => healthPoints;
    public int MaxHP => healthConfiguration.MaxHealthPoints;

    protected override void Awake()
    {
        base.Awake();
        gameManagerInstance = GameObject.Find("GameManager").GetComponent<GameManagerObject>();
    }

    protected override void OnDamageTaken()
    {
        healthBar.UpdateHealthBar();
    }

    protected override void OnHealTaken()
    {
        healthBar.UpdateHealthBar();
    }

    protected override void OnLifeLost()
    {
        base.OnLifeLost();
        gameObject.SetActive(false);
        gameManagerInstance.RespawnPlayer();
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        Debug.Log("<color=red>No more lives...</color>");
    }
}
