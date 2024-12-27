using UnityEngine;

public class CollisionSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip collisionSound;

    private const float MAX_MAGNITUDE = 40.0f;

    void OnCollisionEnter(Collision collision)
    {
        // skip playing if not colliding with player or already playing
        if (!collision.gameObject.CompareTag("Player") || audioSource.isPlaying) return;

        // play sound volume based on collision magnitude
        float magnitude = collision.relativeVelocity.magnitude;
        audioSource.volume = Mathf.Clamp(magnitude / MAX_MAGNITUDE, 0.0f, 1.0f);
        Debug.Log("Collision magnitude: " + magnitude + " Volume: " + audioSource.volume);
        audioSource.PlayOneShot(collisionSound);
    }   
}
