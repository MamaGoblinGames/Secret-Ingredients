using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canJump = false;
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

    void OnCollisionStay(Collision collision) {
        canJump = true;
        coyoteTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Update coyote frames
        if (coyoteTimer >= coyoteFrames) {
            canJump = false;
        }
        coyoteTimer += Time.deltaTime;

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
