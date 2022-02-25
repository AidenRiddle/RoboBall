using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CharacterMovement : MonoBehaviour
{
    #region Serialized Fields

    [Header("GameObject References")]
    [SerializeField] GameObject yawController;

    [SerializeField] GameObject tiltController;

    [SerializeField] GameObject head;

    [SerializeField] GameObject body;

    [SerializeField] GameObject cam;

    [SerializeField] Rigidbody rigidBody;

    [Header("Physics")]
    [SerializeField] float torque = 30;

    [SerializeField] float maxSpeed = 15;

    [SerializeField] float slopeMultiplier = 1;

    [SerializeField] float gravityMultiplier = 1;

    [Tooltip("Max distance the ray will travel to check for a floor under the character")]
    [SerializeField] float floorCheckDistance = 0.2f;

    [Header("Animation Parameters")]
    [SerializeField] float tiltMultiplier = 1;

    [SerializeField] float yawLerpSpeed;

    [SerializeField] float tiltLerpSpeed;

    #endregion

    #region Global Variables

    Rigidbody ball;
    Quaternion neckRotation;
    float decelerationMultiplier;
    float forwardInput;
    float rightInput;

    #endregion

    #region Getters

    public bool IsFalling
    {
        get
        {
            return !Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, floorCheckDistance, GameManager.RaycastMask);
        }
    }

    public Vector3 Velocity
    {
        get
        {
            return ball.velocity;
        }
    }

    #endregion

    void Awake()
    {
        ball = rigidBody;
        ball.maxAngularVelocity = Mathf.Infinity; //uncaps the max angular velocity (locked at 7 by default);
        ball.centerOfMass = body.transform.localPosition;

        neckRotation = yawController.transform.rotation;
        decelerationMultiplier = torque / maxSpeed;
    }

    private void OnDisable()
    {
        ball.angularVelocity = Vector3.zero;
        ball.velocity = Vector3.zero;
    }

    void OnMove(InputValue value) //Called by Unity InputSystem
    {
        forwardInput = value.Get<Vector2>().y;
        rightInput = value.Get<Vector2>().x;
    }

    void FixedUpdate()
    {
        UpdateForces();
        UpdateNeckRotation();
        UpdateHeadRotation();
    }

    Vector3 SlopeDirection()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, out hit, floorCheckDistance, GameManager.RaycastMask))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red, 2);
            Vector3 scaledNormal = hit.normal;

            return new Vector3(scaledNormal.x, 0, scaledNormal.z);
        }
        return Vector3.zero;
    }

    void UpdateForces()
    {
        Vector3 inputTorque;
        var inputDirection = Vector3.Normalize(forwardInput * cam.transform.right + rightInput * cam.transform.forward * -1);

        if (inputDirection == Vector3.zero)
        {
            inputTorque = -ball.angularVelocity;
        }
        else
        {
            inputTorque = (inputDirection * torque) - (ball.angularVelocity * decelerationMultiplier);
        }

        Vector3 totalSlopeTorque = Quaternion.Euler(0, 90, 0) * (SlopeDirection() * slopeMultiplier); //Quaternion required for Torque, not Force!

        //Force from Slope
        ball.AddTorque(totalSlopeTorque);

        //Force from Input
        ball.AddTorque(inputTorque, ForceMode.Acceleration);

        //Force of Gravity
        ball.AddForce(GameManager.Gravity * ball.mass * Vector3.down * gravityMultiplier);
    }

    //Points the head towards the location targeted by the crosshair
    void UpdateHeadRotation()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 500f, GameManager.RaycastMask))
            head.transform.LookAt(hit.point, Vector3.up);
        else head.transform.LookAt(cam.transform.forward * 500f + cam.transform.position, Vector3.up);
    }

    void UpdateNeckRotation()
    {
        Quaternion normalizer = Quaternion.Euler(0, cam.transform.localEulerAngles.y, 0) * neckRotation;
        Quaternion finalTilt = Quaternion.Euler(forwardInput * tiltMultiplier, 0, rightInput * -1 * tiltMultiplier);

        Vector3 neckYaw = Quaternion.RotateTowards(yawController.transform.rotation, normalizer, yawLerpSpeed).eulerAngles;
        Vector3 neckTilt = Quaternion.RotateTowards(tiltController.transform.localRotation, finalTilt, tiltLerpSpeed).eulerAngles;

        yawController.transform.eulerAngles = new Vector3(0, neckYaw.y, 0);
        tiltController.transform.localEulerAngles = new Vector3(neckTilt.x, 0, neckTilt.z);
    }
}
