using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private IEnumerator coroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Button startButton = root.Q<Button>("start_button");
        startButton.clicked += () => LoadGameScene();

        coroutine = WaitAndHideLogo(4.0f, root);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndHideLogo(float waitTime, VisualElement root)
    {
        yield return new WaitForSeconds(waitTime);
        // root.Q<VisualElement>("MGG_Logo").style.opacity = 0;
        root.Q<VisualElement>("MGG_Logo").style.top = Length.Percent(100);
        yield return new WaitForSeconds(1.0f);
        root.Q<VisualElement>("MGG_Logo").style.display = DisplayStyle.None;
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("Mission Info");
    }
}
