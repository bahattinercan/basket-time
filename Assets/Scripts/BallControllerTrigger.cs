using UnityEngine;

public class BallControllerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager.instance.currentBasketTransform = transform.parent;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GameManager.instance.canThrow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.instance.canThrow = false;
    }
}