using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;


[CreateAssetMenu(fileName = "Flavor", menuName = "Scriptable Objects/Flavor")]
public class Flavor : ScriptableObject
{
    const float flavorMin = -100f;
    const float flavorMax = 100f;
    const float flavorNeutral = 0f;
    const float flavorNeutralPercent = 50f;

    public static float Min = flavorMin;
    public static float Max = flavorMax;
    public static float Neutral = flavorNeutral;

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
    [Range(0f, 100f)]
    public float neutralizeSpeed = 0f;
    
    [HideInInspector]
    public Length sweetPercent = Length.Percent(flavorNeutralPercent);
    [HideInInspector]
    public Length sourPercent = Length.Percent(flavorNeutralPercent);
    [HideInInspector]
    public Length saltyPercent = Length.Percent(flavorNeutralPercent);
    [HideInInspector]
    public Length bitterPercent = Length.Percent(flavorNeutralPercent);
    [HideInInspector]
    public Length umamiPercent = Length.Percent(flavorNeutralPercent);
    [HideInInspector]
    public Length temperaturePercent = Length.Percent(flavorNeutralPercent);

    private Length convertFlavorValueToPercent(float value) {
        return Length.Percent((value + (flavorMax - flavorMin) / 2) / (flavorMax - flavorMin)*100);
    }

    public void SyncFlavorPercentages() {
        // create 0 to 100 percentage values for each flavor in Length.Percent format
        sweetPercent = convertFlavorValueToPercent(sweet);
        sourPercent = convertFlavorValueToPercent(sour);
        saltyPercent = convertFlavorValueToPercent(salty);
        bitterPercent = convertFlavorValueToPercent(bitter);
        umamiPercent = convertFlavorValueToPercent(umami);
        temperaturePercent = convertFlavorValueToPercent(temperature);
    }
    
    void Initialize() {
        SyncFlavorPercentages();
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

    private float CalculateNeutralizedFlavor(float flavorValue, float effect) {
        return flavorValue > flavorNeutral ? Math.Max(flavorValue-effect, 0) : Math.Min(flavorValue+effect, 0);
    }
    public void Neutralize(float amountPerSecond) {
        float effect = amountPerSecond * Time.deltaTime;
        sweet = CalculateNeutralizedFlavor(sweet, effect);
        sour = CalculateNeutralizedFlavor(sour, effect);
        salty = CalculateNeutralizedFlavor(salty, effect);
        bitter = CalculateNeutralizedFlavor(bitter, effect);
        umami = CalculateNeutralizedFlavor(umami, effect);
        temperature = CalculateNeutralizedFlavor(temperature, effect);

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

        // Note that this will only really work well for something
        // like water, where the flavor parts are neutralized by the same amount.
        // We can add more complicated neutralization per-flavor later.
        if (neutralizeSpeed > 0) Neutralize(neutralizeSpeed);

        sweet = Mathf.Clamp(sweet, flavorMin, flavorMax);
        sour = Mathf.Clamp(sour, flavorMin, flavorMax);
        salty = Mathf.Clamp(salty, flavorMin, flavorMax);
        bitter = Mathf.Clamp(bitter, flavorMin, flavorMax);
        umami = Mathf.Clamp(umami, flavorMin, flavorMax);
        temperature = Mathf.Clamp(temperature, flavorMin, flavorMax);

        SyncFlavorPercentages();
    }
}
