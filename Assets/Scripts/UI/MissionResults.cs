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

    private int CalculateDisguiseRating(Flavor playerFlavor) {
        float flavorDifference = CalculateTotalFlavorDifference(playerFlavor);
        float flavorDifferencePercent = flavorDifference / 600.0f * 100.0f;
        return Mathf.CeilToInt(100f-flavorDifferencePercent);
    }

    private void SetResults(string resultElementString, Flavor playerFlavor) {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement.Q(resultElementString);
        // root.dataSource = playerFlavor;
        int disguiseRating = CalculateDisguiseRating(playerFlavor);
        ResultData result = new(disguiseRating);
        Label flavorDifferenceLabel = root.Q<Label>("flavor_difference");
        flavorDifferenceLabel.text = result.DisguiseRatingText;

        Label missionScoreLabel = root.Q<Label>("mission_score");
        missionScoreLabel.text = result.MissionScore;

        root.style.display = DisplayStyle.Flex;
    }
    private void OnEnable()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        
        if (playersInfo.numPlayers >= 1) {
            SetResults("player_1_results", playersInfo.player1Flavor);
        }
        if (playersInfo.numPlayers >= 2) {
            SetResults("player_2_results", playersInfo.player2Flavor);
        }
        if (playersInfo.numPlayers >= 3) {
            SetResults("player_3_results", playersInfo.player3Flavor);
        }
        if (playersInfo.numPlayers >= 4) {
            SetResults("player_4_results", playersInfo.player4Flavor);
        }

        // show buttons after a few seconds to avoid immediate clicking on accident
        coroutine = WaitAndShowButton(2.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndShowButton(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
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
