using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharge", menuName = "Scriptable Objects/PlayerCharge")]
public class PlayerCharge : ScriptableObject
{
    const float max = 2000f;
    public static float Max = max;
    public static float JumpBarOpacityDefault = 0.35f;
    
    [Range(0f, max)]
    public float chargeLevel = 0f;
    public float maxCharge = Max;
    public bool canJump = false;

    [Range(0f, 1f)]
    public float jumpBarOpacity = JumpBarOpacityDefault;

    public void OnEnable() {
        Reset();
    }
    
    public void Reset() {
        chargeLevel = 0f;
        canJump = false;
        jumpBarOpacity = JumpBarOpacityDefault;
    }
}
