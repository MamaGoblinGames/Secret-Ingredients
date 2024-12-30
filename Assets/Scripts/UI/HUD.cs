using System;
using UnityEngine;
using UnityEngine.UIElements;

public partial class HUD : MonoBehaviour
{
    public HUDData hudData;
    public PlayersInfo playersInfo;

    private SoundConfig soundConfig;
    private AudioSource musicAudioSource;

    public void StartGame() {
        GetComponent<UIDocument>().enabled = true;
        hudData.StartGame();
        musicAudioSource.clip = soundConfig.mainGameMusic;
        musicAudioSource.Play();
    }

    private void Start() {
        musicAudioSource = GameObject.Find("Music").GetComponent<AudioSource>();
        soundConfig = GameObject.Find("UI Sounds").GetComponent<SoundConfigHolder>().soundConfig;
        musicAudioSource.clip = soundConfig.lobbyMusic;
        musicAudioSource.Play();
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
