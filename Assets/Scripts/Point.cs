using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    GameController gc;
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        //nhan diem
        if (gc.CheckBoundWithPlayer(gameObject, gc.player) && gc.player.GetComponent<PlayerController>().canMove)
        {
            gc.GainPoint();
            Destroy(gameObject);

        }

        //destroy khi cham vao goc trai
        if (gc.CheckBoundWithPlayer(gameObject, gc.leftOffScreen))
        {
            Destroy(gameObject);
        }
    }
}
