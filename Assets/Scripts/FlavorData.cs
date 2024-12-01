using System.Collections.Generic;
using UnityEngine;

public class FlavorData : MonoBehaviour
{
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;

    public bool killOnTransfer;
    public int[] specialVals = null;
    public int[] specialNames = null;

    public Flavor myFlavor;
    public void Initialize(Flavor myFlavor) {
        this.myFlavor = myFlavor;
    }

    void Start() {
        if (killOnTransfer && TryGetComponent(out ParticleSystem part)) {
            collisionEvents = new List<ParticleCollisionEvent>();
        }
    }

    // Used for rigidbody based flavors
    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            FlavorHolder otherFlavor = collision.gameObject.GetComponent<FlavorHolder>();
            otherFlavor.flavor.TransferFlavor(myFlavor, 0.2f);
            // Delete the thing
            if (killOnTransfer) Destroy(gameObject);
        }
    }

    // Used for particle based flavors
    void OnParticleCollision(GameObject other) {
        if (other.CompareTag("Player") && TryGetComponent(out ParticleSystem part)) {
            // find out how many particles have collided with the player
            // > When OnParticleCollision is invoked from a script attached to a ParticleSystem, the GameObject parameter represents a GameObject with an attached Collider struck by the ParticleSystem. The ParticleSystem receives at most one message per Collider that is struck. As above, ParticlePhysicsExtensions.GetCollisionEvents must be used to retrieve all the collision incidents on the GameObject.
            int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
            // get the flavor of the player and transfer the flavor to the player
            FlavorHolder otherFlavor = other.GetComponent<FlavorHolder>();
            otherFlavor.flavor.TransferFlavor(myFlavor, 0.1f*numCollisionEvents);
        }
    }

    // Used for area effect flavors
    void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            FlavorHolder otherFlavor = other.GetComponent<FlavorHolder>();
            otherFlavor.flavor.TransferFlavor(myFlavor, Time.deltaTime * 0.75f);
        }
    }
}
