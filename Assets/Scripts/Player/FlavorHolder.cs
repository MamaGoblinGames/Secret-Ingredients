using UnityEngine;

// FlavorHolder is a MonoBehaviour that holds a Flavor.
// It is used to attach a Flavor to a GameObject that can be referenced using GetComponent<FlavorHolder>()
// This is needed because you cannot reference a ScriptableObject directly using GetComponent.
public class FlavorHolder : MonoBehaviour
{
    public Flavor flavor;
}