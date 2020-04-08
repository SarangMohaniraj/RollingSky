using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    bool active;
    Rigidbody rb;
    Vector3 startPos;
    public float force;
    //Freeze ALL but position Y

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        active = false;

        if (gameObject.GetComponent<MeshCollider>())
            Destroy(gameObject.GetComponent<MeshCollider>());
        if (!gameObject.GetComponent<BoxCollider>())
            gameObject.AddComponent<BoxCollider>();
        if (!gameObject.GetComponent<Rigidbody>())
            gameObject.AddComponent<Rigidbody>();

        rb = GetComponent<Rigidbody>();

        Material mat = GetComponent<MeshRenderer>().material;
        if (mat.GetColor("_Color") != Color.magenta)
            mat.SetColor("_Color", Color.magenta);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
        {
            rb.AddForce(Vector3.up * force, ForceMode.Impulse);
            active = false;
        }
        else if (transform.position.y < startPos.y-.1f)
        {
            rb.isKinematic = true;
            transform.position = startPos;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            active = true;
            rb.isKinematic = false;
        }
            
    }

}
