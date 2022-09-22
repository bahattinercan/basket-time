using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithCameraDistance : MonoBehaviour
{
    private float distance=7.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DestroyThis", 1f, 1f);        
    }

    private void DestroyThis()
    {
        if (GameManager.instance.cameraTransform.position.y > transform.position.y + distance)
        {
            Destroy(gameObject);
        }
    }
}
