using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    Vector3 startPos;
    public GameObject anchorPoint;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);

        // if right mouse held down
        if (Input.GetMouseButton(1))
        {
            // rotate camera
            anchorPoint.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * -1, 0, 0));
        }

        // scroll wheel distance FOV
        Camera.main.fieldOfView += Input.GetAxis("Mouse ScrollWheel") * -5;

        // lock camera's Z rotation to 0 always
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
}
