using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] Transform objectToFollow;

    Vector3 distance;

    private void Awake()
    {
        distance = objectToFollow.localPosition;
    }

    private void Update()
    {
        transform.position = objectToFollow.position - distance;
        objectToFollow.localPosition = distance;
    }
}
