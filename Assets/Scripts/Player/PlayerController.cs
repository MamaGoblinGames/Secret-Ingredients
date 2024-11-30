using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private bool isGrounded = false;
    private Rigidbody rb;
    public GameObject cameraTarget;
    public float dCharge;
    public PlayerCharge playerCharge;
    public float timeCharging;
    public float coyoteFrames;
    public float coyoteTimer;
    public Flavor playerFlavor;
    private bool canJump;
    private bool charging;

    // Input
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCharge.chargeLevel = 0;
        playerCharge.canJump = false;
        playerCharge.jumpBarOpacity = 0.35f;
        playerFlavor.Neutralize();
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
    }

    private void OnEnable() {
        player.FindAction("Fire").started += DoCharge;
        player.FindAction("Fire").canceled += DoJump;
        player.FindAction("Pause").started += DoPause;
        player.Enable();
    }

    private void OnDisable() {
        player.FindAction("Fire").started -= DoCharge;
        player.FindAction("Fire").canceled -= DoJump;
        player.FindAction("Pause").started -= DoPause;
        player.Disable();
    }

    // OnCollisionStay is called once per frame for every Collider or Rigidbody that touches another Collider or Rigidbody.
    // NOTE: Collision stay events are not sent for sleeping Rigidbodies.
    //       "sleeping" = stopped moving and is no longer being processed by the Physics Engine.
    void OnCollisionStay(Collision collision) {
        isGrounded = true;
        coyoteTimer = 0;
    }

    void OnCollisionEnter(Collision collision) {
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
            case function.exponential: return Mathf.Min(coeff[0] + Mathf.Pow(coeff[1]*coeff[2],t), max); // Exponential. y = a + bc^t
            case function.powerSeries: return Mathf.Min(coeff[0] + Mathf.Pow(coeff[1]*t,coeff[2]), max); // Power series. y = a + bt^c
            case function.sin: return Mathf.Min(coeff[0] + coeff[1]*Mathf.Sin(coeff[2]*t + coeff[3]), max); // Sine. y = a + bsin(ct)
            default: return Mathf.Min(coeff[0] + coeff[1]*t, max); // Linear. y = a + bt
        }
    }

    void Update()
    {

        // Figure out if jumping is allowed
        canJump = false;
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

        playerCharge.canJump = canJump;

        if (canJump) {
            playerCharge.jumpBarOpacity = 1f;
        } else {
            playerCharge.jumpBarOpacity = 0.35f;
        }

        // Charge up
        if (Time.timeScale != 0 && charging) {
            timeCharging += Time.deltaTime;
            playerCharge.chargeLevel = ChargeFunction(
                timeCharging,
                function.powerSeries,   // <-- This one
                new float[]{200, dCharge, 1.6f},
                playerCharge.maxCharge
            );
        }
    }

    void DoCharge(InputAction.CallbackContext context) {
        charging = true;
    }

    void DoJump(InputAction.CallbackContext context) {
        charging = false;
        if (Time.timeScale != 0) {
            if (canJump) {
                rb.AddForce(cameraTarget.transform.forward * playerCharge.chargeLevel);
                rb.AddTorque(cameraTarget.transform.right * playerCharge.chargeLevel);
                canJump = false;
            }
            timeCharging = 0;
            playerCharge.chargeLevel = 0;
        }
    }

    void DoPause(InputAction.CallbackContext context) {
        if (Time.timeScale == 0) Time.timeScale = 1;
        else Time.timeScale = 0;
        Debug.Log("pause");
    }
}
