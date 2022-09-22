using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNShoot : MonoBehaviour
{
    public float power = 10f;
    public Vector2 minPower, maxPower;
    private Rigidbody2D rb;
    private Camera cam;
    private Vector2 force;
    private Vector3 startPoint, endPoint;
    private TrajectoryLine trajectoryLine;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        trajectoryLine = GetComponent<TrajectoryLine>();
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            startPoint.z = 15;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            currentPoint.z = 15;
            trajectoryLine.RenderLine(startPoint, currentPoint);
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            endPoint.z = 15;

            force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
                Mathf.Clamp(startPoint.y - endPoint.y ,minPower.y, maxPower.y));
            rb.AddForce(force * power, ForceMode2D.Impulse);
            trajectoryLine.EndLine();
            
        }
    }
}
