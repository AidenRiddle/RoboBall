using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ImpactObject : MonoBehaviour
{
    [SerializeField] float waitTime = 1f;
    [SerializeField] bool pauseGameBeforeAttack = true;
    [SerializeField] float attackForce = 10f;
    [SerializeField] bool debug = false;

    bool completed = false;

    void Update()
    {
        if(!completed && Time.timeSinceLevelLoad >= waitTime)
        {
            completed = true;
            if(pauseGameBeforeAttack) Debug.Break();
            Attack();
        }
    }

    void Attack()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -attackForce));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(debug) Debug.Log(collision.collider);
    }
}
