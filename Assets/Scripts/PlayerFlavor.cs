using UnityEngine;

[CreateAssetMenu(fileName = "Flavor", menuName = "Scriptable Objects/Flavor")]
public class Flavor : ScriptableObject
{
    [Range(0f, 100f)]
    public float sweet = 0f;
    [Range(0f, 100f)]
    public float sour = 0f;
    [Range(0f, 100f)]
    public float salty = 0f;
    [Range(0f, 100f)]
    public float bitter = 0f;
    [Range(0f, 100f)]
    public float umami = 0f;
    [Range(0f, 100f)]
    public float temperature = 0f;
}
