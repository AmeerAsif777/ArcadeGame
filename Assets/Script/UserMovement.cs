using UnityEngine;

public class UserMovement : MonoBehaviour
{
    private Rigidbody rigidBody;
    private bool travelnAir = false;

    private bool inCrouch = false;
    private Vector3 ogScale;
    [SerializeField] float verticalSensitivity = 2;
    [SerializeField] float horizontalSensitivity = 2;
    [SerializeField] float speed = 0.5f;
    [SerializeField] bool isJumping = false;
    [SerializeField] float forceOfJumping = 1f;
    [SerializeField] float crouchScale = 1.5f;
    [SerializeField] Camera userView = null;

    //Called when the gameobject is created (which is at the start of the game)
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); //get the player's rigidbody component
        Cursor.visible = false; //make it so the cursor isn't visible
        ogScale = transform.localScale;
    }

    //called once per frame
    private void Update()
    {
        //Check to see if the player wants to move their camera
        CheckingCameraMovement();
        if (Input.GetKeyDown(KeyCode.Space) && !travelnAir)
        {
            rigidBody.AddForce(new Vector3(0, forceOfJumping, 0)); //jump
            travelnAir = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !travelnAir && !inCrouch)
        {
            transform.localScale -= new Vector3(0, transform.localScale.y - crouchScale, 0);
            inCrouch = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && !travelnAir && inCrouch)
        {
            transform.localScale = ogScale;
            inCrouch = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.visible) Cursor.visible = false; //make it so the cursor isn't visible
            else Cursor.visible = true; //make it so the cursor is visible
        }

        //If you're not aiming, move at regular speed
        if (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space)) CheckForMovement(speed);
        //else you'll move at half speed
        else CheckForMovement(speed / 1.5f);
    }

    //Check for ground collision 
    private void OnCollisionEnter(Collision c) { if (c.gameObject.tag == "Terrain") travelnAir = false; }

    //When the player goes to move, we'll move their rigidbody
    private void CheckForMovement(float moveSpeed)
    {
        rigidBody.MovePosition(transform.position + (transform.right * Input.GetAxis("Vertical") * moveSpeed)
            + (transform.forward * -Input.GetAxis("Horizontal") * moveSpeed));
    }

    //When the player attempts to move their camera
    private void CheckingCameraMovement()
    {
        float mouseX = Input.GetAxisRaw("Mouse X"); //get x input
        float mouseY = Input.GetAxisRaw("Mouse Y"); //get y input

        Vector3 rotateX = new Vector3(mouseY * verticalSensitivity, 0, 0); //calculate the x rotation based on the y input
        Vector3 rotateY = new Vector3(0, mouseX * horizontalSensitivity, 0); //calculate the y rotation based on the x input

        rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(rotateY)); //rotate rigid body
        userView.transform.Rotate(-rotateX); //rotate the camera negative to the x rotation (so the movement isn't inversed)
    }
}
