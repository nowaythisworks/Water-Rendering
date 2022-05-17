using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject target;
    public float speed = 1f;
    public bool followRotation = true;
    public bool followYPosition = true;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = target.transform.position;
        if (!followYPosition)
        {
            targetPos.y = transform.position.y;
        }
        // lerp to targets
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
        // rotate to target lerp
        if (followRotation) transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, speed * Time.deltaTime);
    }
}
