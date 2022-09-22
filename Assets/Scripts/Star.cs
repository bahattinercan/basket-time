using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.ball))
        {
            GameManager.instance.AddStar();
            gameObject.SetActive(false);
        }
    }
}
