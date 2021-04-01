using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    private BoxCollider2D playerCollider;
    public float initialYoffset, initialYsize, crouchYoffset = 0.59f, crouchYsize= 1.33f;
    public float speed;
    private Rigidbody2D rigbod2d;
    public float jump;
    public bool onGround;
    public Vector3 playerStartPosition;

    public ScoreController scoreController;

    public void KeyPicked()
    {
        //Player Pcked the key increase score
        Debug.Log("Reached KeyPicked function");
        scoreController.IncreaseScore(25);
    }

    // Start is called before the first frame update
    private void Awake()
    {
        playerCollider = gameObject.GetComponent<BoxCollider2D>();
        rigbod2d = gameObject.GetComponent<Rigidbody2D>();
        initialYoffset = playerCollider.offset.y;
        initialYsize = playerCollider.size.y;
        playerStartPosition = gameObject.transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        //Debug.Log("Player Speed is:"+horizontal);
        float vertical = Input.GetAxisRaw("Vertical");
        //Debug.Log("Player can jump : "+vertical);
        PlayerMovementAnimation(horizontal,vertical);
        MovePlayer(horizontal,vertical);
    }

    private void MovePlayer(float horizontal,float vertical)
    {
        //Move player horizontally
        Vector3 position = transform.position;
        position.x += horizontal * speed * Time.deltaTime;
        transform.position = position;

        //Move player vertically
        if(vertical>0 && onGround)
        {
            onGround = false;
           // Debug.Log("Move vertically");
            rigbod2d.AddForce(new Vector2(0f, jump), ForceMode2D.Force);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
      //  Debug.Log("OnCollisionEnter2D" + collision.collider.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
        //  Debug.Log("OnCollisionExit2D");
    }

    private void PlayerMovementAnimation(float horizontal,float vetical)
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontal));
        Vector3 scale = transform.localScale;
        if (horizontal < 0)
        {
            scale.x = -1f * Mathf.Abs(scale.x);

        }
        else if (horizontal > 0)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouch", true);
            playerCollider.offset = new Vector2(playerCollider.offset.x, crouchYoffset);
            playerCollider.size = new Vector2(playerCollider.size.x, crouchYsize);
        }
        else
        {
            animator.SetBool("isCrouch", false);
            playerCollider.offset = new Vector2(playerCollider.offset.x, initialYoffset);
            playerCollider.size = new Vector2(playerCollider.size.x, initialYsize);
        }
        
        if (vetical > 0)
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }
    }
}
