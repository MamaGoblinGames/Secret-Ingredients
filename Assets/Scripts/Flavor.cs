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

    private void ShowFlavorChanges(GameObject other, float originalSweet, float originalSour, float originalSalty, float originalBitter, float originalUmami, float originalTemperature) {
        if(originalSweet != sweet) {
            Debug.Log("flavor change: sweet=" + (sweet - originalSweet));
            Vector3 newPos = new Vector3(other.transform.position.x + 0f, other.transform.position.y + 1f, other.transform.position.z);
            other.GetComponent<PlayerController>().flavorNumbers.sweet.Spawn(newPos, (sweet - originalSweet), other.transform);
        }
        if(originalSour != sour) {
            Debug.Log("flavor change: sour=" + (sour - originalSour));
            Vector3 newPos = new Vector3(other.transform.position.x + 0.5f, other.transform.position.y + 1.75f, other.transform.position.z);
            other.GetComponent<PlayerController>().flavorNumbers.sour.Spawn(newPos, (sour - originalSour), other.transform);
        }
        if(originalSalty != salty) {
            Debug.Log("flavor change: salty=" + (salty - originalSalty));
            Vector3 newPos = new Vector3(other.transform.position.x - 0.75f, other.transform.position.y + 1.5f, other.transform.position.z);
            other.GetComponent<PlayerController>().flavorNumbers.salty.Spawn(newPos, (salty - originalSalty), other.transform);
        }
        if(originalBitter != bitter) {
            Debug.Log("flavor change: bitter=" + (bitter - originalBitter));
            Vector3 newPos = new Vector3(other.transform.position.x + 0.5f, other.transform.position.y + 0.25f, other.transform.position.z);
            other.GetComponent<PlayerController>().flavorNumbers.bitter.Spawn(newPos, (bitter - originalBitter), other.transform);
        }
        if(originalUmami != umami) {
            Debug.Log("flavor change: umami=" + (umami - originalUmami));
            Vector3 newPos = new Vector3(other.transform.position.x - 0.75f, other.transform.position.y + 0.5f, other.transform.position.z);
            other.GetComponent<PlayerController>().flavorNumbers.umami.Spawn(newPos, (umami - originalUmami), other.transform);
        }
        if(originalTemperature != temperature) {
            Debug.Log("flavor change: temperature=" + (temperature - originalTemperature));
            Vector3 newPos = new Vector3(other.transform.position.x + 0.75f, other.transform.position.y + 1.25f, other.transform.position.z);
            other.GetComponent<PlayerController>().flavorNumbers.temperature.Spawn(newPos, (temperature - originalTemperature), other.transform);
        }
    }

    private void ApproachTarget(Flavor target, float strength = 1f, GameObject other = null) {
        float originalSweet = sweet;
        float originalSour = sour;
        float originalSalty = salty;
        float originalBitter = bitter;
        float originalUmami = umami;
        float originalTemperature = temperature;
        
        // determine the amount to move toward the target using strength, time, and a multiplier
        // (2000 is a multiplier that seems to work well)
        float effect = strength * Time.deltaTime * 1000;
        sweet = CalculatedApproachedFlavor(sweet, target.sweet, effect);
        sour = CalculatedApproachedFlavor(sour, target.sour, effect);
        salty = CalculatedApproachedFlavor(salty, target.salty, effect);
        bitter = CalculatedApproachedFlavor(bitter, target.bitter, effect);
        umami = CalculatedApproachedFlavor(umami, target.umami, effect);
        temperature = CalculatedApproachedFlavor(temperature, target.temperature, effect);

        if (other) {
            ShowFlavorChanges(other, originalSweet, originalSour, originalSalty, originalBitter, originalUmami, originalTemperature);
        }
    }

    private float CalculateAddedFlavor(float myFlavorValue, float otherFlavor, float strength = 1f) {
        return Mathf.Clamp(myFlavorValue + otherFlavor * strength, flavorMin, flavorMax);
    }

    /* Add flavor values */
    private void AddFlavorValues(Flavor flavor, float strength = 1f, GameObject other = null) {
        float originalSweet = sweet;
        float originalSour = sour;
        float originalSalty = salty;
        float originalBitter = bitter;
        float originalUmami = umami;
        float originalTemperature = temperature;

        sweet = CalculateAddedFlavor(sweet, flavor.sweet, strength);
        sour = CalculateAddedFlavor(sour, flavor.sour, strength);
        salty = CalculateAddedFlavor(salty, flavor.salty, strength);
        bitter = CalculateAddedFlavor(bitter, flavor.bitter, strength);
        umami = CalculateAddedFlavor(umami, flavor.umami, strength);
        temperature = CalculateAddedFlavor(temperature, flavor.temperature, strength);

        if (other) {
            ShowFlavorChanges(other, originalSweet, originalSour, originalSalty, originalBitter, originalUmami, originalTemperature);
        }
    }

    public void TransferFlavor (Flavor flavor, float strength = 1f, GameObject other = null) {
        // make copy of original flavor for later comparison
        float originalFlavorSweet = sweet;

        if (flavor.flavorMethod == FlavorMethod.Approach) {
            ApproachTarget(flavor, strength, other);
        } else {
            AddFlavorValues(flavor, strength, other);
        }

        SyncFlavorPercentages();
    }
}
