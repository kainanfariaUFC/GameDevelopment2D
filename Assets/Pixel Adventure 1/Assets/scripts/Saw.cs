using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
   
   public float speed;
   public float moveTime;
   private bool side = true;
   private float timer;

    // Update is called once per frame
    void Update()
    {
        if(side){
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }else{
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        timer -= -Time.deltaTime;
        if(timer >= moveTime){
            side = !side;
            timer = 0;
        }
    }
    
}
