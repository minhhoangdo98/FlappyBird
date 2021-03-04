using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    GameController gc;
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        //khi nguoi choi cham dat thi gameover
        if (gc.CheckBoundWithPlayer(gameObject, gc.player) && gc.player.GetComponent<PlayerController>().canMove)
        {
            StartCoroutine(gc.Gameover());
        }

        //reset lai vi tri sang goc ben phai khi cham vao goc ben trai
        if (gc.CheckBoundWithPlayer(gameObject, gc.leftOffScreen))
        {
            gameObject.transform.position = new Vector2(gc.rightOffScreen.transform.position.x, gameObject.transform.position.y);
        }
    }
}
