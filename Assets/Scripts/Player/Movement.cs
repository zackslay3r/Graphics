using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {


    // This is the player's character controller.
    CharacterController charControl;


    // These are floats for the current speed of the player as well as jump speed.
    public float speed;
    public float jumpSpeed;
    // This is used for directional movement.
    private Vector3 moveDirection = Vector3.zero;
    // This is gravity that is defaulted to 5.
    public float gravity = 5.0f;
    // This float is used to store the old Y coordate of our player (for the previous frame.)
    float oldY;

    // these floats are used for walk speed and crouch speed.
    public float walkSpeed, crouchSpeed;
    // These two Vector 3's are used to store the beginning centre and scale of the mesh collider/renderer.
    private Vector3 startCentre;
    private Vector3 startScale;


    // As well as the starting height.
    private float startHeight;



    /// <summary>
    /// On awake, we want to set all our private variables.
    /// This means we want to set our character controller to be the controller that is attached to the player.
    /// we also want to set our startHeight, startCentre and startScale to be of the player when they start so we have values
    /// that we can revert to when we go from crouching to standing.
    /// </summary>
    /// <returns>void</returns>
    void Awake()
    {
        charControl = GetComponent<CharacterController>();
        startHeight = charControl.height;
        startCentre = charControl.center;
        startScale = gameObject.transform.GetChild(1).localScale;

    }

    /// <summary>
    /// During update, we want to check every frame if our player is crouching.
    /// </summary>
    /// <returns>void</returns>
    private void Update()
    {
        // If the character control is grounded. 
        if (charControl.isGrounded)
        {
            // do the crouch check.
            crouch();

        }
            
    }
    /// <summary>
    /// During FixedUpdate, we want to do our base movement as well as jumping.
    /// we want to do this during fixed update so that we can be assured that the movement lines up with our physics tick's.
    /// </summary>
    /// <returns>void</returns>
    private void FixedUpdate()
    {
        //Set up the move direction.
      moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
      moveDirection = transform.TransformDirection(moveDirection);
      moveDirection.y = oldY;
      moveDirection *= speed;

        // If the character controller, is grounded, check to see if the user jumped.
        if (charControl.isGrounded)
        {

            if (Input.GetButton("Jump"))
            {
                oldY = jumpSpeed * Time.deltaTime;
                transform.parent = null;
            }

        }
        // if not, we will just pull them down to the ground via gravity.
        else
        {
            oldY -= gravity * Time.deltaTime;
        }
        // then, we will finally move the player.
        charControl.Move(moveDirection * Time.deltaTime);
    }


    /// <summary>
    /// This crouch function is responsible for checking to see if the user has pressed the button to crouch and if so, to rescale, recentre and change the height of our player.
    /// once the user stop's crouching, they should then return to the original values.
    /// </summary>
    /// <returns>void </returns>
    void crouch()
    {

        // have all the new variables be set to defaults.
        Vector3 newCentre = startCentre;
        Vector3 newScale = startScale;
        float newHeight = startHeight;

        // If the user pressed the left control button, change the default values.
        if (Input.GetKey(KeyCode.LeftControl))
        {
            newHeight = 0.5f * startHeight;
            newScale = new Vector3(newScale.x, 0.5f, newScale.z);
            speed = crouchSpeed;
        }
        // else, just have the speed value be the same as walking.
        else
        {
            speed = walkSpeed;
        }

        
        // The last height and scale of the objects is assigned here.
        float lastHeight = charControl.height;
        Vector3 lastScale = newScale;

        // lerp the childs local scale so that it is always the same no matter the scale of the player.
        gameObject.transform.GetChild(1).localScale = Vector3.Lerp(gameObject.transform.GetChild(1).localScale, lastScale, 5.0f * Time.deltaTime);


        // lerp the height of the player.
        charControl.height = Mathf.Lerp(charControl.height, newHeight, 5.0f * Time.deltaTime);
        charControl.center = new Vector3(newCentre.x, newCentre.y, newCentre.z);

        // force the position of the player upwards so that it is not going though the floor.
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (charControl.height - lastHeight) * 0.5f, gameObject.transform.position.z);

    }


}



