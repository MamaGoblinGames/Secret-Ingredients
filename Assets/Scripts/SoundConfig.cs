using System;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "SoundConfig", menuName = "Scriptable Objects/Sound Configuration")]
public class SoundConfig : ScriptableObject
{

    [Header("Collisions")]
    public AudioClip defaultCollision;
    public float maxCollisionVolume = 1f;
    public float maxCollisionMagnitude = 40.0f;
    public float minCollisionMagnitude = 7.0f;

    [Header("Particle Pickup")]
    public AudioClip particlePickup;
    public float particlePickupVolume = 0.5f;
    public float particlePickupPitchMax = 1.1f;
    public float particlePickupPitchMin = 0.9f;

    [Header("UI Sounds")]
    public AudioClip pause;
    public float pausePitch = 1f;
    public AudioClip unpause;
    public float unpausePitch = 2f;

    [Header("Player Sounds")]
    public AudioClip jump;
    public float maxJumpVolume = 1f;
    public float jumpPitch = 1f;
    public AudioClip charge;
}
