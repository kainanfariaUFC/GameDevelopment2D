using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public bool isJumping;
    public bool doubleJump;
    private AudioSource jumpAudio;

    private Rigidbody2D rgbdy;
    private Animator anime;
    

    // Start is called before the first frame update
    void Start()
    {
        rgbdy = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        jumpAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move(){
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;

        if(Input.GetAxis("Horizontal") > 0f){
            anime.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if(Input.GetAxis("Horizontal") < 0f){
            anime.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if(Input.GetAxis("Horizontal") == 0f){
            anime.SetBool("walk", false);
        }

    }

    void Jump(){
        if(Input.GetButtonDown("Jump")){
            jumpAudio.Play();
            if(!isJumping){
                rgbdy.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anime.SetBool("jump", true);
            }else{
                if(doubleJump){
                    rgbdy.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 8){
            isJumping = false;
            anime.SetBool("jump", false);
        }
        if(collision.gameObject.tag == "Spike"){
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
        if(collision.gameObject.tag == "Saw"){
            if( Apple.instance.score > 0) {
                Apple.instance.score = 0;
            }else{
                GameController.instance.ShowGameOver();
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.layer == 8){
            isJumping = true;
        }
    }
}
