using UnityEngine;

public class Turret : TriggerActions
{
    [Header("Object References")]
    [SerializeField] Transform emitter;
    [SerializeField] GameObject projectile;

    [Header("Parameters")]
    [SerializeField] float fireRate;
    [SerializeField] int damage;

    bool targetWithinDetectionArea = false;
    bool resting = true;

    float fireDelta;

    void Awake()
    {
        fireRate = 60 / fireRate;
    }

    private void FixedUpdate()
    {
        ResetRotation();
        if (resting) enabled = false;
    }

    public override void TargetEntered(Collider other)
    {
        resting = false;
        fireDelta = fireRate;
        targetWithinDetectionArea = true;
    }

    public override void TargetInside(Collider other)
    {
        if (IsTargetVisible(other))
        {
            LookAtTarget(other.attachedRigidbody.position);
            TryToFire();
        }
    }

    public override void TargetLost()
    {
        targetWithinDetectionArea = false;
    }

    void LookAtTarget(Vector3 target)
    {
        transform.LookAt(target);
    }

    private void TryToFire()
    {
        if (fireDelta >= fireRate)
        {
            Fire();
            fireDelta -= fireRate;
        }
        fireDelta += Time.fixedDeltaTime;
    }

    void Fire()
    {
        Instantiate(projectile, emitter.position, emitter.rotation);
        GameManager.Player.GetComponent<PlayerHealth>().TakeDamage(damage);
    }

    bool IsTargetVisible(Collider target)
    {
        return Physics.Linecast(emitter.transform.position, target.transform.position, out RaycastHit hit, GameManager.RaycastMask) 
            && hit.collider.transform.root.CompareTag(targetTag);
    }

    void ResetRotation()
    {
        if (transform.localRotation != Quaternion.Euler(0, 0, 0))
        {
            if(!targetWithinDetectionArea)
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.Euler(0, 0, 0), 1);
        }
        else resting = true;
    }
}
