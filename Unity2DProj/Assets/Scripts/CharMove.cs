﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    public GameObject sword;
    private Vector2 moveinput;

    //public GameObject win;

    public int player = 0; // player 0/1 (0:arrows 1:wasd)
    private string axis;
    public float maxMeter = 100f;
    public float jump = 0.1f;
    public float speed = 5f;
    private UnityEngine.KeyCode[,] controls;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Health =  GameObject.Find("health" + (player + 1)).GetComponent<Text>();
        Meter = GameObject.Find("meter" + (player + 1)).GetComponent<Text>();
        Health.text = "Health: " + health;
        Meter.text = "Meter: " + meter;


        controls = new UnityEngine.KeyCode[,] { { KeyCode.W, KeyCode.UpArrow }, { KeyCode.Q, KeyCode.Keypad1 }, { KeyCode.E, KeyCode.Keypad2 }, { KeyCode.F, KeyCode.Keypad3 }, { KeyCode.Z, KeyCode.Keypad4 }, { KeyCode.R, KeyCode.Keypad5 } };
        

        /*
         * player1  player2 
         * hor      horwasd
         * up       w
         * q        1
         * e        2
         * 
        */

        if (player == 0)
            axis = "HorizontalWASD";
        else
            axis = "Horizontal";

    }
    bool isGrounded = false;
    // Update is called once per frame
    void Update()
    {
        //Movement
        //
        float x = Input.GetAxisRaw(axis);


        string[] animations = new string[] {"testAnim", "swordright", "spinattack", "upair", "downair"};
        if (isGrounded == true && Input.GetKeyDown(controls[0, player]))
        {

            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            isGrounded = false;
        }
        rb.velocity = new Vector2(x * speed, rb.velocity.y);
        

        for(int i = 0; i < animations.Length ; i++)
        {
            //UnityEngine.Debug.Log(i);
            if(meter >= 10 && Input.GetKeyDown(controls[i + 1, player]))
            {
                sword.SetActive(true);
                GetComponent<Animation>().Play(animations[i]);
                meter -= 10;
            }
        }    
 



        if (meter <= maxMeter)
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
            //meter = meter + 10f;
            //Meter.text = "Meter: " + meter;
            maxMeter += 20;
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
        if (other.gameObject.tag == "killbox" || other.gameObject.tag == "sword")
        {
            //if (other.transform.parent.name.Equals(this.transform.parent.name))
            //    return;

            

            health = health - 15;
            Health.text = "Player " + player + " Health: " + health;
            if (health <= 0)
            {
                Health.text = "Game over!";
                endGame();
                meter = 0;
                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag == "spear")
        {
            //if (other.transform.parent.name.Equals(this.transform.parent.name))
            //    return;

            health = health - 10;
            Health.text = "Player " + player + " Health: " + health;
            if (health <= 0)
            {
                Health.text = "Game over!";
                endGame();
                meter = 0;
                Destroy(gameObject);
            }
        }
    }

    public void endGame()
    {
        int winner = player == 0 ? 1 : 0;
        try
        {
            GameObject x = GameObject.Find("Game Over");
            x.transform.position = new Vector2(764, x.transform.position.y);

            GameObject.Find("EndGameText").GetComponent<Text>().text = "Winner: Player " + winner;
            //.GetComponent<Button>().GetComponentInChildren<Text>().text = 
        }
        catch (System.Exception)
        {
            throw;
        }

        

    }
    
    
}
