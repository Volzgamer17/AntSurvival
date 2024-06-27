using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // how sensitive we want the mouse to be
    public float mouseSensitivity = 100f;

    // reference to the actual player object so we move the whole player and not just the camera
    public Transform playerBody;

    // the value we will want the player to move around the x axis
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // locks the cursor to the center of the screen so it doesn't leave the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        // finds the mouse position relative to the sensitivity and frame rate
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // decreases our xRotation based on the mouse's y position
        xRotation -= mouseY;
        // clamps keep a value in a specified range
        // this stops the player from looking all the way behind them 
        // (above their head & below their feet)
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotates the player around the x axis based on the mouse's y position
        // after our clamp
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        // rotates the player around the y axis based on the mouse's x position
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
