using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] float rotationSpeed = 150f;
    [SerializeField] float targetRotationSpeed = 5f;

    private bool isJumping = false;
    private bool isAttacking = false;
  

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float moveDirection = HandleMovement();
        HandleRotate(moveDirection);
        HandleJump();
        HandleAttack();

    }

  

    private void HandleAttack()
    {
        if (Input.GetMouseButton(0) && !isAttacking)
        {
            isAttacking = true;
            // TODO: Saldırı animasyonu eklenecek
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && !isJumping)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isJumping = true;
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
        return moveDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
    public void AttackAnimationFinished()
    {
        isAttacking = false;
        // TODO: Saldırı animasyonu durdurulacak
    }

}