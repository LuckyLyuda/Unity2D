using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class CharMove : MonoBehaviour
{
    Rigidbody rb;
    public int health = 100;
    public float meter = 0f;
    public Text UIText;
    public Text Health;
    public Text Meter;
    public GameObject hitbox;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Health.text = "Health: " + health;
        Meter.text = "Meter: " + meter;
    }
    bool grounded = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.01f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-0.01f, 0, 0);
        }
        if (grounded == true && Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 8, 0);
            grounded = false;
        }
        if (meter >= 10 && Input.GetKeyDown(KeyCode.S))
        {
            Instantiate(hitbox, transform.position, transform.rotation);
            meter = meter - 10;
        }
        if (meter <= 100f)
        {
            meter = meter + 10 * Time.deltaTime;
            Meter.text = "Meter: " + meter;
        }
    }
        public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            grounded = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            //coin disappears
            meter = meter + 10f;
            Meter.text = "Meter: " + meter;
            Destroy(other.gameObject);
            GetComponent<AudioSource>().Play();
        }
        if (other.gameObject.tag == "hitbox2")
        {
            health = health - 10;
            Health.text = "Health: " + health;
            if (health <= 0)
            {
                Health.text = "Game over!";
                meter = 0;
                Destroy(gameObject);
            }
        }
    }
}
