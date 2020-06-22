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
    public int gold = 0;
    public Text UIText;
    public Text Health;
    public Text Meter;
    public Text Gold;
    public GameObject hitbox;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Health.text = "Health: " + health;
        Meter.text = "Meter: " + meter;
        Gold.text = "Gold: " + gold;
    }
    bool grounded = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(0.01f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-0.01f, 0, 0);
        }
        if (grounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 8, 0);
            grounded = false;
        }
        if (meter >= 10 && Input.GetKeyDown(KeyCode.F))
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

        if (collision.gameObject.tag == "killbox")
        {
            health = health - 10;
            Health.text = "Health: " + health;
            if (health <= 0)
            {
                Health.text = "Game over!";
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            //coin disappears
            gold++;
            Gold.text = "Gold: " + gold;
            meter = meter + 10f;
            Meter.text = "Meter: " + meter;
            Destroy(other.gameObject);
            GetComponent<AudioSource>().Play();
        }
    }
}
