using System;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "SoundInfo", menuName = "Scriptable Objects/Sound Info")]
public class SoundInfo : ScriptableObject
{
    const float flavorMin = -100f;
    const float flavorMax = 100f;
    const float flavorNeutral = 0f;
    const float flavorNeutralPercent = 50f;

    public static float Min = flavorMin;
    public static float Max = flavorMax;
    public static float Neutral = flavorNeutral;

    [Header("Collisions")]
    public AudioClip defaultCollision;
    public float maxCollisionVolume = 1f;
    public AudioClip particlePickup;
    public float particlePickupVolume = 0.5f;

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
