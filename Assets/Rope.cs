using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject end;

    // Update is called once per frame
    void Update()
    {
        // set the linerenderer's 0 to this position, and 1 to the end position
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, end.transform.position);
    }
}
