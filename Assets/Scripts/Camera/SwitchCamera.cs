using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SwitchCamera : MonoBehaviour
{

    public Camera joinCam;
    public PlayerInputManager playerManager;
    public GameObject hud;
    public GameObject lobby;

    // Update is called once per frame
    public void StartMatch()
    {
        Destroy(joinCam);
        playerManager.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
        lobby.GetComponent<UIDocument>().enabled = false;
        hud.GetComponent<HUD>().StartGame();
        Destroy(this);
    }
}
