using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    GameController gc;
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        //khi cham vao nguoi choi thi gameover
        if (gc.CheckBoundWithPlayer(gameObject,gc.player) && gc.player.GetComponent<PlayerController>().canMove)
        {
            StartCoroutine(gc.Gameover());
        }

        //khi di ra goc trai man hinh thi destroy
        if (gc.CheckBoundWithPlayer(gameObject, gc.leftOffScreen))
        {
            Destroy(gameObject);
        }
    }
}
