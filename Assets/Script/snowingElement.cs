using UnityEngine;

public class SnowingElement : MonoBehaviour
{
    private bool isSparkExist = false;
    [SerializeField] bool sunRaise = true;
    [SerializeField] bool isSnowing = false;
    [SerializeField] GameObject Snowpartice = null;
    private void Start()
    {
        //  rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isSnowing)
        {
            /*SkinnedMeshRenderer bowSkin = bowElement.transform.GetComponent<SkinnedMeshRenderer>();
            Rigidbody arrowRB = arrow.transform.GetComponent<Rigidbody>();
            SkinnedMeshRenderer arrowSkin = arrow.transform.GetComponent<SkinnedMeshRenderer>();
            PotencyOfArrow af = arrow.transform.GetComponent<PotencyOfArrow>();
            rigidBody.MovePosition(transform.position + (transform.right * Input.GetAxis("Vertical") * moveSpeed)
                      + (transform.forward * -Input.GetAxis("Horizontal") * moveSpeed));
                      */
        }
    }
}