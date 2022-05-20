using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // wasd controls
    public float bounceSpeed = 5;
    public float minHeight = -0.5f;
    public float maxHeight = 0.5f;
    public GameObject shipContents;
    public GameObject oceanWater;

    public float maxSpeed = 3;
    public float turnRate = 10;
    private float forwardSpeed;
    private Vector3 startPos;
    private Vector3 startWaterDir;

    void Start()
    {
        startPos = transform.position;
        startWaterDir = oceanWater.GetComponent<Renderer>().material.GetVector("WaterDirection");
    }

    // Update is called once per frame
    void Update()
    {
        // manipulate forwardSpeed with W and S keys, min 0 max 3
        if (Input.GetKeyDown(KeyCode.W))
        {
            forwardSpeed = Mathf.Clamp(forwardSpeed + 1, -1, maxSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            forwardSpeed = Mathf.Clamp(forwardSpeed - 1, -1, maxSpeed);
        }

        // move the ship forward at forwardSpeed
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -turnRate);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * Time.deltaTime * turnRate);
        }

        // simulate water bouyancy by ping ponging the ship up and down randomly and smoothly, starting at the minheight and ending at the maxheight (Lerping)
        float newHeight = Mathf.PingPong(Time.time, maxHeight - minHeight) + minHeight;
        shipContents.transform.position = Vector3.Slerp(shipContents.transform.position, new Vector3(shipContents.transform.position.x, newHeight, shipContents.transform.position.z), Time.deltaTime * bounceSpeed);

        // get the oceanWater shader, set the property "_WaterDirection" to the ship's forward direction
        Vector3 cPos = new Vector2(transform.position.z, -transform.position.x);
        oceanWater.GetComponent<Renderer>().material.SetVector("WaterDirection", cPos / 4);

        // randomly slerp the ship on the Vector3.forward axis, pingponging
    }
}
