using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class CharMove : MonoBehaviour
{
    Rigidbody2D rb;
    public int health = 100;
    public float meter = 0f;
    public Text UIText;
    public Text Health;
    public Text Meter;
    public GameObject hitbox;
    private Vector2 moveinput;


    public GameObject sword;
    public float jump = 0.1f;
    public float speed = 5f;
    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Health.text = "Health: " + health;
        Meter.text = "Meter: " + meter;
    }
    
    // Update is called once per frame
    void Update()
    {
        //Movement
        float x = Input.GetAxisRaw("Horizontal");
        if (isGrounded == true && Input.GetKeyDown(KeyCode.W))
        {

            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            isGrounded = false;
        }
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
        
        if(Input.GetKeyDown(KeyCode.E))
        {

            sword.SetActive(true);
            GetComponent<Animation>().Play();
            //sword.SetActive(false);

        }

        //Attack
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
    

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
           isGrounded = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
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
