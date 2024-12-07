using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{

    private LineRenderer line;

    private LayerMask layers;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        layers = LayerMask.GetMask("HackWalls");
        line.SetPosition(0, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.up, 1000, layers);
        line.SetPosition(1, transform.position + transform.up * ray.distance);
    }
}
