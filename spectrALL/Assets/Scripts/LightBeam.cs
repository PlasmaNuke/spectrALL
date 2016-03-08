using UnityEngine;
using System.Collections;

public class LightBeam : MonoBehaviour
{
    int stageTop = 5;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
        if (hit.collider != null)
        {
            Vector3[] positions = new Vector3[2];
            positions[0] = transform.position;
            positions[1] = hit.point;
            lineRenderer.SetPositions(positions);


        }
        else
        {
            Vector3[] positions = new Vector3[2];
            positions[0] = transform.position;
            positions[1] = new Vector3(transform.position.x, stageTop);
            lineRenderer.SetPositions(positions);
        }
    }
}
