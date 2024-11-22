using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "FlavorSettings", menuName = "Scriptable Objects/Flavor Settings")]
public class FlavorSettings : ScriptableObject
{
    public Length sweetTargetPosition = Length.Percent(0);
    public Length sourTargetPosition = Length.Percent(0);
    public Length saltyTargetPosition = Length.Percent(0);
    public Length bitterTargetPosition = Length.Percent(0);
    public Length savoryTargetPosition = Length.Percent(0);
}
