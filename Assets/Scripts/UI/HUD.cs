using System;
using UnityEngine;
using UnityEngine.UIElements;

public partial class HUD : MonoBehaviour
{
    public HUDData hudData;
    public PlayersInfo playersInfo;

    public void StartGame() {
        GetComponent<UIDocument>().enabled = true;
        hudData.StartGame();
    }

    private void Awake() {
        playersInfo.ResetPlayers();
        hudData.gameStarted = false;
        hudData.ResetTimeRemaining();
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
