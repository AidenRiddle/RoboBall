using UnityEngine;

public class WindCaster : TriggerActions
{
    [SerializeField] float windForce = 1200;
    [Tooltip("In Seconds")]
    [SerializeField] float upTime = 1;
    [Tooltip("In Seconds")]
    [SerializeField] float downTime = 0.5f;

    private bool isBlowing = true;
    public bool IsBlowing => isBlowing;
    bool isSafe => counter == 0 || counter == 3 || counter == 4 ? true : false;
    float timer = 0;

    int counter = 0;

    bool affectTarget { get => IsBlowing && !isSafe; }

    private void Update()
    {
        timer += Time.deltaTime;
        if (IsBlowing)
        {
            if (timer >= upTime)
            {
                timer = 0;
                isBlowing = false;
            }
        }else{
            if (timer >= downTime)
            {
                timer = 0;
                isBlowing = true;
            }
        }
    }

    public override void TargetEntered(Collider other)
    {
        counter++;
    }

    public override void TargetInside(Collider other)
    {
        if (affectTarget)
        {
            other.attachedRigidbody.AddForce(transform.forward * windForce, ForceMode.Force);
        }
    }

    public override void TargetLost()
    {
        counter--;
    }

}
