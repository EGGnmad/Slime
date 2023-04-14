using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float speed = 3f;
    Vector2 moveVelocity;
    Vector2 beforeDirection;


    [Header("Dash")]
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    private bool canDash = true;
    private bool isDashing;


    [Header("Others")]
    private bool isFacingRight = true;


    private Rigidbody2D rb;
    private Animator animator;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // top-down move
        float horizontalValue = Input.GetAxisRaw("Horizontal");
        float verticalValue = Input.GetAxisRaw("Vertical");

        moveVelocity = new Vector2(horizontalValue, verticalValue).normalized;
        beforeDirection = moveVelocity;

        moveVelocity *= speed;

        // dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            animator.SetTrigger("dash");
            StartCoroutine(Dash());
        }
    }

    private void LateUpdate()
    {
        if (isDashing) return;
        rb.velocity = moveVelocity;
        Flip();
    }

    private void Flip()
    {
        if(isFacingRight && moveVelocity.x < 0f || !isFacingRight && moveVelocity.x > 0f)
        {
            isFacingRight = !isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = beforeDirection * dashingPower;

        yield return new WaitForSeconds(dashingTime);

        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }
}
