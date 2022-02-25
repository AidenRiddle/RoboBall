using UnityEngine;

public class TriggerActions : MonoBehaviour
{
    [SerializeField]
    protected string targetTag;
    public string TargetTag { get => targetTag; }

    public virtual void TargetEntered(Collider other){ return; }

    public virtual void TargetInside(Collider other){ return; }

    public virtual void TargetLost(){ return; }
}
