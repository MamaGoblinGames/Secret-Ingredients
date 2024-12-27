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
        audioSource = GameObject.Find("Collisions Audio Source").GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (audioSource == null) return;

        // play sound volume based on collision magnitude
        float magnitude = collision.relativeVelocity.magnitude;
        ObjectSoundInfo otherSounds = collision.gameObject.GetComponent<ObjectSoundInfo>();
        AudioClip audioClip = otherSounds ? otherSounds.collisionSound : collisionSound;
        audioSource.volume = Mathf.Clamp((magnitude - MIN_MAGNITUDE) / (MAX_MAGNITUDE - MIN_MAGNITUDE), 0.0f, 1.0f);
        Debug.Log("Collision magnitude: " + magnitude + " Volume: " + audioSource.volume);
        audioSource.PlayOneShot(audioClip);
    }
}
