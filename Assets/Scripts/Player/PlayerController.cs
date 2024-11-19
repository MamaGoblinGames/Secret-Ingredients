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
    public PlayerCharge playerCharge;
    public float timeCharging;
    public float coyoteFrames;
    public float coyoteTimer;

    public void Initialize(PlayerCharge playerCharge) {
        this.playerCharge = playerCharge;
        this.playerCharge.chargeLevel = 0;
        maxCharge = playerCharge.maxCharge;
    }

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

    // Charge function enumeration
    private enum function {
        linear, //0
        quadratic, //1
        exponential, //2
        powerSeries, //3
        sin //4
    }

    float ChargeFunction(float t, function func, float[] coeff, float max) {
        switch(func) {
            case function.quadratic: return Mathf.Min(coeff[0] + coeff[1]*t + Mathf.Pow(coeff[2]*t,2f), max); // Quadratic. y = a + bt + ct^2
            break;
            case function.exponential: return Mathf.Min(coeff[0] + Mathf.Pow(coeff[1]*coeff[2],t), max); // Exponential. y = a + bc^t
            break;
            case function.powerSeries: return Mathf.Min(coeff[0] + Mathf.Pow(coeff[1]*t,coeff[2]), max); // Power series. y = a + bt^c
            break;
            case function.sin: return Mathf.Min(coeff[0] + coeff[1]*Mathf.Sin(coeff[2]*t + coeff[3]), max); // Sine. y = a + bsin(ct)
            break;
            default: return Mathf.Min(coeff[0] + coeff[1]*t, max); // Linear. y = a + bt
            break;
        }
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
            timeCharging += Time.deltaTime;
            charge = ChargeFunction(timeCharging,
                                                            function.powerSeries   // <-- This one
            , new float[]{200, dCharge, 1.6f}, maxCharge);
        }
        else if (Input.GetKeyUp(KeyCode.Space)) {
            if (canJump) {
                rb.AddForce(cameraTarget.transform.forward * charge);
                rb.AddTorque(cameraTarget.transform.right * charge);
                canJump = false;
            }
            timeCharging = 0;
            charge = 0;
        }
        playerCharge.chargeLevel = charge;
    }
}
