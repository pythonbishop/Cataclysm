using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLineEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public GameObject rocket;
    LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rocket && target)
        {
        Vector3[] positions = {target.transform.position, rocket.transform.position};
        if (lineRenderer)
        {
            lineRenderer.SetPositions(positions);
        }
        }
    }
}
