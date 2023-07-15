using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] float leftPoint;
    [SerializeField] float rightPoint;

    Collider2D coll;
    Rigidbody2D rb;
    Animator anim;

    [SerializeField] LayerMask Ground;

    bool facingLeft = true;
    float jumpLenght = 2f;
    float jumpHeight = 4f;


    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim.GetBool("isJumping"))
        {
            if (rb.velocity.y < .1)
            {
                anim.SetBool("isFalling", true);
                anim.SetBool("isJumping", false);
            }
        }
        if (coll.IsTouchingLayers(Ground) && anim.GetBool("isFalling"))
        {
            anim.SetBool("isFalling", false);
        }
    }

    // Update is called once per frame
    void Move()
    {
        if (facingLeft)
        {
            if(transform.position.x > leftPoint)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }

                if (coll.IsTouchingLayers(Ground)) 
                {
                    rb.velocity = new Vector2(-jumpLenght, jumpHeight);
                    anim.SetBool("isJumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {
            if (transform.position.x < rightPoint)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }

                if (coll.IsTouchingLayers(Ground))
                {
                    rb.velocity = new Vector2(jumpLenght, jumpHeight);
                    anim.SetBool("isJumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

    public void HandlePause()
    {
        if (isPaused)
        {
            isPaused = false;
        }
        else
        {
            isPaused = true;
        }
    }
}
