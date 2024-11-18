using System.Collections;
using UnityEngine;


public class FakeTarget : MonoBehaviour
{
    float timer = 0;
    public float gazeTime = 1f;

    public void OnRayCast()
    {
        timer += Time.deltaTime;
        if (timer > gazeTime)
        {
            GazeSuccess();
        }
    }
    void GazeSuccess()
    {
        timer = 0;
    }
}
