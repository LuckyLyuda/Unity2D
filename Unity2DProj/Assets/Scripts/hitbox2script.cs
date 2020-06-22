using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox2script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public int speed = 2;
    double timer = 1;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            Destroy(gameObject);
        }
    }
}
