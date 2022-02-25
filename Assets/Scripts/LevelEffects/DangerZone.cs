using UnityEngine;

public class DangerZone : TriggerActions
{
    public override void TargetEntered(Collider other)
    {
        other.transform.root.GetComponentInChildren<Damageable>().TriggerDeath();
    }
}
