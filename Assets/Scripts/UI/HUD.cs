using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

public partial class HUD : MonoBehaviour
{
    private ProgressBar sweetFlavorBar => GetComponent<UIDocument>().rootVisualElement.Q<ProgressBar>("sweetProgress");
    private ProgressBar savoryFlavorBar => GetComponent<UIDocument>().rootVisualElement.Q<ProgressBar>("savoryProgress");
    private ProgressBar saltyFlavorBar => GetComponent<UIDocument>().rootVisualElement.Q<ProgressBar>("saltyProgress");
    private ProgressBar sourFlavorBar => GetComponent<UIDocument>().rootVisualElement.Q<ProgressBar>("sourProgress");
    private ProgressBar bitterFlavorBar => GetComponent<UIDocument>().rootVisualElement.Q<ProgressBar>("bitterProgress");

    private void OnEnable() 
    {
    }

    // public HUD (
    //     SerializedProperty SweetProperty,
    //     SerializedProperty SaltyProperty,
    //     SerializedProperty SourProperty,
    //     SerializedProperty SavoryProperty,
    //     SerializedProperty BitterProperty,
    //     float sweetFlavor = 0,
    //     float sourFlavor = 0,
    //     float savoryFlavor = 0,
    //     float saltyFlavor = 0,
    //     float bitterFlavor = 0
    // ) {
    //     Init(SweetProperty, SaltyProperty, SourProperty, SavoryProperty, BitterProperty, sweetFlavor, sourFlavor, savoryFlavor, saltyFlavor, bitterFlavor);
    // }

    // void Init(
    //     SerializedProperty SweetProperty,
    //     SerializedProperty SaltyProperty,
    //     SerializedProperty SourProperty,
    //     SerializedProperty SavoryProperty,
    //     SerializedProperty BitterProperty,
    //     float sweetFlavor = 0,
    //     float sourFlavor = 0,
    //     float savoryFlavor = 0,
    //     float saltyFlavor = 0,
    //     float bitterFlavor = 0
    // ) {
    //     VisualTreeAsset asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UIs/HUD.uxml");
    //     asset.CloneTree(this);

    //     sweetFlavorBar.value = sweetFlavor;
    //     savoryFlavorBar.value = savoryFlavor;
    //     saltyFlavorBar.value = saltyFlavor;
    //     sourFlavorBar.value = sourFlavor;
    //     bitterFlavorBar.value = bitterFlavor;

    //     sweetFlavorBar.BindProperty(SweetProperty);
    //     saltyFlavorBar.BindProperty(SaltyProperty);
    //     sourFlavorBar.BindProperty(SourProperty);
    //     savoryFlavorBar.BindProperty(SavoryProperty);
    //     bitterFlavorBar.BindProperty(BitterProperty);
    // }
}
