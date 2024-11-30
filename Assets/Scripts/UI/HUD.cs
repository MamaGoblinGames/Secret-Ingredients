using System;
using UnityEngine;

public partial class HUD : MonoBehaviour
{
    public HUDData hudData;
    private void OnEnable() {
        hudData.ResetTimeRemaining();
    }

    void Start() {
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
