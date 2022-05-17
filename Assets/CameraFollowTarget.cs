using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public GameObject target;
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        // lerp to target
        transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
