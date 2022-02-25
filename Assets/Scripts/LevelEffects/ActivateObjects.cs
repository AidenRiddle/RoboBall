using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ActivateObjects : TriggerActions
{
    [SerializeField] List<GameObject> targets;
    public List<GameObject> Targets { get => targets; }

    [Tooltip("Forces all listed targets to be disabled when playmode is activated")]
    [SerializeField] bool forceDisabledOnStartup = false;

    [SerializeField] bool toggleStates = false;

    private void Awake()
    {
        if(forceDisabledOnStartup) foreach (var target in targets) target.SetActive(false);
    }

    public override void TargetEntered(Collider other)
    {
        if (toggleStates) foreach (var target in targets) target.SetActive(!target.activeSelf);

        else foreach (var target in targets) target.SetActive(true);
    }
}

[CustomEditor(typeof(ActivateObjects))]
public class DisplayAffectedGameObjects : Editor
{
    bool drawAffectedObjects = true;

    public override void OnInspectorGUI()
    {
        drawAffectedObjects = GUILayout.Toggle(drawAffectedObjects, "Display Affected Objects In Scene");
        DrawDefaultInspector();
    }

    void OnSceneGUI()
    {
        ActivateObjects myObj = (ActivateObjects) target;
        var targets = myObj.Targets;
        if (!drawAffectedObjects || targets.Count == 0) return;

        Handles.color = Color.red;
        Vector3 center = myObj.transform.position;
        foreach (var target in targets)
        {
            if(target != null)
            Handles.DrawLine(center, target.transform.position);
        }
    }
}
