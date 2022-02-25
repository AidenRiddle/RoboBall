using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] BulletStats bulletConfiguration;

    Vector3 birthLocation;
    Rigidbody rb;

    bool active = true;

    private void OnEnable()
    {
        birthLocation = gameObject.transform.position;

        rb = gameObject.GetComponent<Rigidbody>();
        rb.useGravity = false;

        rb.velocity = transform.forward * bulletConfiguration.Speed;
    }

    void FixedUpdate()
    {
        if (!active) return;
        isOutOfView();
    }

    private void isOutOfView()
    {
        if (Vector3.Distance(birthLocation, gameObject.transform.position) > bulletConfiguration.TravelDistance) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Damageable targetHealth))
        {
            targetHealth.TakeDamage(bulletConfiguration.Damage);
        }
        rb.Sleep();
        DestroySelf();
    }

    //AUTOMATICALLY DESTROYS GAMEOBJECT AFTER PARTICLE EFFECT FINISHES
    private void DestroySelf()
    {
        active = false;
        ParticleSystem exp = GetComponent<ParticleSystem>();
        exp.Play();
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        //Destroy(gameObject, 0.25f);
    }
}
