using UnityEngine;

public class PlayerCollisionSounds : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip collisionSound;

    private const float MAX_MAGNITUDE = 40.0f;
    private const float MIN_MAGNITUDE = 7.0f;

    void Start()
    {
        // get audio source component
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (audioSource == null) return;

        // Calculate volume based on collision magnitude.
        // Volume is 0 when magnitude is less than MIN_MAGNITUDE.
        float magnitude = collision.relativeVelocity.magnitude;
        float volume = Mathf.Clamp((magnitude - MIN_MAGNITUDE) / (MAX_MAGNITUDE - MIN_MAGNITUDE), 0.0f, 1.0f);

        // Find out if the other object has a collision sound
        ObjectSoundInfo otherSounds = collision.gameObject.GetComponent<ObjectSoundInfo>();

        // If the other object has a collision sound, use that. Otherwise, use the default sound.
        AudioClip audioClip = otherSounds ? otherSounds.collisionSound : collisionSound;
        // Set the volume.
        audioSource.volume = volume;

        // Play the sound.
        audioSource.PlayOneShot(audioClip);

        Debug.Log("Collision Sound - magnitude: " + magnitude + " Volume: " + audioSource.volume + " Clip: " + audioClip.name);
    }
}
