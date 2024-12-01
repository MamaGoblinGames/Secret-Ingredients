using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

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
    private bool canJump;
    private bool charging;
    public int playerNumber;

    // Input
    private InputActionAsset inputAsset;
    public InputActionMap player;
    private InputAction move;

    // Camera
    [SerializeField] CinemachineBrain m_CinemachineBrain;
    [SerializeField] CinemachineCamera m_CinemachineCamera;
    [SerializeField] CinemachineInputAxisController m_CinemachineInputAxis;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PlayerInfo playerInfo = playersInfo.RegisterPlayer(rb.gameObject.name);
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        
        currentFlavor = playerInfo.flavor;
        flavorHolder = GetComponent<FlavorHolder>();
        flavorHolder.flavor = currentFlavor;
        currentCharge = playerInfo.charge;
        playerNumber = playerInfo.playerNumber;

        // Shift one bit per brain Count.
        m_CinemachineBrain.ChannelMask = (OutputChannels)(1 << playerNumber);
        m_CinemachineCamera.OutputChannel = (OutputChannels)(1 << playerNumber);
        m_CinemachineInputAxis.PlayerIndex = playerNumber;
        m_CinemachineInputAxis.Controllers[0].Input.InputAction = InputActionReference.Create(player.FindAction("Look"));
        m_CinemachineInputAxis.Controllers[1].Input.InputAction = InputActionReference.Create(player.FindAction("Look"));

        // find flavor particle systems and add myself as a collider
        ParticleSystem[] particleSystems = FindObjectsByType<ParticleSystem>(FindObjectsSortMode.None);
        foreach (ParticleSystem ps in particleSystems) {
            Debug.Log("Adding collider to "+ps.name);
            ps.trigger.AddCollider(GetComponent<Collider>());
        }
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

        currentCharge.canJump = canJump;

        if (canJump) {
            currentCharge.jumpBarOpacity = 1f;
        } else {
            currentCharge.jumpBarOpacity = PlayerCharge.JumpBarOpacityDefault;
        }

        // Charge up
        if (Time.timeScale != 0 && charging) {
            timeCharging += Time.deltaTime;
            currentCharge.chargeLevel = ChargeFunction(
                timeCharging,
                function.powerSeries,   // <-- This one
                new float[]{200, dCharge, 1.6f},
                PlayerCharge.Max
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
                rb.AddForce(cameraTarget.transform.forward * currentCharge.chargeLevel);
                rb.AddTorque(cameraTarget.transform.right * currentCharge.chargeLevel);
                canJump = false;
            }
            timeCharging = 0;
            currentCharge.chargeLevel = 0;
        }
    }

    void DoPause(InputAction.CallbackContext context) {
        if (Time.timeScale == 0) Time.timeScale = 1;
        else Time.timeScale = 0;
    }
}
