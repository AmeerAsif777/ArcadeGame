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
    [SerializeField] bool isWalking = false;

    [SerializeField] bool isHolding = false;
    [SerializeField] float forceOfJumping = 1f;
    [SerializeField] float crouchScale = 1.5f;
    [SerializeField] Camera userView = null;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Cursor.visible = false;
        ogScale = transform.localScale;
    }

    //called once per frame

    private void checkingArrow()
    {
        /*float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        Vector3 rotateX = new Vector3(mouseY * verticalSensitivity, 0, 0);
        Vector3 rotateY = new Vector3(0, mouseX * horizontalSensitivity, 0);

        rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(rotateY));
        userView.transform.Rotate(-rotateX);
        */
    }
    private void Update()
    {
        CheckingCameraMovement();
        if (Input.GetKeyDown(KeyCode.Space) && !travelnAir)
        {
            rigidBody.AddForce(new Vector3(0, forceOfJumping, 0));
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
        if (isWalking)
        {
            CheckForMovement(speed / 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.visible) Cursor.visible = false;
            else Cursor.visible = true;
        }
        if (isWalking)
        {
            CheckForMovement(speed / 1.5f);
        }

        if (isHolding)
        {
            CheckingCameraMovement();
            CheckForMovement(speed / 1.5f);
        }

        if (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space)) CheckForMovement(speed);
        else CheckForMovement(speed / 1.5f);
    }


    private void OnCollisionEnter(Collision c) { if (c.gameObject.tag == "Terrain") travelnAir = false; }

    private void CheckForMovement(float moveSpeed)
    {
        rigidBody.MovePosition(transform.position + (transform.right * Input.GetAxis("Vertical") * moveSpeed)
            + (transform.forward * -Input.GetAxis("Horizontal") * moveSpeed));
    }


    private void CheckingCameraMovement()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        Vector3 rotateX = new Vector3(mouseY * verticalSensitivity, 0, 0);
        Vector3 rotateY = new Vector3(0, mouseX * horizontalSensitivity, 0);

        rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(rotateY));
        userView.transform.Rotate(-rotateX);
    }
}
