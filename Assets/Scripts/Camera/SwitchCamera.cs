using UnityEngine;

public class SwitchCamera : MonoBehaviour
{

    public Camera joinCam;

    // Update is called once per frame
    public void StartMatch()
    {
        Destroy(joinCam);
        Destroy(this);
    }
}
