using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharge", menuName = "Scriptable Objects/PlayerCharge")]
public class PlayerCharge : ScriptableObject
{
    [Range(0f, 2000f)]
    public float chargeLevel = 0f;

    public float maxCharge = 2000f;

    public bool canJump = false;

    [Range(0f, 100f)]
    public float jumpBarOpacity = 0.35f;
}
