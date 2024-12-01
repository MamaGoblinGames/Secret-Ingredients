using System;
using UnityEngine;

public partial class HUD : MonoBehaviour
{
    public HUDData hudData;
    public PlayersInfo playersInfo;
    private void Awake() {
        playersInfo.ResetPlayers();
        hudData.StartGame();
    }

    void Update() {
        if (!hudData.gameStarted) {
            return;
        }
        TimeSpan newTimeRemaining = hudData.timeRemaining.Add(TimeSpan.FromSeconds(-Time.deltaTime));
        if (newTimeRemaining <= TimeSpan.Zero) {
            newTimeRemaining = TimeSpan.Zero;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Mission Recap");
        }
        hudData.SetTimeRemaining(newTimeRemaining);
    }
}
