using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeWall : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
        }
    }
}
