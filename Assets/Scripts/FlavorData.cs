using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlavorData : MonoBehaviour
{
    public bool modifier;
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
            if (modifier) {
                otherFlavors.sweet += sweet;
                otherFlavors.sour += sour;
                otherFlavors.salty += salty;
                otherFlavors.bitter += bitter;
                otherFlavors.umami += umami;
                otherFlavors.temperature += temperature;

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

                if (otherFlavors.sweet > otherFlavors.flavorMax) otherFlavors.sweet = otherFlavors.flavorMax;
                if (otherFlavors.sweet < otherFlavors.flavorMin) otherFlavors.sweet = otherFlavors.flavorMin;

                if (otherFlavors.sour > otherFlavors.flavorMax) otherFlavors.sour = otherFlavors.flavorMax;
                if (otherFlavors.sour < otherFlavors.flavorMin) otherFlavors.sour = otherFlavors.flavorMin;

                if (otherFlavors.salty > otherFlavors.flavorMax) otherFlavors.salty = otherFlavors.flavorMax;
                if (otherFlavors.salty < otherFlavors.flavorMin) otherFlavors.salty = otherFlavors.flavorMin;

                if (otherFlavors.bitter > otherFlavors.flavorMax) otherFlavors.bitter = otherFlavors.flavorMax;
                if (otherFlavors.bitter < otherFlavors.flavorMin) otherFlavors.bitter = otherFlavors.flavorMin;

                if (otherFlavors.umami > otherFlavors.flavorMax) otherFlavors.umami = otherFlavors.flavorMax;
                if (otherFlavors.umami < otherFlavors.flavorMin) otherFlavors.umami = otherFlavors.flavorMin;

                if (otherFlavors.temperature > otherFlavors.flavorMax) otherFlavors.temperature = otherFlavors.flavorMax;
                if (otherFlavors.temperature < -otherFlavors.flavorMax) otherFlavors.temperature = -otherFlavors.flavorMax;
            }
        }
    }
}
