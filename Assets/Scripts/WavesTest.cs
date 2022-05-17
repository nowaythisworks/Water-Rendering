using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesTest : MonoBehaviour
{
    public float waterSpeed = 1;
    public GameObject ship; // the ship's theoretical position    private float shipXOffset;
    private float shipXOffset;
    private float shipZOffset;

    public float waterOffsetAdjustmentSpeed;
    private Vector3 startPosition;

    Mesh mesh;
    Vector3[] verts;

    FastNoiseLite noise = new FastNoiseLite();

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        mesh = GetComponent<MeshFilter>().mesh;
        verts = mesh.vertices;
        
        noise.SetNoiseType(FastNoiseLite.NoiseType.Cellular);
        noise.SetFractalType(FastNoiseLite.FractalType.PingPong);
        noise.SetFrequency(100f);
        noise.SetFractalOctaves(5);
        noise.SetFractalLacunarity(2.00f);

    }

    // Update is called once per frame
    void Update()
    {
        shipXOffset = ship.transform.position.x - startPosition.x;
        shipZOffset = ship.transform.position.z - startPosition.z;

        float timeModifier = Time.time * 0.001f * waterSpeed;

        float midHeight = 0;
        float leftHeight = 0;
        float rightHeight = 0;
        float frontHeight = 0;
        float rearHeight = 0;

        for (int i = 0; i < verts.Length; i++)
        {
            float x = verts[i].x;
            float y = verts[i].y;
            float z = noise.GetNoise(x + timeModifier + (startPosition.x * transform.localScale.x) + (shipXOffset * waterOffsetAdjustmentSpeed),
             y + timeModifier + (startPosition.z * transform.localScale.x) - (shipZOffset * waterOffsetAdjustmentSpeed));
            verts[i] = new Vector3(x, y, z);

            if (i == verts.Length / 2)
            {
                midHeight = z;
                ship.transform.position = Vector3.Lerp(ship.transform.position, new Vector3(ship.transform.position.x, (z * -0.5f), ship.transform.position.z), 1 * Time.deltaTime);
            }
            else if (i == (verts.Length/2) - 1)
            {
                leftHeight = z;
            }
            else if (i == (verts.Length/2) + 1)
            {
                rightHeight = z;
            }
            else if (i == ((verts.Length/2) + (verts.Length/6)))
            {
                frontHeight = z;
            }
            else if (i == ((verts.Length/2) - (verts.Length/6)))
            {
                rearHeight = z;
            }
        }

        float rX = 0;
        float rF = 0;

        if (leftHeight < midHeight)
        {
            // tilt ship to the left Lerp
            rX = -5;
        }
        else if (rightHeight < midHeight)
        {
            // tilt ship to the right Lerp
            rX = 5;
        }

        if (frontHeight < midHeight)
        {
            // tilt ship forward Lerp
            rF = -5;
        }
        else if (rearHeight < midHeight)
        {
            // tilt ship forward Lerp
            rF = 5;
        }

        ship.transform.rotation = Quaternion.Lerp(ship.transform.rotation, Quaternion.Euler(0, rF, rX), 0.5f * Time.deltaTime);

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(shipZOffset * 0.1f, -shipXOffset * 0.1f);

        mesh.vertices = verts;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        transform.position = new Vector3(ship.transform.position.x, transform.position.y, ship.transform.position.z);
    }
}
