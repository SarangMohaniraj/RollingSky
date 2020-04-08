using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public GameObject cameraObject;
    Vector3 offset;

    public float speed;
    float hSpeed { get { return speed * 1.5f; } }
    //float velocity;
    public float fallMultiplier;
    Rigidbody rb;
    public static Vector3 startPos { get; set; }
    float timer = 5f;

    public Text text;
    public float gameTimer = 45f;
    bool win = false;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        offset = cameraObject.transform.position - transform.position;
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!win)
        {
            gameTimer -= Time.deltaTime;
            if (gameTimer < 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            text.text = (gameTimer % 60).ToString("f2");
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right * speed, Space.World);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * hSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * hSpeed * Time.deltaTime, Space.World);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) && IsGrounded()) //&& rb.velocity.y == 0
            Jump(speed/3f);

        if (rb.velocity.y < 0)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        else if(rb.velocity.y > 0)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier/2 - 1) * Time.fixedDeltaTime;

        rb.velocity = new Vector3(0, rb.velocity.y, 0);

    }

    private void LateUpdate()
    {
        cameraObject.transform.position = transform.position + offset;

        if (!IsGrounded() && !Physics.Raycast(transform.position, Vector3.up, out _, 20f))
        {
            timer -= Time.deltaTime;

            if(timer <= 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            if (timer != 5f) timer = 5f;
        }
    }

    bool IsGrounded() => Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Tile");

    void Jump(float force) => rb.AddForce(Vector3.up * force, ForceMode.Impulse);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("VictoryPlatform"))
        {
            text.text = "You won with " + (gameTimer % 60).ToString("f2") + " seconds remaining!!!";
            speed = 0;
            text.color = Color.green;
            win = true;
        }
            
        else if (!win && collision.gameObject.CompareTag("PingPong") || collision.gameObject.CompareTag("Hammer") || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Fan"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        

    }


}
