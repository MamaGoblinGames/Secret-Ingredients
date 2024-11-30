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

    private void HideAllResults() {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q("player_1").style.display = DisplayStyle.None;
        root.Q("player_2").style.display = DisplayStyle.None;
        root.Q("player_3").style.display = DisplayStyle.None;
        root.Q("player_4").style.display = DisplayStyle.None;
    }

    private void SetResults(string playerNumber, Flavor playerFlavor) {
        VisualElement element = GetComponent<UIDocument>().rootVisualElement.Q("player_" + playerNumber);
        int disguiseRating = CalculateDisguiseRating(playerFlavor);
        ResultData result = new(disguiseRating);
        Label flavorDifferenceLabel = element.Q<Label>("flavor_difference");
        flavorDifferenceLabel.text = result.DisguiseRatingText;

        Label missionScoreLabel = element.Q<Label>("mission_score");
        missionScoreLabel.text = result.MissionScore;

        element.style.display = DisplayStyle.Flex;
        Debug.Log("Player " + playerNumber + " disguise rating: " + disguiseRating);
    }
    private void OnEnable()
    {
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        HideAllResults();
        
        if (playersInfo.numPlayers >= 1) {
            SetResults("1", playersInfo.player1Flavor);
        }
        if (playersInfo.numPlayers >= 2) {
            SetResults("2", playersInfo.player2Flavor);
        }
        if (playersInfo.numPlayers >= 3) {
            SetResults("3", playersInfo.player3Flavor);
        }
        if (playersInfo.numPlayers >= 4) {
            SetResults("4", playersInfo.player4Flavor);
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
