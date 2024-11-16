using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
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
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("KitchenTest");
    }
}
