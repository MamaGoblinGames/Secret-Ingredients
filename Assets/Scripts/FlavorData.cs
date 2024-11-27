using UnityEngine;

public class FlavorData : MonoBehaviour
{   
    public bool killOnTransfer;
    public int[] specialVals = null;
    public int[] specialNames = null;

    public Flavor myFlavor;
    public Flavor playerFlavor;
    public void Initialize(Flavor playerFlavor, Flavor myFlavor) {
        this.playerFlavor = playerFlavor;
        this.myFlavor = myFlavor;
    }

    void Start() {
        if (killOnTransfer && TryGetComponent(out ParticleSystem part)) {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            
            foreach (GameObject player in players) {
                if(player.TryGetComponent(out Collider collider)) {
                    part.trigger.AddCollider(collider);
                }
            }
        }
    }

    // Used for rigidbody based flavors
    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerFlavor) {
            playerFlavor.TransferFlavor(myFlavor, 0.2f);
            // Delete the thing
            if (killOnTransfer) Destroy(gameObject);
        }
    }

    // Used for particle based flavors
    void OnParticleCollision(GameObject other) {
        if (other.CompareTag("Player") && playerFlavor) {
            playerFlavor.TransferFlavor(myFlavor, 0.1f);
        }
    }

    // Used for area effect flavors
    void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player") && playerFlavor) {
            playerFlavor.TransferFlavor(myFlavor, Time.deltaTime * 0.75f);
        }
    }
}
