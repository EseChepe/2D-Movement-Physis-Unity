using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{   
    public Transform target;
    public Vector3 offset;

    //The higher this value is the quicker the camera will follow the player
    [Range (1,10)] public float smoothFactor;

    private void FixedUpdate() {
        Follow();
    }

    void Follow() {
        Vector3 targetPosition = target.position + offset;
        
        //Makes the camera transition smoother and non-instantaneous
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
