using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_sprite : MonoBehaviour
{
    private Transform myCameraTransform;

    private void Start()
    {
        myCameraTransform = FindObjectOfType<Camera>().transform;
    }

    private void LateUpdate()
    {
        transform.forward = new Vector3(myCameraTransform.forward.x, transform.forward.y, myCameraTransform.forward.z);
    }
}
