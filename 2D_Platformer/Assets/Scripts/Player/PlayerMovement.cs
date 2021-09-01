using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D playerRB;
    private float speed;
    private float jumpForce;
    private bool isJumping;
    private bool doubleJump;
    public GameObject playerSprite;
    private PlayerCombat playerCombat;
    /*__________GETTERS AND SETTERS__________*/
    public float Speed { get => speed; set => speed = value; }
    public float JumpForce { get => jumpForce; set => jumpForce = value; }
    public bool IsJumping { get => isJumping; set => isJumping = value; }
    public bool DoubleJump { get => doubleJump; set => doubleJump = value; }

    /*_______________________________________*/


    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GetComponent<PlayerCombat>();
        Speed = 5f;
        JumpForce = 7f;
        playerRB = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
        
    }
    private void Move()
    {
       
        float movement = Input.GetAxis("Horizontal");
        if (!playerCombat.IsAtacking)
        {
            if (movement < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                playerSprite.GetComponent<Animator>().SetInteger("transition", 1);
            }
            else if (movement > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                playerSprite.GetComponent<Animator>().SetInteger("transition", 1);
            }
            else if (movement == 0 && !IsJumping)
            {
                playerSprite.GetComponent<Animator>().SetInteger("transition", 0);
            }
            playerRB.velocity = new Vector2(movement * Speed, playerRB.velocity.y);
        }

        
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!IsJumping)
            {
                playerSprite.GetComponent<Animator>().SetInteger("transition", 2);
                playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                IsJumping = true;
                DoubleJump = true;
            }
            else if (DoubleJump)
            {
                playerSprite.GetComponent<Animator>().SetInteger("transition", 2);
                playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                DoubleJump = false;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(6))
        {
            IsJumping = false;
        }
    }


}
