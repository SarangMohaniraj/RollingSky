using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float repTime;
    public GameObject bomb;
    static float t;
    public float bulletSize;
    // Start is called before the first frame update
    void Start()
    {
        t = repTime;
        InvokeRepeating("Shoot", 0, repTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bomb);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.AddComponent<SphereCollider>();
        bullet.transform.localScale = new Vector3(bulletSize, bulletSize, bulletSize);
        bullet.transform.parent = transform;
        bullet.tag = "Bullet";
        bullet.name = "Bullet";
        bullet.AddComponent<Bullet>();
        

    }

    public class Bullet : MonoBehaviour
    {
        float timer = t;

        private void Update()
        {
            transform.Translate(Vector3.back * 60f * Time.deltaTime);
            if (timer > 0)
                timer -= Time.deltaTime;

            else
                Destroy(gameObject);
        }
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}
