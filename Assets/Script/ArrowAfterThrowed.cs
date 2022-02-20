using UnityEngine;

public class ArrowAfterThrowed : MonoBehaviour
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
        if (isSparkExist && !sparks.GetComponent<ParticleSystem>().isEmitting)
        {
            Destroy(sparks, 1f);
            isSparkExist = false;
        }
        if (isHitted && isEmbedded)
        {
            isHitted = true;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Arrow" || col.gameObject.tag == "Player") return;
        transform.GetComponent<PotencyOfArrow>().enabled = false;
        rb.isKinematic = true;
        sparks = Instantiate(SparkElementOfPrefab, transform) as GameObject;
        SparkElementOfPrefab.transform.rotation = transform.rotation;
        isSparkExist = true;
        transform.localScale += new Vector3(3, 3, 3);
        transform.SetParent(GameObject.FindGameObjectWithTag("ArrowContainer").transform, true);
    }
}