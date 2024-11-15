using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOffset : MonoBehaviour
{
    public Transform target;
    public float offset;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (transform.position != target.position) {
                transform.position = target.position;
            }
            else {
                transform.Translate(0, 0, offset);
            }
        }
    }
}
