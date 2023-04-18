using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingModel : Model
{
    public int maxHp;
    [SerializeField] private float godModeCooldown = 3f;
    public bool canTakeDamage = true;

    private SpriteRenderer sprite;
    private Animator animator;

    void Start()
    {
        hp = maxHp;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public override void TakeDamage(int decrease)
    {
        if (hp <= 0) return;
        if (!canTakeDamage) return;
        canTakeDamage = false;

        hp -= decrease;

        if (hp <= 0)
        {
            animator.SetBool("run", false);
            animator.SetTrigger("death");
        }

        StartCoroutine(GodModeCooldown());
        StartCoroutine(GodModeIndicator(godModeCooldown));
    }

    IEnumerator GodModeCooldown()
    {
        yield return new WaitForSeconds(godModeCooldown);
        canTakeDamage = true;
    }

    IEnumerator GodModeIndicator(float time)
    {
        sprite.color = Color.gray;
        yield return new WaitForSeconds(time);
        sprite.color = Color.white;
    }
}
