using System.Collections;
using UnityEngine;

public class ShootArrowAndBow : MonoBehaviour
{
    private GameObject arrow;
    private TrailRenderer trailR;
    private bool stopDraw = false;

    private bool reset = false;
    private bool isFiring = false;
    private Vector3 bowPositionChanging = new Vector3(0, 0.005f * 2f, -0.0025f * 2f);
    private Vector3 arrowPositionChanging = new Vector3(-0.01651724137f * 2f, -0.00655172413f * 2f, 0.00125862068f * 2f);
    private Vector3 entireBowPositionChanges = new Vector3(0, 0, 0);
    private Vector3 entireArrowPositionChanges = new Vector3(0, 0, 0);
    private bool isarrowKnocked = false;
    private float drawDistance = 0;
    private Quaternion rotationOriginal;
    private Vector3 positionOriginal;
    [SerializeField] GameObject PreFabOfArrowElement = null;
    [SerializeField] GameObject bowElement = null;
    [SerializeField] int numberOfArrows = 10;
    [SerializeField] int pullSpeed = 10;
    private float changeRot = 45f / 46f;
    private float totalHipRotChange = -85f;

    private void Start()
    {
        rotationOriginal = bowElement.transform.localRotation;
        positionOriginal = bowElement.transform.localPosition;
        makeArrowToSpawn();
    }

    private void Update()
    {
        makeArrowToShoot();
        if (reset)
        {
            if (bowElement.transform.localRotation.eulerAngles.x >= rotationOriginal.eulerAngles.x) StartCoroutine(makeRotationNull());
            else reset = false;
        }
    }
    /*    private void CheckingCameraMovement()
        {
            float mouseX = Input.GetAxisRaw("Mouse X"); //get x input
            float mouseY = Input.GetAxisRaw("Mouse Y"); //get y input

            Vector3 rotateX = new Vector3(mouseY * verticalSensitivity, 0, 0); //calculate the x rotation based on the y input
            Vector3 rotateY = new Vector3(0, mouseX * horizontalSensitivity, 0); //calculate the y rotation based on the x input

            rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(rotateY)); //rotate rigid body
            userView.transform.Rotate(-rotateX); //rotate the camera negative to the x rotation (so the movement isn't inversed)
        }
    */
    private void makeArrowToSpawn()
    {
        if (numberOfArrows > 0)
        {
            isarrowKnocked = true;
            arrow = Instantiate(PreFabOfArrowElement, transform.position, transform.rotation) as GameObject;
            arrow.transform.SetParent(transform, true);
            arrow.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            trailR = arrow.GetComponent<TrailRenderer>();
            if (reset)
            {
                reset = false;
                bowElement.transform.localRotation = rotationOriginal;
                bowElement.transform.localPosition = positionOriginal;
            }

        }
    }

    private IEnumerator makeRotationNull()
    {
        while (bowElement.transform.localRotation.eulerAngles.x > rotationOriginal.eulerAngles.x)
        {
            bowElement.transform.Rotate(Time.deltaTime * -5, 0, 0, Space.Self);
            if (bowElement.transform.localPosition.y >= -0.4) bowElement.transform.localPosition = new Vector3(0, bowElement.transform.localPosition.y - 0.002f, bowElement.transform.localPosition.z);
            yield return new WaitForSeconds(0.001f);
        }
    }

    private IEnumerator revertRotation()
    {
        while (bowElement.transform.localRotation.eulerAngles.x > rotationOriginal.eulerAngles.x)
        {
            bowElement.transform.Rotate(Time.deltaTime * -5, 0, 0, Space.Self);
            bowElement.transform.localPosition -= entireBowPositionChanges;
            transform.localPosition -= entireArrowPositionChanges;
            entireBowPositionChanges = new Vector3(0, 0, 0);
            entireArrowPositionChanges = new Vector3(0, 0, 0);
            if (bowElement.transform.localPosition.y >= -0.4) bowElement.transform.localPosition = new Vector3(0, bowElement.transform.localPosition.y - 0.002f, bowElement.transform.localPosition.z);
            yield return new WaitForSeconds(0.001f);
        }
    }

    private void ResetBow()
    {
        entireBowPositionChanges.y = 0;
        bowElement.transform.localPosition -= entireBowPositionChanges;
        transform.localPosition -= entireArrowPositionChanges;
        entireBowPositionChanges = new Vector3(0, 0, 0);
        entireArrowPositionChanges = new Vector3(0, 0, 0);
        totalHipRotChange = -85f;
        reset = true;
    }

    private void makeArrowToShoot()
    {
        if (numberOfArrows > 0)
        {
            SkinnedMeshRenderer bowSkin = bowElement.transform.GetComponent<SkinnedMeshRenderer>();
            Rigidbody arrowRB = arrow.transform.GetComponent<Rigidbody>();
            SkinnedMeshRenderer arrowSkin = arrow.transform.GetComponent<SkinnedMeshRenderer>();
            PotencyOfArrow af = arrow.transform.GetComponent<PotencyOfArrow>();
            if (Input.GetMouseButton(0) && !stopDraw)
            {
                if (Input.GetKeyDown(KeyCode.R)) { stopDraw = true; drawDistance = 0; ResetBow(); }
                if (!stopDraw)
                {
                    drawDistance += Time.deltaTime * pullSpeed;
                    if (drawDistance < 100)
                    {
                        bowElement.transform.localPosition += bowPositionChanging;
                        transform.localPosition += arrowPositionChanging;
                        bowElement.transform.localRotation = Quaternion.Euler(totalHipRotChange += changeRot, -90, 0);
                        entireBowPositionChanges += bowPositionChanging;
                        entireArrowPositionChanges += arrowPositionChanging;
                    }
                    else drawDistance = 100;
                }
            }
            if (isFiring)
            {
                // transform.localScale -= new Vector3(0, transform.localScale.y - crouchScale, 0);
                // inCrouch = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (stopDraw) stopDraw = false;
                else if (drawDistance >= 10)
                {
                    arrow.transform.SetParent(null);
                    isarrowKnocked = false;
                    arrow.transform.position = transform.position;
                    arrowRB.isKinematic = false;
                    af.forceToShoot = af.forceToShoot * ((drawDistance / 100) + 0.05f);
                    drawDistance = 0;
                    af.enabled = true;
                    numberOfArrows -= 1;
                    trailR.enabled = true;
                    ResetBow();
                }
                else
                {
                    drawDistance = 0;
                    ResetBow();
                }
            }
            arrowSkin.SetBlendShapeWeight(0, drawDistance);
            bowSkin.SetBlendShapeWeight(0, drawDistance);
        }

        if (Input.GetMouseButtonDown(0) && !isarrowKnocked) makeArrowToSpawn();
    }
}
