using System;
using UnityEngine;
using UnityEngine.UIElements;

public enum FlavorMethod {
    Additive,
    Approach
}

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

    [Header("Flavor Profile")]
    [Range(flavorMin, flavorMax)]
    public float sweet = flavorNeutral;
    [Range(flavorMin, flavorMax)]
    public float sour = flavorNeutral;
    [Range(flavorMin, flavorMax)]
    public float salty = flavorNeutral;
    [Range(flavorMin, flavorMax)]
    public float bitter = flavorNeutral;
    [Range(flavorMin, flavorMax)]
    [Tooltip("Savory")]
    public float umami = flavorNeutral;
    [Range(flavorMin, flavorMax)]
    [Tooltip("Spicy")]
    public float temperature = flavorNeutral;
    [Header("Is this a target flavor or an additive flavor?")]
    [Tooltip("Additive simply adds it's flavor, but target flavors makes the other flavor approach this one.")]
    public FlavorMethod flavorMethod = FlavorMethod.Additive;
    
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

    public void OnValidate() {
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

    /* Approach the target flavor values */
    private float CalculatedApproachedFlavor(float myFlavorValue, float targetFlavorValue, float effect) {
        return myFlavorValue > targetFlavorValue
            ? Math.Max(myFlavorValue-effect, targetFlavorValue)
            : Math.Min(myFlavorValue+effect, targetFlavorValue);
    }
    private void ApproachTarget(Flavor target, float strength = 1f) {
        // determine the amount to move toward the target using strength, time, and a multiplier
        // (2000 is a multiplier that seems to work well)
        float effect = strength * Time.deltaTime * 2000;
        sweet = CalculatedApproachedFlavor(sweet, target.sweet, effect);
        sour = CalculatedApproachedFlavor(sour, target.sour, effect);
        salty = CalculatedApproachedFlavor(salty, target.salty, effect);
        bitter = CalculatedApproachedFlavor(bitter, target.bitter, effect);
        umami = CalculatedApproachedFlavor(umami, target.umami, effect);
        temperature = CalculatedApproachedFlavor(temperature, target.temperature, effect);
    }

    private float CalculateAddedFlavor(float myFlavorValue, float otherFlavor, float strength = 1f) {
        return Mathf.Clamp(myFlavorValue + otherFlavor * strength, flavorMin, flavorMax);
    }

    /* Add flavor values */
    private void AddFlavorValues(Flavor flavor, float strength = 1f) {
        sweet = CalculateAddedFlavor(sweet, flavor.sweet, strength);
        sour = CalculateAddedFlavor(sour, flavor.sour, strength);
        salty = CalculateAddedFlavor(salty, flavor.salty, strength);
        bitter = CalculateAddedFlavor(bitter, flavor.bitter, strength);
        umami = CalculateAddedFlavor(umami, flavor.umami, strength);
        temperature = CalculateAddedFlavor(temperature, flavor.temperature, strength);
    }

    public void TransferFlavor (Flavor flavor, float strength = 1f) {
        if (flavor.flavorMethod == FlavorMethod.Approach) {
            ApproachTarget(flavor, strength);
        } else {
            AddFlavorValues(flavor, strength);
        }

        SyncFlavorPercentages();
    }
}
