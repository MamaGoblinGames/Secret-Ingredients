using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MissionInfo : MonoBehaviour
{
    public Flavor flavorSettings;
    private IEnumerator coroutine;

    private void OnEnable()
    {
        // flavorSettings.Neutralize();

        coroutine = WaitAndGenerateFlavors(1.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndGenerateFlavors(float waitTime)
    {
        // yield return new WaitForSeconds(waitTime);
        flavorSettings.sweet = Random.Range(Flavor.Min, Flavor.Max);
        flavorSettings.sour = Random.Range(Flavor.Min, Flavor.Max);
        flavorSettings.salty = Random.Range(Flavor.Min, Flavor.Max);
        flavorSettings.bitter = Random.Range(Flavor.Min, Flavor.Max);
        flavorSettings.umami = Random.Range(Flavor.Min, Flavor.Max);
        flavorSettings.temperature = Random.Range(Flavor.Min, Flavor.Max);
        flavorSettings.SyncFlavorPercentages();

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // wait for 3 second animation to finish + 1 extra second,
        // then show the start button
        yield return new WaitForSeconds(waitTime+3f);
        Button startButton = root.Q<Button>("start_button");
        startButton.clicked += () => LoadGameScene();

        startButton.style.opacity = 1;
    }

    void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("KitchenTest");
    }
}
