using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trampoline : MonoBehaviour
{
    
    private Animator anima;

    void Start(){
        anima = GetComponent<Animator>();
    }

    public float jump_force;


    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            anima.SetTrigger("jump");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jump_force), ForceMode2D.Impulse);
            anima.SetTrigger("jump");
        }
    }
}
