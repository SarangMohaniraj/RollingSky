using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseTile : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update

    void Start()
    {
        if (gameObject.GetComponent<MeshCollider>())
            Destroy(gameObject.GetComponent<MeshCollider>());
        if (gameObject.GetComponent<BoxCollider>() == null)
            gameObject.AddComponent<BoxCollider>();

        rb = !gameObject.GetComponent<Rigidbody>() ? gameObject.AddComponent<Rigidbody>() : gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -100f)
            Destroy(gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            rb.constraints = RigidbodyConstraints.None;
    }
}
