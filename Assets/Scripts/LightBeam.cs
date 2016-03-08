using UnityEngine;
using System.Collections;

public class LightBeam : MonoBehaviour
{
    int stageTop = 5;
    LineRenderer reflected = null;

    // Update is called once per frame
    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
        if (hit.collider != null)
        {
            Debug.DrawRay(hit.point, hit.normal, Color.green);
            Vector3[] positions = new Vector3[2];
            positions[0] = transform.position;
            positions[1] = hit.point;
            lineRenderer.SetPositions(positions);

            switch (hit.collider.gameObject.GetComponent<Prism>().lightEffect)
            {
                case LightEffectType.Reflect:
                    if (reflected == null)
                    {
                        reflected = CreateNewLine(hit, 0.1f, 0.1f);
                        reflected.material = lineRenderer.material;
                    }

                    Vector3[] reflectedPositions = new Vector3[2];
                    reflectedPositions[0] = hit.point;
                    incidenceAngle = Vector2.Angle(hit.point, hit.normal);
                    reflectedPositions[1] = new Vector2(hit.point.x * Mathf.Tan(Mathf.Deg2Rad * incidenceAngle), hit.point.y * Mathf.Tan(Mathf.Deg2Rad * incidenceAngle)) * 10f;
                    reflected.SetPositions(reflectedPositions);
                    break;
                case LightEffectType.Refract:
                    if (reflected == null)
                    {
                        reflected = CreateNewLine(hit, 0.1f, 1f);
                        reflected.material = lineRenderer.material;
                    }
                    Vector3[] refractedPositions = new Vector3[2];
                    refractedPositions[0] = hit.point;
                    incidenceAngle = Vector2.Angle(hit.normal, positions[1] - positions[0]);
                    refractedPositions[1] = new Vector2(hit.point.x * Mathf.Tan(Mathf.Deg2Rad * incidenceAngle), hit.point.y * Mathf.Tan(Mathf.Deg2Rad * incidenceAngle)) * 10f;
                    reflected.SetPositions(refractedPositions);
                    break;
                case LightEffectType.Block:
                default:
                    if (reflected != null)
                    {
                        Destroy(reflected.gameObject);
                    }
                    break;
            }
        }
        else
        {
            Vector3[] positions = new Vector3[2];
            positions[0] = transform.position;
            positions[1] = new Vector3(transform.position.x, stageTop);
            lineRenderer.SetPositions(positions);

            if (reflected != null)
            {
                Destroy(reflected.gameObject);
            }
        }
    }
    public float incidenceAngle;

    private LineRenderer CreateNewLine(RaycastHit2D hit, float startWidth, float endWidth)
    {
        var lineObject = new GameObject("LightBeam2");
        lineObject.transform.position = hit.point;
        var line = lineObject.AddComponent<LineRenderer>();
        line.SetWidth(startWidth, endWidth);
        return line;
    }
}
