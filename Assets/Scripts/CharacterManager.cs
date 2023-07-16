using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] float rotationSpeed = 150f;
    [SerializeField] float targetRotationSpeed = 5f;

    private bool isJumping = false;
    private bool isMoving = false;
    private bool isFalling = false;

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveDirection = HandleMovement();
        HandleRotate(moveDirection);
        HandleJump();
        HandleFall();
    }


    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
            animator.SetBool("isJump", isJumping);

        }
    }

    private void HandleFall()
    {
        if (rb.velocity.y < -0.1f)
        {
            isFalling = true;
            animator.SetBool("isJump", isJumping);
            animator.SetBool("isFall", isFalling);
        }
    }

    private void HandleRotate(float moveDirection)
    {
        if (moveDirection < 0)
        {
            //spriteRenderer.flipX = true; 
            transform.rotation = Quaternion.Euler(-0f, 180f, 0f); // Sola dönme

        }
        else if (moveDirection > 0)
        {
            //spriteRenderer.flipX = false; 
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Sola dönme

        }
    }

    private float HandleMovement()
    {
        float moveDirection = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);

        if (Mathf.Abs(moveDirection) > 0.1f && !isJumping && !isFalling)
        {
            isMoving = true;
            animator.SetBool("isMove", isMoving);
        }
        else
        {
            isMoving = false;
            animator.SetBool("isMove", isMoving);
        }

        return moveDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            isFalling = false;
            animator.SetBool("isJump", isJumping);
            animator.SetBool("isFall", isFalling);
        }
        if (collision.gameObject.CompareTag("Boundary"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
   

}