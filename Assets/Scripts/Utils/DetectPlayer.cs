using UnityEngine;
using UnityEditor;

public class DetectPlayer : MonoBehaviour
{
    TriggerActions actor;
    string targetTag;

    private void Awake()
    {
        actor = transform.root.GetComponentInChildren<TriggerActions>();
        targetTag = actor.TargetTag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (VerifyObject(other)) actor.TargetEntered(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (VerifyObject(other)) actor.TargetInside(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (VerifyObject(other)) actor.TargetLost();
    }

    bool VerifyObject(Collider other)
    {
        return other.transform.root.CompareTag(targetTag);
    }
}

[CustomEditor(typeof(DetectPlayer))]
public class DisplayDetectionArea : Editor
{
    bool drawAffectedObjects = false;

    public override void OnInspectorGUI()
    {
        drawAffectedObjects = GUILayout.Toggle(drawAffectedObjects, "Display Detection Area");
        DrawDefaultInspector();
    }

    void OnSceneGUI()
    {
        ((DetectPlayer)target).GetComponent<MeshRenderer>().enabled = drawAffectedObjects;
    }
}
