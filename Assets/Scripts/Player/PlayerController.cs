using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canJump = false;
    private Rigidbody rb;
    public GameObject cameraTarget;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionExit(Collision collision) {
        canJump = false;
    }

    void OnCollisionStay(Collision collision) {
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (canJump) {
                rb.AddForce(cameraTarget.transform.forward * 1000);
                rb.AddTorque(cameraTarget.transform.right * 1000);
            }
        }
    }
}
