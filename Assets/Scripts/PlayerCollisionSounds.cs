using UnityEngine;

public class PlayerCollisionSounds : MonoBehaviour
{
    private AudioSource collisionAudioSource;
    private AudioSource particlePickupAudioSource;
    public SoundInfo soundInfo;

    private const float MAX_MAGNITUDE = 40.0f;
    private const float MIN_MAGNITUDE = 7.0f;

    void Start()
    {
        // get audio source component to use for collision sounds
        collisionAudioSource = GetComponents<AudioSource>()[0];

        // get audio source component to use for particle pickup sounds and set volume to 50%
        particlePickupAudioSource = GetComponents<AudioSource>()[1];
    }

    void OnParticleCollision(GameObject other) {
        if (particlePickupAudioSource == null) return;

        // Play the sound.
        particlePickupAudioSource.PlayOneShot(soundInfo.particlePickup, soundInfo.particlePickupVolume);

        // Debug.Log("Particle Collision Sound - Clip: " + soundInfo.particlePickup.name);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collisionAudioSource == null) return;

        // Calculate volume based on collision magnitude.
        // Volume is 0 when magnitude is less than MIN_MAGNITUDE.
        // Scale volume from 0 to maxCollisionVolume.
        float magnitude = collision.relativeVelocity.magnitude;
        float volume = Mathf.Clamp(
            (magnitude - MIN_MAGNITUDE) / (MAX_MAGNITUDE - MIN_MAGNITUDE) * soundInfo.maxCollisionVolume,
            0.0f, 1.0f
        );

        // Find out if the other object has a collision sound
        ObjectSoundInfo otherSounds = collision.gameObject.GetComponent<ObjectSoundInfo>();

        // If the other object has a collision sound, use that. Otherwise, use the default sound.
        AudioClip audioClip = otherSounds ? otherSounds.collisionSound : soundInfo.defaultCollision;
        // Play the sound with the calculated volume.
        collisionAudioSource.PlayOneShot(audioClip, volume);

        // Debug.Log("Collision Sound - magnitude: " + magnitude + " Volume: " + volume + " Clip: " + audioClip.name);
    }
}
