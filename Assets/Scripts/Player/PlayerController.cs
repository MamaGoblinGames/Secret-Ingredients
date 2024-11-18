using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isGrounded = false;
    private Rigidbody rb;
    public GameObject cameraTarget;
    public float dCharge;
    public float maxCharge;
    public float charge;
    public float coyoteFrames;
    public float coyoteTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // OnCollisionStay is called once per frame for every Collider or Rigidbody that touches another Collider or Rigidbody.
    // NOTE: Collision stay events are not sent for sleeping Rigidbodies.
    //       "sleeping" = stopped moving and is no longer being processed by the Physics Engine.
    void OnCollisionStay(Collision collision) {
        isGrounded = true;
        coyoteTimer = 0;
    }

    void OnCollisionExit(Collision collision) {
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool canJump = false;
        if (isGrounded) {
            coyoteTimer = 0;
            canJump = true;
        }
        else {
            if (coyoteTimer < coyoteFrames) {
                canJump = true;
                coyoteTimer += Time.deltaTime;
            }
        }

        // Jump if applicable
        if (Input.GetKey(KeyCode.Space)) {
            charge += dCharge * Time.deltaTime;
            if (charge > maxCharge) {
                charge = maxCharge;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space)) {
            if (canJump) {
                rb.AddForce(cameraTarget.transform.forward * charge);
                rb.AddTorque(cameraTarget.transform.right * charge);
                canJump = false;
            }
            charge = 0;
        }
    }
}
