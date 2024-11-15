using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    public Transform target;
    public Vector3 offPosition;
    public Quaternion offRotation;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (transform.position != target.position) {
                transform.position = target.position;
            }
            else {
                // Formula to combine transforms
                transform.position = target.position + offPosition;
                transform.rotation = target.rotation * offRotation;
            }
        }
    }
}
