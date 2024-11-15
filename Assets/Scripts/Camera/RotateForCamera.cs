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

    // Update is called once per frame
    void Update()
    {
        if (transform.position != cam.transform.position) {
            if (Input.GetKey(KeyCode.W)) {
                if (this.transform.rotation.x <= 90) deltaAngleX += thirdPersonSensitivity;
            }
            if (Input.GetKey(KeyCode.S)) {
                if (this.transform.rotation.x >= -45) deltaAngleX -= thirdPersonSensitivity;
            }
            if (Input.GetKey(KeyCode.D)) {
                deltaAngleY -= thirdPersonSensitivity;
            }
            if (Input.GetKey(KeyCode.A)) {
                deltaAngleY += thirdPersonSensitivity;
            }
        }
        else {
            if (Input.GetKey(KeyCode.S)) {
                if (this.transform.rotation.x <= 90) deltaAngleX += firstPersonSensitivity;
            }
            if (Input.GetKey(KeyCode.W)) {
                if (this.transform.rotation.x >= -45) deltaAngleX -= firstPersonSensitivity;
            }
            if (Input.GetKey(KeyCode.A)) {
                deltaAngleY -= firstPersonSensitivity;
            }
            if (Input.GetKey(KeyCode.D)) {
                deltaAngleY += firstPersonSensitivity;
            }
        }

        transform.Rotate(0.0f, deltaAngleY * Time.deltaTime, 0.0f, Space.World);
        transform.Rotate(deltaAngleX * Time.deltaTime, 0.0f, 0.0f, Space.Self);

        deltaAngleY = 0;
        deltaAngleX = 0;
    }
}
