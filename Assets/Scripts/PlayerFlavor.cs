using UnityEngine;

[CreateAssetMenu(fileName = "PlayerFlavor", menuName = "Scriptable Objects/PlayerFlavor")]
public class PlayerFlavor : ScriptableObject
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
