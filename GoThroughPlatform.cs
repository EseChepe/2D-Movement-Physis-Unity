using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoThroughPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        //Checks if the player suddenly changed his mind about going down and resets the rotational offset
        if (Input.GetButtonUp("Down")) {
            waitTime = 0.15f;
            effector.rotationalOffset = 0;
        }

        //Allows the player do go Down when it presses the 'S' key
        if (Input.GetButton("Down"))
        {
            if (waitTime <= 0) {
                effector.rotationalOffset = 180f;
                waitTime = 0.15f;
            } else {
                waitTime -= Time.deltaTime;
            }
        }

        //Allows the player to go through the platform again after already dropping through it
        if (Input.GetButtonDown("Jump")) {
            effector.rotationalOffset = 0;
        }
    }
}
