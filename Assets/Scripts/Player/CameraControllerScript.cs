using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour {

    // The player, as well as a float for the mouse sensitivity.
    public float mouseSensitivity;
    public Transform playerBody;

    // We need to have a axis clamp in order for the player to not have the camera rotate over the allowable bounds.
    float xAxisClamp = 0;

    /// <summary>
    /// Every frame, we are going to run the rotate camera function.
    /// </summary>
    /// <returns>void</returns>
    private void Update()
    {
        RotateCamera();    
    }

    /// <summary>
    /// On Awake, we are going to have the cursor state be locked.
    /// </summary><returns>void </returns>
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }
    void RotateCamera()
    {
        // Get the mouse input of the users.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        
        float rotAmountX = mouseX * mouseSensitivity;
        float rotAmountY = mouseY * mouseSensitivity;

        xAxisClamp -= rotAmountY;
    
        Vector3 targetRotCam = transform.rotation.eulerAngles;
        Vector3 targetRotBody = playerBody.rotation.eulerAngles;
 
        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0;
        targetRotBody.y += rotAmountX;

        // If it is over or under the allowed limit, then clamp it so that it stays within bounds.
            if (xAxisClamp > 90)
        {
            xAxisClamp = 90;
            targetRotCam.x = 90;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            targetRotCam.x = 270;
        }

        transform.rotation = Quaternion.Euler(targetRotCam);
        playerBody.rotation = Quaternion.Euler(targetRotBody);
    }



}
