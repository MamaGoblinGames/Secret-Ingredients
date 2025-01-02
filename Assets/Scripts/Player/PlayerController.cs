using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using DamageNumbersPro;
using MoreMountains.Feedbacks;

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
    public MMFeedbacks jumpFeedback;
    public MMFeedbacks collisionFeedback;
    private SoundConfig soundConfig;

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
    public FlavorNumbers flavorNumbers;

    // Input
    private InputActionAsset inputAsset;
    public InputActionMap player;
    private InputAction move;

    // Camera
    [SerializeField] CinemachineBrain m_CinemachineBrain;
    [SerializeField] CinemachineCamera m_CinemachineCamera;
    [SerializeField] CinemachineInputAxisController m_CinemachineInputAxis;

    private AudioSource musicAudioSource;
    private AudioSource sfxAudioSource;
    private Outline[] outlines;

    private void Start() {
        musicAudioSource = GameObject.Find("Music").GetComponent<AudioSource>();
        sfxAudioSource = GameObject.Find("UI Sounds").GetComponent<AudioSource>();
        soundConfig = GameObject.Find("UI Sounds").GetComponent<SoundConfigHolder>().soundConfig;
    }

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

        // set flavor numbers and associate it with this player's camera
        flavorNumbers = playerInfo.flavorNumbers;
        flavorNumbers.sweet.cameraOverride = cameraTarget.transform;
        flavorNumbers.sour.cameraOverride = cameraTarget.transform;
        flavorNumbers.salty.cameraOverride = cameraTarget.transform;
        flavorNumbers.bitter.cameraOverride = cameraTarget.transform;
        flavorNumbers.umami.cameraOverride = cameraTarget.transform;
        flavorNumbers.temperature.cameraOverride = cameraTarget.transform;

        // Shift one bit per brain Count.
        m_CinemachineBrain.ChannelMask = (OutputChannels)(1 << playerNumber);
        m_CinemachineCamera.OutputChannel = (OutputChannels)(1 << playerNumber);
        m_CinemachineInputAxis.PlayerIndex = playerNumber - 1;
        if (m_CinemachineInputAxis.Controllers.Count > 0) {
            m_CinemachineInputAxis.Controllers[0].Input.InputAction = InputActionReference.Create(player.FindAction("Look"));
        }
        if (m_CinemachineInputAxis.Controllers.Count > 1) {
            m_CinemachineInputAxis.Controllers[1].Input.InputAction = InputActionReference.Create(player.FindAction("Look"));
        }
        if (m_CinemachineInputAxis.Controllers.Count > 2) {
            m_CinemachineInputAxis.Controllers[2].Input.InputAction = InputActionReference.Create(player.FindAction("Look"));
        }
        if (m_CinemachineInputAxis.Controllers.Count > 3) {
            m_CinemachineInputAxis.Controllers[3].Input.InputAction = InputActionReference.Create(player.FindAction("Look"));
        }

        // find flavor particle systems and add myself as a collider
        ParticleSystem[] particleSystems = FindObjectsByType<ParticleSystem>(FindObjectsSortMode.None);
        foreach (ParticleSystem ps in particleSystems) {
            Debug.Log("Adding collider to "+ps.name);
            ps.trigger.AddCollider(GetComponent<Collider>());
        }

        // find all highlightable/outline objects
        outlines = FindObjectsByType<Outline>(FindObjectsSortMode.None);
        Debug.Log("Found " + outlines.Length + " outlines");        
    }

    private void OnEnable() {
        player.FindAction("Submit").started += DoSubmit;
        player.Enable();
    }

    private void OnDisable() {
        player.FindAction("Fire").started -= DoCharge;
        player.FindAction("Fire").canceled -= DoJump;
        player.FindAction("Pause").started -= DoPause;
        player.FindAction("Submit").started -= DoSubmit;
        // player.FindAction("Highlight").started -= DoHighlight;
        player.Disable();
    }

    // OnCollisionStay is called once per frame for every Collider or Rigidbody that touches another Collider or Rigidbody.
    // NOTE: Collision stay events are not sent for sleeping Rigidbodies.
    //       "sleeping" = stopped moving and is no longer being processed by the Physics Engine.
    void OnCollisionStay(Collision collision) {
        isGrounded = true;
        coyoteTimer = 0;
    }

    // Calculate volume based on collision magnitude.
    // Volume is 0 when magnitude is less than minCollisionMagnitude.
    // Scale volume from 0 to maxCollisionVolume.
    private float GetCollisionVolume(float collisionMagnitude) {
        return Mathf.Clamp(
            (collisionMagnitude - soundConfig.minCollisionMagnitude)
              / (soundConfig.maxCollisionMagnitude - soundConfig.minCollisionMagnitude)
              * soundConfig.maxCollisionVolume,
            0.0f, 1.0f
        );
    }

    void OnCollisionEnter(Collision collision) {
        isGrounded = true;
        coyoteTimer = 0;

        // Find out if the other object has a collision sound
        ObjectSoundInfo otherSounds = collision.gameObject.GetComponent<ObjectSoundInfo>();

        // If the other object has a collision sound, use that. Otherwise, use the default sound.
        AudioClip audioClip = otherSounds ? otherSounds.collisionSound : soundConfig.defaultCollision;

        // Play the sound with the calculated volume.
        float volume = GetCollisionVolume(collision.relativeVelocity.magnitude);
        sfxAudioSource.pitch = 1f;
        sfxAudioSource.PlayOneShot(audioClip, volume);

        if (volume > 0) {
            // Play the collision feedback
            collisionFeedback.PlayFeedbacks(collision.transform.position, volume);
        }

        // Debug.Log("Collision Sound - magnitude: " + collision.relativeVelocity.magnitude + " Volume: " + volume + " Clip: " + audioClip.name);
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

    void DoHighlight(InputAction.CallbackContext context) {
        if (context.performed) {
            Debug.Log("Highlighting");
            foreach(Outline outline in outlines) {
                outline.enabled = true;
            }
            // Array.ForEach(outlines, o => o.enabled = true);
        }
        else {
            Debug.Log("Unhighlighting");
            // Array.ForEach(outlines, o => o.enabled = false);
            foreach(Outline outline in outlines) {
                outline.enabled = false;
            }
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
                float jumpSoundVolume = currentCharge.chargeLevel/PlayerCharge.Max * soundConfig.maxJumpVolume;
                sfxAudioSource.pitch = soundConfig.jumpPitch;
                sfxAudioSource.PlayOneShot(soundConfig.jump, jumpSoundVolume);
            }
            timeCharging = 0;
            currentCharge.chargeLevel = 0;
        }
    }

    void DoPause(InputAction.CallbackContext context) {
        // unpause
        if (Time.timeScale == 0) {
            Time.timeScale = 1;
            musicAudioSource.UnPause();
            sfxAudioSource.pitch = soundConfig.unpausePitch;
            Debug.Log("Unhighlighting");
            foreach(Outline outline in outlines) {
                outline.enabled = false;
            }
        }
        // pause
        else {
            Time.timeScale = 0;
            musicAudioSource.Pause();
            sfxAudioSource.pitch = soundConfig.pausePitch;
            Debug.Log("Highlighting");
            foreach(Outline outline in outlines) {
                Debug.Log("Highlighting outline");
                outline.enabled = true;
            }
        }
        sfxAudioSource.PlayOneShot(soundConfig.pause);
    }

    void DoSubmit(InputAction.CallbackContext context) {
        SwitchCamera cam = FindAnyObjectByType<SwitchCamera>();
        if (cam != null) {
            cam.StartMatch();
            foreach(PlayerController dude in FindObjectsByType<PlayerController>(FindObjectsSortMode.None)) {
                dude.player.FindAction("Fire").started += dude.DoCharge;
                dude.player.FindAction("Fire").canceled += dude.DoJump;
                dude.player.FindAction("Pause").started += dude.DoPause;
                // dude.player.FindAction("Highlight").started += dude.DoHighlight;
            }
        }
    }
}
