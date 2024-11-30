using System;
using UnityEngine;

public partial class HUD : MonoBehaviour
{
    public HUDData hudData;
    public PlayersInfo playersInfo;
    private void OnEnable() {
        hudData.ResetTimeRemaining();
        playersInfo.ResetPlayers();
    }

    void Update() {
        TimeSpan newTimeRemaining = hudData.timeRemaining.Add(TimeSpan.FromSeconds(-Time.deltaTime));
        if (newTimeRemaining <= TimeSpan.Zero) {
            newTimeRemaining = TimeSpan.Zero;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Mission Recap");
        }
        hudData.SetTimeRemaining(newTimeRemaining);
    }
}
