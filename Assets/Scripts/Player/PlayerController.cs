using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isGrounded = false;
    private Rigidbody rb;
    [Header("Configurable Fields")]
    [Tooltip("The original flavor of the player.")]
    public Flavor originalFlavor;
    public PlayerCharge originalCharge;
    public GameObject cameraTarget;
    public PlayersInfo playersInfo;
    public float coyoteFrames;
    public float dCharge;

    [Header("Realtime, computed values.")]
    [Tooltip("The current realtime flavor of the player.")]
    public Flavor currentFlavor;
    public FlavorHolder flavorHolder;
    public PlayerCharge currentCharge;
    public float timeCharging;
    public float coyoteTimer;
    public int playerNumber;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerInfo playerInfo = playersInfo.RegisterPlayer();
        currentFlavor = playerInfo.flavor;
        flavorHolder = GetComponent<FlavorHolder>();
        flavorHolder.flavor = currentFlavor;
        currentCharge = playerInfo.charge;
        playerNumber = playerInfo.playerNumber;
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
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (Time.timeScale != 0) {
                Time.timeScale = 0;
            }
            else {
                Time.timeScale = 1;
            }
        }

        // Figure out if jumping is allowed
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
        if (Input.GetMouseButton(0) /*Left button*/ && Time.timeScale != 0) {
            timeCharging += Time.deltaTime;
            currentCharge.chargeLevel = ChargeFunction(
                timeCharging,
                function.powerSeries,   // <-- This one
                new float[]{200, dCharge, 1.6f},
                PlayerCharge.Max
            );
        }
        else if (Input.GetMouseButtonUp(0) && Time.timeScale != 0) {
            if (canJump) {
                rb.AddForce(cameraTarget.transform.forward * currentCharge.chargeLevel);
                rb.AddTorque(cameraTarget.transform.right * currentCharge.chargeLevel);
                canJump = false;
            }
            timeCharging = 0;
            currentCharge.chargeLevel = 0;
        }

        currentCharge.canJump = canJump;

        if (canJump) {
            currentCharge.jumpBarOpacity = 1f;
        } else {
            currentCharge.jumpBarOpacity = PlayerCharge.JumpBarOpacityDefault;
        }
    }
}
