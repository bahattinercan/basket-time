using UnityEngine;

public class DragNShoot3 : MonoBehaviour
{
    public float power = 10f;
    public Vector2 minPower, maxPower;
    private Rigidbody rb;
    private Camera cam;
    private Vector2 force;
    private Vector3 startPoint, endPoint;
    private TrajectoryLine trajectoryLine;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        trajectoryLine = GetComponent<TrajectoryLine>();
        cam = Camera.main;
    }

    private void Update()
    {
        ThrowBall();
    }

    private void ThrowBall()
    {
        if (GameManager.instance.canThrow)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                startPoint.z = 15;
                //GameManager.instance.currentBasketTransform.Find("mesh").GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 100);
                /*Transform bottomCollider = GameManager.instance.currentBasketTransform.Find("colliders/bottomCollider");
                bottomCollider.position = new Vector3(bottomCollider.position.x, bottomCollider.position.y - .5f, 0);*/

            }

            if (Input.GetMouseButton(0))
            {
                Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                currentPoint.z = 15;

                /*
                Vector3 lastBasketPos = GameManager.instance.currentBasketTransform.position;
                Vector3 basketLookAtPos = new Vector3((lastBasketPos.x+ (currentPoint.x-startPoint.x)),
                    (lastBasketPos.y+(currentPoint.y-startPoint.y)),
                    15);
                */
                trajectoryLine.RenderLine(startPoint, currentPoint);

                //Debug.DrawLine(lastBasketPos, basketLookAtPos, Color.red, 2f);
                //GameManager.instance.currentBasketTransform.LookAt(basketLookAtPos, new Vector3(0,0,1));
            }

            if (Input.GetMouseButtonUp(0))
            {
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 15;

                force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
                    Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));

                rb.AddForce(force * power, ForceMode.Impulse);
                //GameManager.instance.currentBasketTransform.Find("mesh").GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, 0);

                //transform.position = new Vector3(transform.position.x, transform.position.y + .5f, 0);
                Invoke("BottomColliderUp", 1f);

                trajectoryLine.EndLine();
                //GameManager.instance.canThrow = false;
            }
        }
    }

    private void BottomColliderUp()
    {
        //Transform bottomCollider = GameManager.instance.currentBasketTransform.Find("colliders/bottomCollider");
        //bottomCollider.position = new Vector3(bottomCollider.position.x, bottomCollider.position.y + .6f, 0);
    }
}