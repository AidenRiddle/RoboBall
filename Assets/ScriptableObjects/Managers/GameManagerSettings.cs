using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "GameManagerSettings", menuName = "Game Manager Settings")]
public class GameManagerSettings : ScriptableObject
{
    #region User Settings

    [Header("User Settings")]
    [Range(0f, 5f)]
    [SerializeField] private float sensitivity = 0.3f;
    public float Sensitivity => sensitivity;

    #endregion

    #region Physics Settings

    [Header("Physics Settings")]
    [Tooltip("Number of physics updates every second")]
    [Range(50, 120)]
    [SerializeField] int physicsUpdatesPerSecond = 50;
    public int PhysicsUpdatesPerSecond => physicsUpdatesPerSecond;

    [Space(10)]

    [SerializeField] float gravity = 9.81f;
    public float Gravity => gravity;

    [SerializeField] LayerMask raycastMask;
    public LayerMask RaycastMask => raycastMask;

    #endregion

    #region Graphics Settings

    [Header("Graphics Settings")]
    [Tooltip("Updates per second")]
    [SerializeField] float probeRefreshRate = 60;
    public float ProbeRefreshRate => 1 / probeRefreshRate;

    #endregion

    #region References

    [Header("References")]
    [SerializeField] InputActionAsset inputActionBindings;
    public InputActionAsset InputActionBindings => inputActionBindings;

    #endregion

    private void OnValidate()
    {
        ApplyPhysicsTimeStep();
        ApplyMouseSensitivity();
    }

    void ApplyPhysicsTimeStep()
    {
        Time.fixedDeltaTime = (float)1 / physicsUpdatesPerSecond;
    }

    void ApplyMouseSensitivity()
    {
        //Finds the action associated with Mouse Delta. Used to adjust the Sensitivity
        InputAction lookAction = inputActionBindings.FindAction("Look");
        int indexOfMouseBinding = 0; //Hardcoded. Need to generalize

        //The scale for Mouse Delta is extremely high by default. This value is used to decrease it to a more common value
        float mouseDeltaInputFactor = 0.005f;

        //Creates a copy of the binding because the original cannot be directly changed, and applies the player specific sensitivity
        var binding = lookAction.bindings[indexOfMouseBinding];
        var newProcessor = "ScaleVector2(x = " + (Sensitivity * mouseDeltaInputFactor) + ", y = " + (Sensitivity * mouseDeltaInputFactor) + ")";
        binding.processors = newProcessor;

        //Updates the binding with the newly created Processor
        lookAction.ChangeBinding(indexOfMouseBinding).To(binding);
    }
}
