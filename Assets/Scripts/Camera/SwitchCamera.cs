using UnityEngine;
using UnityEngine.InputSystem;

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
        hud.SetActive(true);
        Destroy(this);
    }
}
