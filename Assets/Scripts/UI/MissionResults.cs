using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MissionResults : MonoBehaviour
{
    public Flavor flavorSettings;
    public Flavor playerFlavor;
    private IEnumerator coroutine;

    private void OnEnable()
    {
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
    }

    void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mission Info");
    }
}
