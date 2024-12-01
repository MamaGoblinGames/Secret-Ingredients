using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCamera : MonoBehaviour
{

    public Camera joinCam;
    public PlayerInputManager playerManager;

    // Update is called once per frame
    public void StartMatch()
    {
        Destroy(joinCam);
        playerManager.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
        Destroy(this);
    }
}
