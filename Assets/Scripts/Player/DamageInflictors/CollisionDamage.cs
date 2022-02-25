using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollisionDamage : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] int impactDamage = 50;
    [Tooltip("Minimum Impulse Magnitude required before dealing damage")]
    [SerializeField] float threshold = 600;

    [Header("Debug Info")]
    [InspectorReadOnly]
    [SerializeField]
    Collider attackingCollider;

    [InspectorReadOnly]
    [SerializeField]
    Collider recievingCollider;

    [InspectorReadOnly]
    [SerializeField]
    float impulse;

    [InspectorReadOnly]
    [SerializeField]
    int damage;

    private void OnCollisionEnter(Collision collision)
    {
        attackingCollider = collision.collider;
        recievingCollider = collision.GetContact(0).thisCollider;
        impulse = Mathf.Abs(collision.impulse.magnitude) - threshold;
        if(impulse >= 0)
        {
            damage = impactDamage;
            playerHealth.TakeDamage(damage);
        }
        else
        {
            damage = 0;
        }
    }
}
