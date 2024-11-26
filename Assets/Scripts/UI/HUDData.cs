using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HUDData", menuName = "Scriptable Objects/HUD Data")]
public class HUDData : ScriptableObject
{
    const float defaultLevelDuration = 5f;
    public TimeSpan timeRemaining = TimeSpan.FromSeconds(defaultLevelDuration);
    public string formattedTimeRemaining;

    private void SyncTimeRemaining() {
        formattedTimeRemaining = timeRemaining.ToString(@"mm\:ss");
    }

    void OnEnable() {
        ResetTimeRemaining();
    }
    void OnValidate() {
        SyncTimeRemaining();
    }

    public void ResetTimeRemaining() {
        timeRemaining = TimeSpan.FromSeconds(defaultLevelDuration);
        SyncTimeRemaining();
    }
    public void SetTimeRemaining(TimeSpan time) {
        timeRemaining = time;
        SyncTimeRemaining();
    }
}
