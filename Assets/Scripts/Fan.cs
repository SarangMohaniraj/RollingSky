using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    public int numberOfRings, obstaclesPerRing;
    public float radius, angle; //use negative angle to go in opposite direction
    public bool horizontal = true; //true is horizontal, false is vertical
    Vector3 axis;

    // Start is called before the first frame update
    void Start()
    {
        axis = horizontal ? Vector3.up : Vector3.forward;

        List<GameObject> clones = new List<GameObject>();
       
        float dAngle = 360f / (float)obstaclesPerRing;
        for (int ring = 1; ring <= numberOfRings; ring++)
        {
            for (int i = 0; i < obstaclesPerRing; i++)
            {
                Quaternion rotation = Quaternion.AngleAxis(i * dAngle, axis);
                Vector3 direction = rotation * (horizontal ? Vector3.forward : Vector3.right);

                Vector3 position = gameObject.transform.position + (direction * radius * ring);
                GameObject go = Instantiate(gameObject, position, rotation);
                Destroy(go.GetComponent<Fan>());
                clones.Add(go);

            }
        }

        foreach (GameObject clone in clones)
            clone.transform.parent = gameObject.transform;
       
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(axis, angle);
    }


}
