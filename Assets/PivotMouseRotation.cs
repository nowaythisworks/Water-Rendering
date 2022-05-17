using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotMouseRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // lock mouse
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // control Y axis with mouse X, and X axis with mouse Y
        transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0));
        // set z rotation to 0
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
}
