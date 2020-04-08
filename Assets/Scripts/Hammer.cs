using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public string direction; //left, right, both
    public float speed;
    public float min; //range should generally be -0.65 to 0.65
    public float max;
    public float delay;
    public Vector3 scale;

    int dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        if (direction.ToLower().Equals("both"))
        {
            if (scale != Vector3.zero)
                transform.localScale = scale;
            else
                transform.localScale = new Vector3(2f, 2f, 2f);
        }
        else
        {
            if (scale != Vector3.zero)
                transform.localScale = scale;
            else
                transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);

            if (direction.ToLower().Equals("right"))
            {
                transform.rotation = new Quaternion(0, 180f, 0,0);
            }
            
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0)
            delay -= Time.deltaTime;
        else
        {
            if (direction.ToLower().Equals("right"))
            {
                if (transform.rotation.x <= min)
                {
                    dir = 1;
                }
                if (transform.rotation.x >= max)
                {
                    dir = -1;
                }
            }
            else
            {
                if (transform.rotation.z <= min)
                {
                    dir = 1;
                }
                if (transform.rotation.z >= max)
                {
                    dir = -1;
                }
            }


            transform.Rotate(Vector3.forward * dir * speed);
        }
    }

   
}
