using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyAnimations : MonoBehaviour
{

    public Animator anim;
    public GameObject Enemy;
    void Start()
    {

    }


    void Update()       //Anim
    {
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isWalking", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKey(KeyCode.E))
        {
            anim.SetBool("isShooting", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("isShooting", false);
        }

        if (Input.GetKey(KeyCode.R))
        {
            anim.SetBool("isDead", true);
            Destroy(Enemy, 10f);
        }
        


    }
}
