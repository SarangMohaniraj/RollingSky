using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    public float speed;
    float dir = 1;

    public float min,max;
   

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Renderer>())
        {
            Material mat = GetComponent<Renderer>().material;
            if (mat.GetColor("_Color") != Color.blue)
                mat.SetColor("_Color", Color.blue);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.x <= min)
        {
            dir = 1;
        }
        if (gameObject.transform.position.x >= max)
        {
            dir = -1;
        }
        gameObject.transform.Translate(Vector3.right * dir * Time.deltaTime * speed);

    }

}
