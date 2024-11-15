using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlavorData : MonoBehaviour
{
    public bool modifier;
    public bool modifiable;
    public int sweet = 0;
    public int sour = 0;
    public int salty = 0;
    public int bitter = 0;
    public int umami = 0;
    public int temperature = 0;
    public bool neutralizeTemp;
    public int flavorMin = 0;
    public int flavorMax = 100;
    public int[] specialVals = null;
    public int[] specialNames = null;

    private FlavorData otherFlavors;

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out FlavorData otherFlavors)) {
            //otherFlavors = collision.gameObject.GetComponent<FlavorData>();

            // The code to modify the collided object's flavors
            if (modifier && otherFlavors.modifiable) {

                // Modify flavors
                otherFlavors.sweet += sweet;
                otherFlavors.sour += sour;
                otherFlavors.salty += salty;
                otherFlavors.bitter += bitter;
                otherFlavors.umami += umami;
                otherFlavors.temperature += temperature;

                // Neutralizing temperature effect (moves toward 0)
                if (neutralizeTemp) {
                    if (otherFlavors.temperature > 0) {
                        if (-otherFlavors.temperature > temperature) otherFlavors.temperature -= temperature;
                        else otherFlavors.temperature = 0;
                    }
                    else if (otherFlavors.temperature < 0) {
                        if (otherFlavors.temperature > temperature) otherFlavors.temperature += temperature;
                        else otherFlavors.temperature = 0;
                    }
                }

                // Clamp flavors into range
                otherFlavors.sweet = clampFlavor(otherFlavors.sweet, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.sour = clampFlavor(otherFlavors.sour, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.salty = clampFlavor(otherFlavors.salty, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.bitter = clampFlavor(otherFlavors.bitter, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.umami = clampFlavor(otherFlavors.umami, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.temperature = clampFlavor(otherFlavors.temperature, otherFlavors.flavorMin, otherFlavors.flavorMax);
            }
        }
    }

    void OnParticleCollision(GameObject other) {
        if (other.TryGetComponent(out FlavorData otherFlavors)) {
            //otherFlavors = collision.gameObject.GetComponent<FlavorData>();

            // The code to modify the collided object's flavors
            if (modifier && otherFlavors.modifiable) {

                // Modify flavors
                otherFlavors.sweet += sweet;
                otherFlavors.sour += sour;
                otherFlavors.salty += salty;
                otherFlavors.bitter += bitter;
                otherFlavors.umami += umami;
                otherFlavors.temperature += temperature;

                // Neutralizing temperature effect (moves toward 0)
                if (neutralizeTemp) {
                    if (otherFlavors.temperature > 0) {
                        if (-otherFlavors.temperature > temperature) otherFlavors.temperature -= temperature;
                        else otherFlavors.temperature = 0;
                    }
                    else if (otherFlavors.temperature < 0) {
                        if (otherFlavors.temperature > temperature) otherFlavors.temperature += temperature;
                        else otherFlavors.temperature = 0;
                    }
                }

                // Clamp flavors into range
                otherFlavors.sweet = clampFlavor(otherFlavors.sweet, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.sour = clampFlavor(otherFlavors.sour, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.salty = clampFlavor(otherFlavors.salty, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.bitter = clampFlavor(otherFlavors.bitter, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.umami = clampFlavor(otherFlavors.umami, otherFlavors.flavorMin, otherFlavors.flavorMax);
                otherFlavors.temperature = clampFlavor(otherFlavors.temperature, otherFlavors.flavorMin, otherFlavors.flavorMax);
            }
        }
    }

    private int clampFlavor(int flavor, int min, int max) {
        return flavor <= min ? min : flavor >= max ? max : flavor;
    }
}
