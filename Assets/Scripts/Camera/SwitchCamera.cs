using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SwitchCamera : MonoBehaviour
{

    public Camera joinCam;
    public PlayerInputManager playerManager;
    public GameObject hud;

    // Update is called once per frame
    public void StartMatch()
    {
        Destroy(joinCam);
        playerManager.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
        hud.GetComponent<UIDocument>().enabled = true;
        hud.GetComponent<HUD>().hudData.StartGame();
        Destroy(this);
    }
}
