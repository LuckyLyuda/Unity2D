using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Move : MonoBehaviour
{
    Rigidbody rb;
    public int health2 = 100;
    public float meter2 = 0f;
    public Text Health2;
    public Text Meter2;
    public GameObject hitbox;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Health2.text = "Health: " + health2;
        Meter2.text = "Meter: " + meter2;
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
        if (grounded == true && Input.GetKeyDown(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 8, 0);
            grounded = false;
        }
        if (meter2 >= 10 && Input.GetKeyDown(KeyCode.DownArrow))
        {
            Instantiate(hitbox, transform.position, transform.rotation);
            meter2 = meter2 - 10;
        }
        if (meter2 <= 100f)
        {
            meter2 = meter2 + 10 * Time.deltaTime;
            Meter2.text = "Meter: " + meter2;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            grounded = true;
        }

        if (collision.gameObject.tag == "hitbox1")
        {
            health2 = health2 - 10;
            Health2.text = "Health: " + health2;
            if (health2 <= 0)
            {
                Health2.text = "Game over!";
                meter2 = 0;
                Destroy(gameObject);
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            //coin disappears
            meter2 = meter2 + 10f;
            Meter2.text = "Meter: " + meter2;
            Destroy(other.gameObject);
            GetComponent<AudioSource>().Play();
        }
    }
}
