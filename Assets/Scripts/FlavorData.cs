using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class FlavorData : MonoBehaviour
{   
    public bool modifier;
    public bool modifiable;
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

    public PlayerFlavor m_PlayerFlavor;
    public void Initialize(PlayerFlavor playerFlavor) {
        m_PlayerFlavor = playerFlavor;
    }

    private FlavorData otherFlavors;

    void Start() {
        if (killOnTransfer && TryGetComponent(out ParticleSystem part)) {
            FlavorData[] flavors = FindObjectsOfType<FlavorData>();
            foreach (FlavorData flavor in flavors) {
                if (flavor.modifiable && flavor.gameObject.TryGetComponent(out Collider collider)) part.trigger.AddCollider(collider);
            }
        }
    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out FlavorData otherFlavors)) {
            // The code to modify the collided object's flavors
            if (modifier && otherFlavors.modifiable && otherFlavors.m_PlayerFlavor) {
                // Modify flavors
                otherFlavors.m_PlayerFlavor.sweet += sweet;
                otherFlavors.m_PlayerFlavor.sour += sour;
                otherFlavors.m_PlayerFlavor.salty += salty;
                otherFlavors.m_PlayerFlavor.bitter += bitter;
                otherFlavors.m_PlayerFlavor.umami += umami;
                otherFlavors.m_PlayerFlavor.temperature += temperature;

                // Neutralizing temperature effect (moves toward 0)
                // if (neutralizeTemp) {
                //     if (otherFlavors.temperature > 0) {
                //         if (-otherFlavors.temperature > temperature) otherFlavors.temperature -= temperature;
                //         else otherFlavors.temperature = 0;
                //     }
                //     else if (otherFlavors.temperature < 0) {
                //         if (otherFlavors.temperature > temperature) otherFlavors.temperature += temperature;
                //         else otherFlavors.temperature = 0;
                //     }
                // }

                // Clamp flavors into range
                otherFlavors.m_PlayerFlavor.sweet = clampFlavor(otherFlavors.m_PlayerFlavor.sweet, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.m_PlayerFlavor.sour = clampFlavor(otherFlavors.m_PlayerFlavor.sour, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.m_PlayerFlavor.salty = clampFlavor(otherFlavors.m_PlayerFlavor.salty, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.m_PlayerFlavor.bitter = clampFlavor(otherFlavors.m_PlayerFlavor.bitter, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.m_PlayerFlavor.umami = clampFlavor(otherFlavors.m_PlayerFlavor.umami, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.m_PlayerFlavor.temperature = clampFlavor(otherFlavors.m_PlayerFlavor.temperature, otherFlavors.flavorMin, otherFlavors.flavorMax);

                // otherFlavors.m_PlayerFlavor.sweet = otherFlavors.sweet;
                // otherFlavors.m_PlayerFlavor.sour = otherFlavors.sour;
                // otherFlavors.m_PlayerFlavor.salty = otherFlavors.salty;
                // otherFlavors.m_PlayerFlavor.bitter = otherFlavors.bitter;
                // otherFlavors.m_PlayerFlavor.umami = otherFlavors.umami;
                // otherFlavors.m_PlayerFlavor.temperature = otherFlavors.temperature;

                // Delete the thing
                if (killOnTransfer) Destroy(gameObject);
            }
        }
    }

    void OnParticleCollision(GameObject other) {
        if (other.TryGetComponent(out FlavorData otherFlavors)) {
            //otherFlavors = collision.gameObject.GetComponent<FlavorData>();

            // The code to modify the collided object's flavors
            if (modifier && otherFlavors.modifiable && otherFlavors.m_PlayerFlavor) {

                // Modify flavors
                otherFlavors.m_PlayerFlavor.sweet += sweet;
                otherFlavors.m_PlayerFlavor.sour += sour;
                otherFlavors.m_PlayerFlavor.salty += salty;
                otherFlavors.m_PlayerFlavor.bitter += bitter;
                otherFlavors.m_PlayerFlavor.umami += umami;
                otherFlavors.m_PlayerFlavor.temperature += temperature;

                // Neutralizing temperature effect (moves toward 0)
                // if (neutralizeTemp) {
                //     if (otherFlavors.temperature > 0) {
                //         if (-otherFlavors.temperature > temperature) otherFlavors.temperature -= temperature;
                //         else otherFlavors.temperature = 0;
                //     }
                //     else if (otherFlavors.temperature < 0) {
                //         if (otherFlavors.temperature > temperature) otherFlavors.temperature += temperature;
                //         else otherFlavors.temperature = 0;
                //     }
                // }

                // Clamp flavors into range
                otherFlavors.m_PlayerFlavor.sweet = clampFlavor(otherFlavors.m_PlayerFlavor.sweet, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.m_PlayerFlavor.sour = clampFlavor(otherFlavors.m_PlayerFlavor.sour, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.m_PlayerFlavor.salty = clampFlavor(otherFlavors.m_PlayerFlavor.salty, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.m_PlayerFlavor.bitter = clampFlavor(otherFlavors.m_PlayerFlavor.bitter, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.m_PlayerFlavor.umami = clampFlavor(otherFlavors.m_PlayerFlavor.umami, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.m_PlayerFlavor.temperature = clampFlavor(otherFlavors.m_PlayerFlavor.temperature, otherFlavors.flavorMin, otherFlavors.flavorMax);

                // otherFlavors.m_PlayerFlavor.sweet = otherFlavors.sweet;
                // otherFlavors.m_PlayerFlavor.sour = otherFlavors.sour;
                // otherFlavors.m_PlayerFlavor.salty = otherFlavors.salty;
                // otherFlavors.m_PlayerFlavor.bitter = otherFlavors.bitter;
                // otherFlavors.m_PlayerFlavor.umami = otherFlavors.umami;
                // otherFlavors.m_PlayerFlavor.temperature = otherFlavors.temperature;
            }
        }
    }

    private float clampFlavor(float flavor, float min, float max) {
        return Mathf.Clamp(flavor, min, max);
    }
}
