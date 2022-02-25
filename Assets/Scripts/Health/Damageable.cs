using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] protected HealthStats healthConfiguration;
    [SerializeField] bool useDeathAnim = false;
    [SerializeField] Animator deathAnim;

    [Header("Debug Info")]
    [InspectorReadOnly][SerializeField]
    protected int lives;
    public int Lives => lives;

    [InspectorReadOnly][SerializeField]
    protected int healthPoints;
    public int HealthPoints => healthPoints;

    protected virtual void Awake()
    {
        lives = healthConfiguration.Lives;
        healthPoints = healthConfiguration.MaxHealthPoints;
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        OnDamageTaken();
        if (healthPoints <= 0)
        {
            TriggerDeath();
        }
    }

    protected virtual void OnDamageTaken() { }

    public void TakeHeal(int points)
    {
        healthPoints += points;
        OnHealTaken();
        if (healthPoints > healthConfiguration.MaxHealthPoints) healthPoints = healthConfiguration.MaxHealthPoints;
    }

    protected virtual void OnHealTaken() { }

    public void TriggerDeath()
    {
        OnLifeLost();
        if (healthConfiguration.Killable)
        {
            lives--;
            if(lives <= 0)
            {
                OnDeath();
            }
        }
    }

    protected virtual void OnLifeLost()
    {
        TakeHeal(healthConfiguration.MaxHealthPoints);
        if (useDeathAnim && deathAnim != null) deathAnim.SetTrigger("Dead");
    }

    //Only called if Killable is true
    protected virtual void OnDeath()
    {
        Destroy(gameObject);
    }
}
