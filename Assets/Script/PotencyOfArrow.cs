using UnityEngine;

public class PotencyOfArrow : MonoBehaviour
{

    private Rigidbody rigidb;
    public float forceToShoot = 2000;
    [SerializeField] float velocityOfArrow = 1f;
    [SerializeField] bool isArrowFlying = false;
    [SerializeField] bool arrowHitsGround = false;


    private void OnEnable()
    {
        rigidb = GetComponent<Rigidbody>();
        rigidb.velocity = Vector3.zero;
        // velocityOfArrow += 2;
        // Debug.Log(velocityOfArrow);
        applyingForce();
    }
    private void Update()
    {

        if (arrowHitsGround)
        {
            // arrowHitsGround = true;
            // velocityOfArrow += 2;
        }
        transform.right = Vector3.Slerp(transform.right, transform.GetComponent<Rigidbody>().velocity.normalized, Time.deltaTime);
    }
    private void applyingForce()
    {
        if (arrowHitsGround && isArrowFlying)
        {
            // isArrowFlying = true;
            // velocityOfArrow += 2;
            // forceToShoot += 4000;
        }

        rigidb.AddRelativeForce(Vector3.right * forceToShoot);
        if (isArrowFlying)
        {
            // isArrowFlying = true;
        }
    }
}
