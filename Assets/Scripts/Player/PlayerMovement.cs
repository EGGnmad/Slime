using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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


    private bool isStunned;

    [Header("Others")]
    [SerializeField] private int damage;
    private bool isFacingRight = true;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AttackClass attack;

    [SerializeField] private GameObject spaceKeyUI;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attack = GetComponent<AttackClass>();
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
        if (Input.GetKeyDown(KeyCode.Space) && canDash && !isStunned)
        {
            animator.SetTrigger("dash");
            StartCoroutine(Dash());
        }

        if (isDashing)
        {
            attack.Attack(damage, 0.5f);
        }

        if (!canDash)
        {
            spaceKeyUI.GetComponent<Image>().fillAmount += Time.deltaTime / (dashingCooldown+dashingTime);
        }
    }

    private void LateUpdate()
    {
        if (isDashing || isStunned) return;

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
        spaceKeyUI.GetComponent<Image>().fillAmount = 0f;

        yield return new WaitForSeconds(dashingTime);

        isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }

    public void Stunned(float time)
    {
        StartCoroutine(Stun(time));
    }

    private IEnumerator Stun(float time)
    {
        rb.velocity = Vector2.zero;
        isStunned = true;
        yield return new WaitForSeconds(time);
        isStunned = false;
    }
}
