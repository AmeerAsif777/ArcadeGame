using UnityEngine;

public class Scoring : MonoBehaviour
{
    private bool isSparkExist = false;
    [SerializeField] bool isHitted = false;
    [SerializeField] bool isEmbedded = false;
    [SerializeField] GameObject SparkElementOfPrefab = null;
    private Rigidbody rigidBody;
    private GameObject sparks;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        float moveSpeed = 1f;
        if (isSparkExist && isEmbedded)
        {
            rigidBody.MovePosition(transform.position + (transform.right * Input.GetAxis("Vertical") * moveSpeed)
                      + (transform.forward * -Input.GetAxis("Horizontal") * moveSpeed));
        }
    }
}