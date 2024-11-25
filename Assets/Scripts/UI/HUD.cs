using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public partial class HUD : MonoBehaviour
{
    public HUDData hudData;
    private void OnEnable() {
        hudData.ResetTimeRemaining();
    }

    void Start() {
    }

    void Update() {
        // if (hudData.timeRemaining > TimeSpan.Zero) {
            TimeSpan newTimeRemaining = hudData.timeRemaining.Add(TimeSpan.FromSeconds(-Time.deltaTime));
            if (newTimeRemaining <= TimeSpan.Zero) {
                newTimeRemaining = TimeSpan.Zero;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Mission Recap");
            }
            hudData.SetTimeRemaining(newTimeRemaining);
            System.Diagnostics.Debug.WriteLine(hudData.formattedTimeRemaining);
        // }
    }
}
