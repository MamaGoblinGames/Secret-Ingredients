using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MissionInfo : MonoBehaviour
{
    public FlavorSettings flavorSettings;
    private IEnumerator coroutine;

    private void OnEnable()
    {
        flavorSettings.sweetTargetPosition = Length.Percent(0);
        flavorSettings.sourTargetPosition = Length.Percent(0);
        flavorSettings.saltyTargetPosition = Length.Percent(0);
        flavorSettings.bitterTargetPosition = Length.Percent(0);
        flavorSettings.savoryTargetPosition = Length.Percent(0);

        coroutine = WaitAndGenerateFlavors(1.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndGenerateFlavors(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        flavorSettings.sweetTargetPosition = Length.Percent(Random.Range(-100, 100));
        flavorSettings.sourTargetPosition = Length.Percent(Random.Range(-100, 100));
        flavorSettings.saltyTargetPosition = Length.Percent(Random.Range(-100, 100));
        flavorSettings.bitterTargetPosition = Length.Percent(Random.Range(-100, 100));
        flavorSettings.savoryTargetPosition = Length.Percent(Random.Range(-100, 100));

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // wait for 3 second animation to finish + 1 extra second,
        // then show the start button
        yield return new WaitForSeconds(4.0f);
        Button startButton = root.Q<Button>("start_button");
        startButton.clicked += () => LoadGameScene();

        startButton.style.opacity = 1;
    }

    void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("KitchenTest");
    }
}
