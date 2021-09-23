using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{

    public PlayerMovement Player;
    public float Timer = 1;
    public bool activateTimer = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activateTimer) {
            Timer -= Time.deltaTime;
        }

        if (Timer < 0) {
            Timer = 0.2f;
            activateTimer = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) { 
            

            Player.isJumping = true;
            
            Player.myBody.AddForce(new Vector2(0, 30), ForceMode2D.Impulse);

            Player.canDash = true;
            activateTimer = true;
        }
    }
    
}
