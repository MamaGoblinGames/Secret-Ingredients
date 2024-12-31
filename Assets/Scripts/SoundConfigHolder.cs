using UnityEngine;

// SoundConfigHolder is a MonoBehaviour that holds a SoundConfig.
// It is used to attach a SoundConfig to a GameObject that can be referenced using GetComponent<SoundConfigHolder>()
// This is needed because you cannot reference a ScriptableObject directly using GetComponent.
public class SoundConfigHolder : MonoBehaviour
{
    public SoundConfig soundConfig;
}