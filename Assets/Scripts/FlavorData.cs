using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class FlavorData : MonoBehaviour
{   
    public bool killOnTransfer;
    public bool neutralizeTemp;
    private float flavorMin = 0f;
    private float flavorMax = 100f;
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

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerFlavor) {
            applyFlavor(0.2f);
            // Delete the thing
            if (killOnTransfer) Destroy(gameObject);
        }
    }

    void OnParticleCollision(GameObject other) {
        if (other.CompareTag("Player") && playerFlavor) {
            applyFlavor(0.1f);
        }
    }

    private float clampFlavor(float flavor) {
        return Mathf.Clamp(flavor, flavorMin, flavorMax);
    }

    private void applyFlavor(float strength = 1f) {
        // apply each flavor attribute to the player's flavor
        playerFlavor.sweet += myFlavor.sweet * strength;
        playerFlavor.sour += myFlavor.sour * strength;
        playerFlavor.salty += myFlavor.salty * strength;
        playerFlavor.bitter += myFlavor.bitter * strength;
        playerFlavor.umami += myFlavor.umami * strength;
        playerFlavor.temperature += myFlavor.temperature * strength;

        // Neutralizing temperature effect (moves toward 0)
        if (neutralizeTemp) {
            if (playerFlavor.temperature > 0) {
                if (-playerFlavor.temperature > myFlavor.temperature) playerFlavor.temperature -= myFlavor.temperature;
                else playerFlavor.temperature = 0;
            }
            else if (playerFlavor.temperature < 0) {
                if (playerFlavor.temperature > myFlavor.temperature) playerFlavor.temperature += myFlavor.temperature;
                else playerFlavor.temperature = 0;
            }
        }

        // clamp the player's flavor
        playerFlavor.sweet = clampFlavor(playerFlavor.sweet);
        playerFlavor.sour = clampFlavor(playerFlavor.sour);
        playerFlavor.salty = clampFlavor(playerFlavor.salty);
        playerFlavor.bitter = clampFlavor(playerFlavor.bitter);
        playerFlavor.umami = clampFlavor(playerFlavor.umami);
        playerFlavor.temperature = clampFlavor(playerFlavor.temperature);
    }
}
