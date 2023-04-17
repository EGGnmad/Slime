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
    [SerializeField] private float dashingPower = 3f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    public bool isDashing;
    private bool canDash = true;


    [Header("Others")]
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private FloatingSwordGenerator swordGenerator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        swordGenerator = GetComponent<FloatingSwordGenerator>();
    }

    private void Update()
    {
        // top-down move
        float horizontalValue = Input.GetAxisRaw("Horizontal");
        float verticalValue = Input.GetAxisRaw("Vertical");

        moveVelocity = new Vector2(horizontalValue, verticalValue).normalized;
        beforeDirection = moveVelocity;
        if (beforeDirection.magnitude < 0.3f)
            beforeDirection = Vector2.right * transform.localScale;
        moveVelocity *= speed;

        // dash
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            animator.SetTrigger("dash");
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            swordGenerator.Circle(16);
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
        spriteRenderer.color = Color.gray;
        canDash = false;
        isDashing = true;
        rb.velocity = beforeDirection * dashingPower;

        yield return new WaitForSeconds(dashingTime);


        spriteRenderer.color = Color.white;
        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }
}
