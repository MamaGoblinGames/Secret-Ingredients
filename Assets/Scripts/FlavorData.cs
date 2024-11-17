using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class FlavorData : MonoBehaviour
{   
    public bool killOnTransfer;
    public float sweet = 0;
    public float sour = 0;
    public float salty = 0;
    public float bitter = 0;
    public float umami = 0;
    public float temperature = 0;
    public bool neutralizeTemp;
    public float flavorMin = 0f;
    public float flavorMax = 100f;
    public int[] specialVals = null;
    public int[] specialNames = null;

    public Flavor playerFlavor;
    public void Initialize(Flavor playerFlavor) {
        this.playerFlavor = playerFlavor;
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

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerFlavor) {
            
            // The code to modify the collided object's flavors
            // Modify flavors
            playerFlavor.sweet += sweet;
            playerFlavor.sour += sour;
            playerFlavor.salty += salty;
            playerFlavor.bitter += bitter;
            playerFlavor.umami += umami;
            playerFlavor.temperature += temperature;

            // Neutralizing temperature effect (moves toward 0)
            if (neutralizeTemp) {
                if (playerFlavor.temperature > 0) {
                    if (-playerFlavor.temperature > temperature) playerFlavor.temperature -= temperature;
                    else playerFlavor.temperature = 0;
                }
                else if (playerFlavor.temperature < 0) {
                    if (playerFlavor.temperature > temperature) playerFlavor.temperature += temperature;
                    else playerFlavor.temperature = 0;
                }
            }

            // Clamp flavors into range
            playerFlavor.sweet = clampFlavor(playerFlavor.sweet, flavorMin, flavorMax);
            playerFlavor.sour = clampFlavor(playerFlavor.sour, flavorMin, flavorMax);
            playerFlavor.salty = clampFlavor(playerFlavor.salty, flavorMin, flavorMax);
            playerFlavor.bitter = clampFlavor(playerFlavor.bitter, flavorMin, flavorMax);
            playerFlavor.umami = clampFlavor(playerFlavor.umami, flavorMin, flavorMax);
            playerFlavor.temperature = clampFlavor(playerFlavor.temperature, flavorMin, flavorMax);

            // Delete the thing
            if (killOnTransfer) Destroy(gameObject);
        }
    }

    void OnParticleCollision(GameObject other) {
        if (other.CompareTag("Player") && playerFlavor) {

            Debug.Log("particle collision!");
            // Modify flavors
            playerFlavor.sweet += sweet;
            playerFlavor.sour += sour;
            playerFlavor.salty += salty;
            playerFlavor.bitter += bitter;
            playerFlavor.umami += umami;
            playerFlavor.temperature += temperature;

            // Neutralizing temperature effect (moves toward 0)
            if (neutralizeTemp) {
                if (playerFlavor.temperature > 0) {
                    if (-playerFlavor.temperature > temperature) playerFlavor.temperature -= temperature;
                    else playerFlavor.temperature = 0;
                }
                else if (playerFlavor.temperature < 0) {
                    if (playerFlavor.temperature > temperature) playerFlavor.temperature += temperature;
                    else playerFlavor.temperature = 0;
                }
            }

            // Clamp flavors into range
            playerFlavor.sweet = clampFlavor(playerFlavor.sweet, flavorMin, flavorMax);
            playerFlavor.sour = clampFlavor(playerFlavor.sour, flavorMin, flavorMax);
            playerFlavor.salty = clampFlavor(playerFlavor.salty, flavorMin, flavorMax);
            playerFlavor.bitter = clampFlavor(playerFlavor.bitter, flavorMin, flavorMax);
            playerFlavor.umami = clampFlavor(playerFlavor.umami, flavorMin, flavorMax);
            playerFlavor.temperature = clampFlavor(playerFlavor.temperature, flavorMin, flavorMax);
        }
    }

    private float clampFlavor(float flavor, float min, float max) {
        return Mathf.Clamp(flavor, min, max);
    }
}
