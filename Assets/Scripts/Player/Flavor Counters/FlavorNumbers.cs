using UnityEngine;
using DamageNumbersPro;

[CreateAssetMenu(fileName = "FlavorNumbers", menuName = "Scriptable Objects/FlavorNumbers")]
public class FlavorNumbers : ScriptableObject
{
    public DamageNumber sweet;
    public DamageNumber sour;
    public DamageNumber salty;
    public DamageNumber bitter;
    public DamageNumber umami;
    public DamageNumber temperature;
}
