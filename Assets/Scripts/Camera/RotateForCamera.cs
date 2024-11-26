using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateForCamera : MonoBehaviour
{
    private float deltaAngleY = 0;
    private float deltaAngleX = 0;
    public GameObject cam;
    public float firstPersonSensitivity = 150f;
    public float thirdPersonSensitivity = 100f;
    public float minAngle = -45; // Zero = straight forward, Negative = looking up
    public float maxAngle = 45;
    public float angleAdjusted;

    void Start()
    {
        minAngle = AdjustAngle(minAngle);
        maxAngle = AdjustAngle(maxAngle);
    }

    // Update is called once per frame
    void Update()
    {
        angleAdjusted = Quaternion.Angle(transform.rotation, Quaternion.identity) * (transform.rotation.eulerAngles.x > 180 ? -1 : 1);

        if (transform.position != cam.transform.position) {
            if (Input.GetKey(KeyCode.S)) {
                if (angleAdjusted > minAngle) deltaAngleX -= thirdPersonSensitivity;
            }
            if (Input.GetKey(KeyCode.W)) {
                if (angleAdjusted < maxAngle) deltaAngleX += thirdPersonSensitivity;
            }
            if (Input.GetKey(KeyCode.D)) {
                deltaAngleY -= thirdPersonSensitivity;
            }
            if (Input.GetKey(KeyCode.A)) {
                deltaAngleY += thirdPersonSensitivity;
            }
        }
        // else {
        //     if (Input.GetKey(KeyCode.S)) {
        //         if (angleAdjusted < maxAdjusted) deltaAngleX += firstPersonSensitivity;
        //     }
        //     if (Input.GetKey(KeyCode.W)) {
        //         if (angleAdjusted > 0) deltaAngleX -= firstPersonSensitivity;
        //     }
        //     if (Input.GetKey(KeyCode.A)) {
        //         deltaAngleY -= firstPersonSensitivity;
        //     }
        //     if (Input.GetKey(KeyCode.D)) {
        //         deltaAngleY += firstPersonSensitivity;
        //     }
        // }

        transform.Rotate(0.0f, deltaAngleY * Time.deltaTime, 0.0f, Space.World);
        transform.Rotate(deltaAngleX * Time.deltaTime, 0.0f, 0.0f, Space.Self);

        deltaAngleY = 0;
        deltaAngleX = 0;
    }

    float AdjustAngle(float angle) {
        return angle > 180 ? angle - 360 : angle;
    }
}
