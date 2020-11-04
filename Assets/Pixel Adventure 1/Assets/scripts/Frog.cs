using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    private Rigidbody2D rd;
    private Animator ani;

    public float speed;

    public Transform right;
    public Transform left;

    public Transform headPoint;

    private bool colliding;

    public LayerMask mask;

    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rd.velocity = new Vector2(speed, rd.velocity.y);
        colliding = Physics2D.Linecast(right.position, left.position, mask);

        if(colliding){
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed = -speed;
        }
    }
    bool playerDestroyed = false;
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){
            float height = collision.contacts[0].point.y - headPoint.position.y;
            if(height > 0 && !playerDestroyed){
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5, ForceMode2D.Impulse);
                speed = 0;
                boxCollider2D.enabled = false;
                circleCollider2D.enabled = false;
                rd.bodyType = RigidbodyType2D.Kinematic;
                ani.SetTrigger("hitFrog");
                Destroy(gameObject, .33f);
               
            }else{
                playerDestroyed = true;
                GameController.instance.ShowGameOver();
                Destroy(collision.gameObject);
            }
        }
    }
}
