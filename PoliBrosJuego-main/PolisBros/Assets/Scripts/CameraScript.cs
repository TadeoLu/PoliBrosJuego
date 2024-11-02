using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform John;

    void Update()
    {
        if (John != null)
        {
            Vector3 position = transform.position;
            position.x = John.position.x;
            position.y = John.position.y;
            transform.position = position;
        }
    }
}
