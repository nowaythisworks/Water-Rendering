using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // wasd controls
    public float speed = 1;
    public float minHeight = -0.5f;
    public float maxHeight = 0.5f;
    public GameObject shipContents;

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        shipContents.transform.Translate(x, 0, z);

        // simulate water bouyancy by ping ponging the ship up and down randomly and smoothly, clamped at the min and max height
        float y = Mathf.PingPong(Time.time, 1) * 0.1f;
        shipContents.transform.position = Vector3.Lerp(new Vector3(shipContents.transform.position.x, minHeight, shipContents.transform.position.z), new Vector3(shipContents.transform.position.x, maxHeight, shipContents.transform.position.z), y * Time.deltaTime);
    }
}
