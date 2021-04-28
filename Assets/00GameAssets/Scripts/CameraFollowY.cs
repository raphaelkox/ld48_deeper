using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowY : MonoBehaviour
{
    public Transform submarine;
    public float offset;

    private void LateUpdate() {
        transform.SetY(submarine.position.y + offset);
    }
}
