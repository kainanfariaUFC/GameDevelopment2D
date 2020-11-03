﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    public float fallingTime;
    private TargetJoint2D target;
    private BoxCollider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        collider2D = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision){
        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 center = GetComponent<BoxCollider2D>().bounds.center;
        if(collision.gameObject.tag == "Player"){
            if(contactPoint.y > center.y){
                Invoke("Falling", fallingTime);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.layer == 9){
            Destroy(gameObject);
        }
    }

    void Falling(){
        target.enabled = false;
        collider2D.isTrigger = true;
    }


}
