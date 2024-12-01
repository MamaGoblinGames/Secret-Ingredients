using System;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "HUDData", menuName = "Scriptable Objects/HUD Data")]
public class HUDData : ScriptableObject
{
    const float defaultLevelDuration = 5f;
    public TimeSpan timeRemaining = TimeSpan.FromSeconds(defaultLevelDuration);
    public string formattedTimeRemaining;
    public bool gameStarted = false;

    public void StartGame() {
        gameStarted = true;
        ResetTimeRemaining();
    }
    private void SyncTimeRemaining() {
        formattedTimeRemaining = timeRemaining.ToString(@"mm\:ss");
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
