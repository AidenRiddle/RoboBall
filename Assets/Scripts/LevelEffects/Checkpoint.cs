using UnityEngine;

public class Checkpoint : TriggerActions
{
    public override void TargetEntered(Collider other)
    {
        GameObject.Find("GameManager").GetComponent<GameManagerObject>().OnCheckpointReached(transform);
    }
}
