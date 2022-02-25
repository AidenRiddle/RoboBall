using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    public PhysicMaterial physMat;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().material = physMat;
        GetComponent<Animator>().updateMode = AnimatorUpdateMode.AnimatePhysics;
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

}
