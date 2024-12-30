using System;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "SoundConfig", menuName = "Scriptable Objects/Sound Configuration")]
public class SoundConfig : ScriptableObject
{
    [Header("Music")]
    public AudioClip lobbyMusic;
    public AudioClip mainGameMusic;

    [Header("Collisions")]
    public AudioClip defaultCollision;
    [Range(0f, 1f)]
    public float maxCollisionVolume = 1f;
    [Min(0.0f)]
    public float maxCollisionMagnitude = 40.0f;
    [Min(0.0f)]
    public float minCollisionMagnitude = 7.0f;

    [Header("Particle Pickup")]
    public AudioClip particlePickup;
    [Range(0f, 1f)]
    public float particlePickupVolume = 0.15f;
    [Range(0f, 2f)]
    public float particlePickupPitchMax = 1.1f;
    [Range(0f, 2f)]
    public float particlePickupPitchMin = 0.9f;

    [Header("Pausing")]
    public AudioClip pause;
    [Range(0f, 2f)]
    public float pausePitch = 1f;
    public AudioClip unpause;
    [Range(0f, 2f)]
    public float unpausePitch = 2f;

    [Header("Jumping")]
    public AudioClip jump;
    [Range(0f, 1f)]
    public float maxJumpVolume = 1f;
    [Range(0f, 2f)]
    public float jumpPitch = 1f;
    [Header("Charging")]
    public AudioClip charge;
}
