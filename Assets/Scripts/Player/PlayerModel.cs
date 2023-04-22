using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerModel : Model
{
    [SerializeField] GameObject boss;
    [SerializeField] private float godModeCooldown = 3f;

    public bool canTakeDamage = true;

    private Animator animator;
    private PlayerMovement movement;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private EgoSwordGenerator egoSwordGenerator;
    [SerializeField] GameDirector gameDirector;

    void Start()
    {
        if(SceneManager.GetActiveScene().name != "StartScene")
        {
            egoSwordGenerator = GetComponent<EgoSwordGenerator>();

            int isKingClear = PlayerPrefs.GetInt("KingClear");
            if (isKingClear == 1)
            {
                egoSwordGenerator.Generate(boss);
            }

            int isTrollkingClear = PlayerPrefs.GetInt("TrollkingClear");
            if (isTrollkingClear == 1)
            {
                maxHp += 5;
            }
        }

        hp = maxHp;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        sprite = GetComponent<SpriteRenderer>();        
    }

    public override void TakeDamage(int decrease)
    {
        if (!canTakeDamage) return;
        if (movement.isDashing) return;

        canTakeDamage = false;

        hp -= decrease;

        if(hp <= 0)
        {
            rb.velocity = Vector2.zero;
            animator.SetTrigger("death");
            GetComponent<PlayerMovement>().enabled = false;

            gameDirector.PlayerDied();
        }

        cameraController.Shake(5f*decrease, 0.1f);

        StartCoroutine(GodModeCooldown());
        StartCoroutine(GodModeIndicator(godModeCooldown));
    }

    public void Stunned(float time)
    {
        if (!canTakeDamage) return;
        if (movement.isDashing) return;

        movement.Stunned(time);
    }

    IEnumerator GodModeCooldown()
    {
        yield return new WaitForSeconds(godModeCooldown);
        canTakeDamage = true;
    }

    IEnumerator GodModeIndicator(float time)
    {
        while (time >= 0f)
        {
            sprite.color = Color.gray;

            yield return new WaitForSeconds(0.2f);

            sprite.color = Color.white;

            yield return new WaitForSeconds(0.2f);

            time -= 0.4f;
        }
    }
}
