using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    public CharacterController controller; //Unity tool to help you code movement without a lot of the complications of physics.
    public float speed = 12f; // the fixed rate we want the player to move
    private Vector3 playerVelocity; // the change of speed of the player. 0 means the player is not moving at all
    private bool groundedPlayer; // a boolean to determine if the player is on the ground or not
    private float grav = -9.81f; // gravity that brings the player back to the ground when jumping
    private float jumpHeight = 1.0f; // how high we want the player's controls to let them jump

    // Start is called before the first frame update
    void Start()
    {
        // finds the CharacterController and sets it equal to our variable
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // checks if the controller is interacting with the ground
        groundedPlayer = controller.isGrounded;

        // checks if the player is on the ground and if they are moving down. 
        // a negative y value indicates the player is moving down, a positive y value would indicate the player is moving up
        if (groundedPlayer && playerVelocity.y <= 0)
        {
            // we want the player to stop moving vertically once they reach the ground after their jump
            playerVelocity.y = 0f;
        }

        // Input.GetAxis returns a value that lets us know what direction the mouse is pointing toward
        // it allows up to get information to then apply movement in that direction
        // a positive value usually means mouse is moving right/down
        // a negative value usually means mouse is moving left/up
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // establishes what direction the player should be moving based on the mouse position
        // relative to the direction the player is facing
        Vector3 move = transform.right * x + transform.forward * z;

        // actually moves the player in the correct direction at the correct speed multiplied by the amount of time in between frames (Time.deltaTime)
        controller.Move(move * speed * Time.deltaTime);


        // changes the height position of the player when the space bar is pressed
        // and the player is on the ground
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            // changes the players movement on the y axis by 
            // the square root of the set jump height * the direction we want it to move in * the gravity
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * grav);
        }

        // adds gravity to that y axis movement
        playerVelocity.y += grav * Time.deltaTime;

        // this moves the player by the by the specified y value based on how the player jumps
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
