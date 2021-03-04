using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float gravity = 2f;
    [SerializeField]
    float jumpPower = 5f;
    public AudioClip flySound, hurtSound;
    public bool canMove = true;
    GameController gc;
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        //trong luc
        if (canMove && gc.gameplay)
            gameObject.transform.Translate(Vector2.down * gravity * Time.deltaTime);      
    }

    private void FixedUpdate()
    {
        //dieu khien
        if (canMove && gc.gameplay)
        {
            if (Input.GetButton("Jump") || Input.GetButton("Fire1"))
            {
                gameObject.transform.Translate(Vector2.up * jumpPower * Time.deltaTime);
                gameObject.GetComponent<AudioSource>().clip = flySound;
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }
}
