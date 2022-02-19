using UnityEngine;

public class UserWalk : MonoBehaviour
{
    private bool isSparkExist = false;
    [SerializeField] bool isHitted = false;
    [SerializeField] bool isEmbedded = false;
    [SerializeField] GameObject SparkElementOfPrefab = null;
    private Rigidbody rb;
    private GameObject sparks;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        // need to test this method
        if (isSparkExist && isHitted)
        {
            /*rb.MovePosition(transform.position + (transform.right * Input.GetAxis("Vertical") * moveSpeed)
                      + (transform.forward * -Input.GetAxis("Horizontal") * moveSpeed));
                      */
        }
    }
}