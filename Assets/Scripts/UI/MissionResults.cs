using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MissionResults : MonoBehaviour
{
    public Flavor flavorSettings;
    public PlayersInfo playersInfo;
    private IEnumerator coroutine;

    private float CalculateFlavorDifference(Length target, Length player) {
        return Mathf.Abs(target.value - player.value);
    }
    private float CalculateTotalFlavorDifference(Flavor playerFlavor) {
        float difference = 0;
        difference += CalculateFlavorDifference(flavorSettings.sweetPercent, playerFlavor.sweetPercent);
        difference += CalculateFlavorDifference(flavorSettings.sourPercent, playerFlavor.sourPercent);
        difference += CalculateFlavorDifference(flavorSettings.saltyPercent, playerFlavor.saltyPercent);
        difference += CalculateFlavorDifference(flavorSettings.bitterPercent, playerFlavor.bitterPercent);
        difference += CalculateFlavorDifference(flavorSettings.umamiPercent, playerFlavor.umamiPercent);
        difference += CalculateFlavorDifference(flavorSettings.temperaturePercent, playerFlavor.temperaturePercent);
        return difference;
    }

    private string CalculateMissionRating(int disguiseRating) {
        if (disguiseRating == 100) {
            return "A++";
        } else if (disguiseRating >= 95) {
            return "A+";
        } else if (disguiseRating >= 90) {
            return "A";
        } else if (disguiseRating >= 85) {
            return "B";
        } else if (disguiseRating >= 75) {
            return "C";
        } else if (disguiseRating >= 65) {
            return "D";
        } else {
            return "F";
        }
    }
    private void OnEnable()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;

        VisualElement root = GetComponent<UIDocument>().rootVisualElement.Q("player_1_results");
        float flavorDifference = CalculateTotalFlavorDifference(playersInfo.player1Flavor);
        float flavorDifferencePercent = flavorDifference / 600.0f * 100.0f;
        int disguiseRating = Mathf.CeilToInt(100f-flavorDifferencePercent);

        Label flavorDifferenceLabel = root.Q<Label>("flavor_difference");
        flavorDifferenceLabel.text = "Your disguise was " + disguiseRating.ToString() + "% effective.";

        Label missionScoreLabel = root.Q<Label>("mission_score");
        missionScoreLabel.text = CalculateMissionRating(disguiseRating);

        // show buttons after a few seconds to avoid immediate clicking on accident
        coroutine = WaitAndShowButton(root, 2.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndShowButton(VisualElement root, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Button button = root.Q<Button>("continue_button");
        button.clicked += () => LoadGameScene();
        button.style.opacity = 1;

        Button quitButton = root.Q<Button>("quit_button");
        quitButton.clicked += () => QuitGame();
        quitButton.style.opacity = 1;
    }

    void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mission Info");
    }

    void QuitGame() {
        Application.Quit();
    }
}
