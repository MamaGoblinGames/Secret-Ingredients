using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddChildCollisions : MonoBehaviour
{
    void OnCollisionExit(Collision collision) {
        this.GetComponentInParent<Rigidbody>().SendMessage("OnCollisionExit", collision);
    }

    void OnCollisionStay(Collision collision) {
        this.GetComponentInParent<Rigidbody>().SendMessage("OnCollisionStay", collision);
    }
}
