using UnityEngine;


[CreateAssetMenu(fileName = "Flavor", menuName = "Scriptable Objects/Flavor")]
public class Flavor : ScriptableObject
{
    const float flavorMin = -100f;
    const float flavorMax = 100f;
    const float flavorNeutral = 0f;

    [Range(flavorMin, flavorMax)]
    public float sweet = flavorNeutral;
    [Range(flavorMin, flavorMax)]
    public float sour = flavorNeutral;
    [Range(flavorMin, flavorMax)]
    public float salty = flavorNeutral;
    [Range(flavorMin, flavorMax)]
    public float bitter = flavorNeutral;
    [Range(flavorMin, flavorMax)]
    public float umami = flavorNeutral;
    [Range(flavorMin, flavorMax)]
    public float temperature = flavorNeutral;

    [HideInInspector]
    public float sweetPercent = 0;
    [HideInInspector]
    public float sourPercent = 0;
    [HideInInspector]
    public float saltyPercent = 0;
    [HideInInspector]
    public float bitterPercent = 0;
    [HideInInspector]
    public float umamiPercent = 0;
    [HideInInspector]
    public float temperaturePercent = 0;

    private void SyncFlavorPercentages() {
        sweetPercent = (sweet + (flavorMax - flavorMin) / 2) / (flavorMax - flavorMin)*100;
        sourPercent = (sour + (flavorMax - flavorMin) / 2) / (flavorMax - flavorMin)*100;
        saltyPercent = (salty + (flavorMax - flavorMin) / 2) / (flavorMax - flavorMin)*100;
        bitterPercent = (bitter + (flavorMax - flavorMin) / 2) / (flavorMax - flavorMin)*100;
        umamiPercent = (umami + (flavorMax - flavorMin) / 2) / (flavorMax - flavorMin)*100;
        temperaturePercent = (temperature + (flavorMax - flavorMin) / 2) / (flavorMax - flavorMin)*100;
    }

    void OnEnable() {
        SyncFlavorPercentages();
    }

    public void Neutralize() {
        sweet = flavorNeutral;
        sour = flavorNeutral;
        salty = flavorNeutral;
        bitter = flavorNeutral;
        umami = flavorNeutral;
        temperature = flavorNeutral;

        SyncFlavorPercentages();
    }

    public void OnValidate() {
        SyncFlavorPercentages();
    }

    public void TransferFlavor (Flavor flavor, float strength = 1f) {
        sweet += flavor.sweet * strength;
        sour += flavor.sour * strength;
        salty += flavor.salty * strength;
        bitter += flavor.bitter * strength;
        umami += flavor.umami * strength;
        temperature += flavor.temperature * strength;

        // TODO: Implement neutralizeTemp
        // if (neutralizeTemp) {
        //     if (playerFlavor.temperature > 0) {
        //         if (-playerFlavor.temperature > myFlavor.temperature) playerFlavor.temperature -= myFlavor.temperature;
        //         else playerFlavor.temperature = 0;
        //     }
        //     else if (playerFlavor.temperature < 0) {
        //         if (playerFlavor.temperature > myFlavor.temperature) playerFlavor.temperature += myFlavor.temperature;
        //         else playerFlavor.temperature = 0;
        //     }
        // }

        sweet = Mathf.Clamp(sweet, flavorMin, flavorMax);
        sour = Mathf.Clamp(sour, flavorMin, flavorMax);
        salty = Mathf.Clamp(salty, flavorMin, flavorMax);
        bitter = Mathf.Clamp(bitter, flavorMin, flavorMax);
        umami = Mathf.Clamp(umami, flavorMin, flavorMax);
        temperature = Mathf.Clamp(temperature, flavorMin, flavorMax);

        SyncFlavorPercentages();
    }
}
