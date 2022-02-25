using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("GameObject References")]
    [SerializeField] GameObject particle;
    [SerializeField] float fireRate;

    float fireDelta;
    bool tryingToFire = false;

    void Awake()
    {
        fireRate = 60 / fireRate;
    }

    void OnFire()
    {
        tryingToFire = !tryingToFire;
        fireDelta = fireRate;
    }

    void FixedUpdate()
    {
        if (tryingToFire && fireDelta >= fireRate)
        {
            Fire();
            fireDelta -= fireRate;
        }
        fireDelta += Time.fixedDeltaTime;
    }
    
    void Fire()
    {
        Instantiate(particle, transform.position, transform.rotation);
    }
}
